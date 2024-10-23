using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public LevelTask[] levelTasks;
    public UnityEvent onTasksComplete;

    private int _tasksLeft;
    
    private void Start()
    {
        _tasksLeft = levelTasks.Length;
    }

    public void TaskCompleted(LevelTask levelTask)
    {
        _tasksLeft--;

        if (_tasksLeft == 0)
        {
            onTasksComplete.Invoke();
        }
    }
}