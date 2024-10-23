using UnityEngine;
using UnityEngine.Serialization;

public abstract class Task : Resettable
{
    public bool isCompleted = false;
    private TaskGroup _taskGroupManager;

    virtual protected void CompleteTask()
    {
        isCompleted = true;
        _taskGroupManager.TaskCompleted(this);
    }

    public void SetTaskManager(TaskGroup mgr)
    {
        _taskGroupManager = mgr;
    }

    public override void Reset()
    {
        isCompleted = false;
    }
}