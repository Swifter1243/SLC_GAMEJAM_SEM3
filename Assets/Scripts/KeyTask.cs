using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTask : LevelTask
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // TODO: do this properly with layers
        {
            CompleteTask();
        }
    }
}
