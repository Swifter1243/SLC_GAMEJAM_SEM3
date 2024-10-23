using UnityEngine;
using UnityEngine.Serialization;

public abstract class LevelTask : MonoBehaviour
{
    public bool isCompleted = false;
    protected LevelManager levelManager;

    protected void CompleteTask()
    {
        isCompleted = true;
        levelManager.TaskCompleted(this);
    }

    public void SetLevelManager(LevelManager lm)
    {
        levelManager = lm;
    }

    public void Reset()
    {
        isCompleted = false;
        _Reset();
    }

    protected abstract void _Reset();
}