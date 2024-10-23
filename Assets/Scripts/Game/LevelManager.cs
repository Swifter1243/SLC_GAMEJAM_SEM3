using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Resettable
{
    public TaskGroup[] taskGroups;
    public UnityEvent onTaskGroupComplete;

    private int _tasksLeft;

    private void Start()
    {
        foreach (TaskGroup task in tasks)
        {
            task.SetTaskManager(this);
        }

        ResetTasks();
    }

    public override void Reset()
    {
        ResetTasks();
    }

    private void ResetTasks()
    {
        _tasksLeft = tasks.Length;
    }

    public void TaskCompleted(Task levelTask)
    {
        _tasksLeft--;

        if (_tasksLeft == 0)
        {
            onTaskGroupComplete.Invoke();
        }
    }
}
