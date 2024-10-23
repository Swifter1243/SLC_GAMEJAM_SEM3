using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Gun gun;
    
    public float speed;
    public float shootForce;

    private void Start()
    {
        gun.onFire.AddListener(OnFire);
    }

    private void OnFire()
    {
        Vector2 toCursor = gun.faceCursor.GetVectorToCursor().normalized;
        rb.velocity -= toCursor * shootForce;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed);
        }
    }
}
