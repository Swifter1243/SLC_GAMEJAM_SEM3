using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    public FaceCursor faceCursor;
    public UnityEvent onFire;
    public Bullet bulletPrefab;
    public float shootForce = 10;

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

        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Vector2 direction = faceCursor.GetVectorToCursor();
        bullet.rb.velocity = direction * shootForce;
    }
}
