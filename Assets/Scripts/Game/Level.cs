using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour, IResettable
{
    public TaskGroup[] taskGroups;

    public UnityEngine.Object resetObjects;
    //private IResettable[] resetObjects;
    public UnityEvent onLevelComplete;

    private int _taskGroupsLeft;

    private void Start()
    {
        //list.Where(t => t is IResettable)



        ResetTaskGroups();
    }

    public void Reset()
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
