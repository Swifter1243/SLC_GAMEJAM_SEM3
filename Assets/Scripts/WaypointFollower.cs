using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static System.TimeZoneInfo;

public class WaypointFollower : MonoBehaviour, IResettable
{
    public Transform[] waypoints;
    public SurfaceEffector2D effector;
    public float travelSpeed = 1;
    public float restTime = 1;
    public int startingIndex = 0;
    public bool backAndForth = false;
    
    private int _currentWaypoint = 0;
    private float _transitionTime = 0;
    private bool _goingForward = true;
    private Transform _lastWaypoint = null;

    private void Start()
    {
        if (waypoints.Length <= 1)
        {
            waypoints = new Transform[2] { transform, transform };
            Debug.LogWarning($"{name} does not have enough waypoints!");
        }
        Reset();
    }

    private IEnumerator TransitionSwitch()
    {
        for (;;) //while true
        {
            _transitionTime = 0;
            _lastWaypoint = waypoints[_currentWaypoint];
            ChooseNextWaypoint();

            Vector2 a = _lastWaypoint.position;
            Vector2 b = waypoints[_currentWaypoint].position;

            float travelTime = Mathf.Max(Vector2.Distance(a, b), 0.01f) / travelSpeed;
            if (effector) effector.speed = (b - a).x / travelTime;

            Coroutine MoveCoroutine = StartCoroutine(TransitionMove(a, b, travelTime));
            yield return new WaitForSeconds(travelTime);

            if (effector) effector.speed = 0;
            transform.position = b;
            StopCoroutine(MoveCoroutine);

            yield return new WaitForSeconds(restTime);
        }
    }

    private IEnumerator TransitionMove(Vector2 a, Vector2 b, float time)
    {
        for (;;) //while true
        {
            _transitionTime += Time.deltaTime;
            Vector3 nextPosition = Vector2.Lerp(a, b, _transitionTime / time);
            transform.position = nextPosition;
            
            yield return null;
        }
    }

    private void ChooseNextWaypoint()
    {
        if (backAndForth) ChooseNextWaypointBackAndForth();
        else ChooseNextWaypointForward();
    }
    private void ChooseNextWaypointBackAndForth()
    {
        if (_currentWaypoint >= 0 | _currentWaypoint <= waypoints.Length) { //sentinal
            _goingForward = !_goingForward;
        }
        if (_goingForward) _currentWaypoint++; else _currentWaypoint--; //continue
    }
    private void ChooseNextWaypointForward()
    {
        _currentWaypoint = ++_currentWaypoint % waypoints.Length;
    }

	public void Reset()
	{
        StopAllCoroutines();
        if (effector) effector.speed = 0;

        _goingForward = true;
        _currentWaypoint = startingIndex;
        transform.position = waypoints[_currentWaypoint].position;

        StartCoroutine(TransitionSwitch());
    }
}
