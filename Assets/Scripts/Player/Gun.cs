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
    public Player player;
    public float shootForce = 10;
    public int bulletsLeft = 1;

    private bool _enabled = true;
    private bool _fired = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _fired = true;
        }
    }

    public void Disable()
    {
        _enabled = false;
    }

    public void Enable()
    {
        _enabled = true;
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
        if (bulletsLeft == 0 || !_enabled)
        {
            return;
        }

        bulletsLeft--;
        UISingleton.Bullets = bulletsLeft;
        onFire.Invoke();

        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Initialize(player);
        Vector2 direction = faceCursor.GetVectorToCursor().normalized;
        bullet.rb.velocity = direction * shootForce;
    }
}
