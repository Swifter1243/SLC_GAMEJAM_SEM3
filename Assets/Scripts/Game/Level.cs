using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-50)]
public class Level : MonoBehaviour, IResettable
{
	public TaskGroup[] taskGroups;

	public MonoBehaviour[] resetArray;
	private IResettable[] resetInterfaces;
	public UnityEvent onLevelComplete;

	private int _taskGroupsLeft;

	private void Start()
	{
		//Kinda cursed???
		resetInterfaces = resetArray.Cast<Object>().OfType<IResettable>().ToArray();

		foreach (TaskGroup group in taskGroups)
		{
			group.SetGroupManager(this);
		}


		ResetTaskGroups();
	}

	public void Reset()
	{
		ResetTaskGroups();
		foreach (IResettable resettable in resetInterfaces) resettable.Reset();
	}

	private void ResetTaskGroups()
	{
		_taskGroupsLeft = taskGroups.Length;
		
	}

	public void TaskGroupCompleted(TaskGroup group)
	{
		_taskGroupsLeft--;

		CheckCompletion();
	}
	private void CheckCompletion()
	{
		if (_taskGroupsLeft == 0) onLevelComplete.Invoke();
	}
}
