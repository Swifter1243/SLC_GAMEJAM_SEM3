using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public FaceCursor faceCursor;
    public UnityEvent onFire;

    bool _fired = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _fired = true;
        }
    }


    private void FixedUpdate()
    {
        if (_fired)
        {
            _fired = false;
            Fire();
        }
    }

    private void Fire()
    {
        onFire.Invoke();
    }
}
