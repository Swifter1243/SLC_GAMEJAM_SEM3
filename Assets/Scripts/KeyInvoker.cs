using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KeyInvoker : MonoBehaviour
{
    public KeyCode keyCode;
    public UnityEvent keyDown;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            keyDown.Invoke();
        }
    }
}
