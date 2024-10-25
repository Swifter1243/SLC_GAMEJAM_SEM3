using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
public class Door : Task
{
	public TaskGroup taskGroup;
	public DoorLock doorLockPrefab;
	public float lockDelay = 0.1f;
	public float openFloatPeriod = 2f;
	public float openFloatAmount = 0.3f;
	public float openFloatOffset = 1;

	public GameObject closedVisuals;
	public GameObject openVisuals;

	private readonly Dictionary<Task, DoorLock> _doorLocks = new ();
	private bool _isOpen = false;
	private float _openTime = 0;

	public void Initialize()
	{
		taskGroup.onTaskComplete.AddListener(UnlockLock);
		openVisuals.SetActive(false);
		closedVisuals.SetActive(true);

		int index = 0;
		foreach (Task task in taskGroup.tasks)
		{
			float y = (index + 1f) / (taskGroup.tasks.Length + 1);
			y *= 2;

			DoorLock doorLock = Instantiate(doorLockPrefab, transform);
			doorLock.SetInitialPosition(new Vector3(0, y, -1));
			_doorLocks.Add(task, doorLock);
			index++;
		}

		LockAllLocks();
	}

	private void LockAllLocks()
	{
        StopAllCoroutines();

        int index = 0;
		foreach (DoorLock doorLock in _doorLocks.Values)
		{
			doorLock.gameObject.SetActive(false);
			StartCoroutine(Lock(doorLock, index * lockDelay));
			index++;
		}
	}

	private IEnumerator Lock(DoorLock doorLock, float delay)
	{
		yield return new WaitForSeconds(delay);

		doorLock.Lock();
	}

	public void Open()
	{
		_isOpen = true;
		_openTime = 0;
		openVisuals.SetActive(true);
		closedVisuals.SetActive(false);
	}

	private void UnlockLock(Task task)
	{
		_doorLocks[task].Open();
	}

	public override void Reset()
	{
		base.Reset();
		_isOpen = false;
		openVisuals.SetActive(false);
		closedVisuals.SetActive(true);

		StopAllCoroutines();
		LockAllLocks();
	}

	private void Update()
	{
		_openTime += Time.deltaTime;
		float y = Mathf.Sin(openFloatPeriod * _openTime) * openFloatAmount + openFloatOffset;
		openVisuals.transform.localPosition = new Vector2(0, y);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == Constants.LAYER_PLAYER && _isOpen)
		{
			CompleteTask();
		}
	}
}
