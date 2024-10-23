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
        foreach (TaskGroup task in taskGroups)
        {
            //TODO
        }

        ResetTasks();
    }

    public override void Reset()
    {
        ResetTasks();
    }

    private void ResetTasks()
    {
        _taskGroupsLeft = taskGroups.Length;
    }

    public void TaskCompleted(Task levelTask)
    {
        _taskGroupsLeft--;

        if (_taskGroupsLeft == 0)
        {
            onLevelComplete.Invoke();
        }
    }
}
