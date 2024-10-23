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
        foreach (LevelTask levelTask in levelTasks)
        {
            levelTask.SetLevelManager(this);
        }
        
        ResetTasks();
    }

    public void Restart()
    {
        ResetTasks();
    }

    private void ResetTasks()
    {
        _tasksLeft = levelTasks.Length;
        
        foreach (LevelTask levelTask in levelTasks)
        {
            levelTask.Reset();
        }
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