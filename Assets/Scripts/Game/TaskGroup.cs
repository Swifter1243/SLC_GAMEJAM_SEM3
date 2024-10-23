using System;
using UnityEngine;
using UnityEngine.Events;

public class TaskGroup : MonoBehaviour
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
    }

    public void Restart()
    {
        ResetTasks();
    }

    private void ResetTasks()
    {
        _tasksLeft = tasks.Length;
        
        foreach (Task levelTask in tasks)
        {
            levelTask.Reset();
        }
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