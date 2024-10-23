using System;
using UnityEngine;

public class TargetTask : Task
{
    public void Hit()
    {
        gameObject.SetActive(false);
        CompleteTask();
    }

    public override void Reset()
    {
        gameObject.SetActive(true);
    }
}
