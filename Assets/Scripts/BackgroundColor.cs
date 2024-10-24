using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    [SerializeField] private float fade = 1f;
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        Color frameCol = color;

        frameCol.a = fade * Mathf.Pow(2, -Time.deltaTime);

        spriteRenderer.color = frameCol;
    }
}
