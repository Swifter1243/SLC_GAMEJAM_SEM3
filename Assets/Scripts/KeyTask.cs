using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTask : LevelTask
{
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            _spriteRenderer.enabled = false;
            CompleteTask();
        }
    }

    protected override void _Reset()
    {
        _spriteRenderer.enabled = true;
    }
}
