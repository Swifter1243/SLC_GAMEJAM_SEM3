using System;
using UnityEngine;
using UnityEngine.Events;

public class TaskGroup : MonoBehaviour, IResettable
{
    public Task[] tasks;
    public UnityEvent onTaskGroupComplete;
    private Level taskGroupManager;


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

    public void Reset()
    {
        ResetTasks();
        CheckCompletion();
    }

    private void ResetTasks()
    {
        _tasksLeft = tasks.Length;
    }


    public void SetGroupManager(Level level)
	{
        taskGroupManager = level;
	}

    public void TaskCompleted(Task levelTask)
    {
        _tasksLeft--;

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