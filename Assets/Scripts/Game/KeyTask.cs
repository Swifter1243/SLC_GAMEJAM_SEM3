using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTask : Task
{
    private SpriteRenderer[] _spriteRenderers;
    
    private void Awake() //Should be as early as possible or serialized beforehand
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Constants.LAYER_PLAYER)
        {
            foreach (SpriteRenderer sprite in _spriteRenderers) sprite.enabled = false;
            CompleteTask(); //FIXME: doesn't this mean that this task can be completed twice?
        }
    }

    protected override void _Reset()
    {
        foreach (SpriteRenderer sprite in _spriteRenderers) sprite.enabled = true;
    }
}
