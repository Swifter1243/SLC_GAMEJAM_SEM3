using UnityEngine;
using UnityEngine.Serialization;

public abstract class LevelTask : MonoBehaviour
{
    public bool isCompleted = false;
    public LevelManager levelManager;

    protected void CompleteTask()
    {
        isCompleted = true;
        levelManager.TaskCompleted(this);
    }
}