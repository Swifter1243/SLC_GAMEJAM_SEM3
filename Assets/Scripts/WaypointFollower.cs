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
    public bool moving = false;

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
    }

    private IEnumerator TransitionSwitch()
    {
        for (;;) //while true
        {
            _transitionTime = 0;
            _lastWaypoint = waypoints[_currentWaypoint];
            ChooseNextWaypoint();

            Transform nextWaypoint = waypoints[_currentWaypoint];

            float travelTime = Mathf.Max(Vector2.Distance(_lastWaypoint.position, nextWaypoint.position), 0.01f) / travelSpeed;
            if (effector) effector.speed = (nextWaypoint.position - _lastWaypoint.position).x / travelTime;

            Coroutine moveCoroutine = StartCoroutine(TransitionMove(_lastWaypoint, nextWaypoint, travelTime));
            yield return new WaitForSeconds(travelTime);

            if (effector) effector.speed = 0;
            transform.position = nextWaypoint.position;
            StopCoroutine(moveCoroutine);

            yield return new WaitForSeconds(restTime);
        }
    }

    private IEnumerator TransitionMove(Transform a, Transform b, float time)
    {
        for (;;) //while true
        {
            _transitionTime += Time.deltaTime;
            transform.position = Vector2.Lerp(a.position, b.position, _transitionTime / time);

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
        if ((_currentWaypoint <= 0 && !_goingForward) || //Sentinal
            (_currentWaypoint >= (waypoints.Length - 1) && _goingForward))
            _goingForward = !_goingForward;

        if (_goingForward) _currentWaypoint++; else _currentWaypoint--; //Continue
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

    public void StartMoving()
    {
        moving = true;
        Reset();
    }
}
