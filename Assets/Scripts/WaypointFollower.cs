using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaypointFollower : Resettable
{
    public Transform[] waypoints;
    public float travelSpeed = 1;
    public float restTime = 1;
    public int startingIndex = 0;
    public bool backAndForth = false;
    
    private int _currentWaypoint = 0;
    private float _transitionTime = 0;
    private bool _goingForward = true;
    private bool _isMoving = false;
    private Transform _lastWaypoint = null;

    private void Start()
    {
        _currentWaypoint = startingIndex;
        transform.position = waypoints[_currentWaypoint].position;
    }

    private void Update()
    {
        if (_isMoving)
        {
            Vector2 a = _lastWaypoint.position;
            Vector2 b = waypoints[_currentWaypoint].position;
            float travelTime = Vector2.Distance(a, b) / travelSpeed;
            
            _transitionTime += Time.deltaTime * travelSpeed;
            
            transform.position = Vector2.Lerp(a, b, _transitionTime / travelTime);

            if (_transitionTime > travelTime)
            {
                _transitionTime = 0;
                _isMoving = false;
            }
        }
        else
        {
            _transitionTime += Time.deltaTime;

            if (_transitionTime > restTime)
            {
                _transitionTime = 0;
                _isMoving = true;
                _lastWaypoint = waypoints[_currentWaypoint];
                ChooseNextWaypoint();
            }
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

	public override void Reset()
	{
        throw new NotImplementedException();
	}
}
