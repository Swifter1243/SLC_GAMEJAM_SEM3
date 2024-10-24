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

            float travelTime = Vector2.Distance(a, b) / travelSpeed;
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
        if (backAndForth)
        {
            ChooseNextWaypointBackAndForth();
        }
        else
        {
            ChooseNextWaypointForward();
        }
    }
    private void ChooseNextWaypointBackAndForth()
    {
        if (_goingForward)
        {
            _currentWaypoint++;
            if (_currentWaypoint == waypoints.Length - 1)
            {
                _goingForward = false;
            }
        }
        else
        {
            _currentWaypoint--;
            if (_currentWaypoint == 0)
            {
                _goingForward = true;
            }
        }
    }
    private void ChooseNextWaypointForward()
    {
        _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
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
