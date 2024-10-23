using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Resettable
{
    public TaskGroup[] taskGroups;
    public UnityEvent onLevelComplete;

    private int _taskGroupsLeft;

    private void Start()
    {
        ResetTaskGroups();
    }

    public override void Reset()
    {
        ResetTaskGroups();
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
