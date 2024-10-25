using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float destroyAfterSeconds = 5;
    private Player _player;
    public GameObject particlePrefab;

    public void Initialize(Player player)
    {
        player.bullets.Add(this);
        Destroy(gameObject, destroyAfterSeconds);
        _player = player;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        _player.bullets.Remove(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Constants.LAYER_TARGET)
        {
            other.gameObject.GetComponent<TargetTask>().Hit();
        }
    }
}
