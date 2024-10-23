﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class TaskGroup : Resettable
{
    public Task[] tasks;
    public UnityEvent onTaskGroupComplete;

    private int _tasksLeft;
    
    private void Start()
    {
        foreach (Task task in tasks)
        {
            task.SetTaskManager(this);
        }

        ResetTasks();
        CheckCompletion();
    }

    public override void Reset()
    {
        ResetTasks();
    }

    private void ResetTasks()
    {
        _tasksLeft = tasks.Length;
        
        //NOTE: this is no longer necessary now that the component Resettable now handles resets.
        //foreach (Task levelTask in tasks)
        //{
        //    levelTask.Reset();
        //}
    }

    public void TaskCompleted(Task levelTask)
    {
        _tasksLeft--;

        CheckCompletion();
    }

    private void CheckCompletion()
	{
        if (_tasksLeft == 0) onTaskGroupComplete.Invoke();
    }
}