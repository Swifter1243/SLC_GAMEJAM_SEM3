﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class TaskGroup : MonoBehaviour, IResettable
{
    public Task[] tasks;
    public UnityEvent onTaskGroupComplete;
    public UnityEvent<Task> onTaskComplete;
    private Level taskGroupManager;

    private int _tasksLeft;
    
    private void Start()
    {
        ResetTasks();
        CheckCompletion();
    }

    public void Reset()
    {
        ResetTasks();
        foreach (Task task in tasks) task.Reset();
        CheckCompletion();
    }

    private void ResetTasks()
    {
        _tasksLeft = tasks.Length;
    }


    public void SetGroupManager(Level level)
	{
        taskGroupManager = level;

        foreach (Task task in tasks) task.SetTaskManager(this);

    }

    public void TaskCompleted(Task levelTask)
    {
        _tasksLeft--;
        onTaskComplete.Invoke(levelTask);

        CheckCompletion();
    }

    private void CheckCompletion()
	{
        if (_tasksLeft == 0) CompleteTasks();

    }
    private void CompleteTasks()
	{
        taskGroupManager.TaskGroupCompleted(this);
        onTaskGroupComplete.Invoke();
    }
}