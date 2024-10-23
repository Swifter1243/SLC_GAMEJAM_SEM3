using UnityEngine;
using UnityEngine.Serialization;

public abstract class LevelTask : MonoBehaviour
{
    public bool isCompleted = false;
    private LevelManager _levelManager;

    protected void CompleteTask()
    {
        isCompleted = true;
        _levelManager.TaskCompleted(this);
    }

    public void SetLevelManager(LevelManager lm)
    {
        _levelManager = lm;
    }

    public void Reset()
    {
        isCompleted = false;
        _Reset();
    }

    protected abstract void _Reset();
}