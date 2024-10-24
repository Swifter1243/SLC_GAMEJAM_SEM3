using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : Task
{
	public TaskGroup taskGroup;
	public DoorLock doorLockPrefab;
	public float lockDelay = 0.1f;
	
	private readonly Dictionary<Task, DoorLock> _doorLocks = new ();
	private bool _isOpen = false;
	
	private void Start()
	{
		taskGroup.onTaskComplete.AddListener(UnlockLock);

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
		
		CloseLocks();
	}

	private void CloseLocks()
	{
        StopAllCoroutines();

        int index = 0;
		foreach (DoorLock doorLock in _doorLocks.Values)
		{
			doorLock.gameObject.SetActive(false);
			StartCoroutine(CloseLock(doorLock, index * lockDelay));
			index++;
		}
	}

	private IEnumerator CloseLock(DoorLock doorLock, float delay)
	{
		yield return new WaitForSeconds(delay);
		
		doorLock.Close();
	}

	public void Open()
	{
		_isOpen = true;
		// TODO: visuals
	}

	private void UnlockLock(Task task)
	{
		_doorLocks[task].Open();
	}

	public override void Reset()
	{
		base.Reset();
		_isOpen = false;

		StopAllCoroutines();
		CloseLocks();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == Constants.LAYER_PLAYER && _isOpen)
		{
			CompleteTask();
		}
	}
}
