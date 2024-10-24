using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public List<Transform> backgroundParallaxObjects = new();

    public float periodX = 1.0f;
    public float periodY = 1.0f;
    public float parallaxAmount = 1.0f;
    public float distance = 0.1f;

    private void Update()
    {
        for (int i = 0; i < backgroundParallaxObjects.Count; i++)
        {
            float dist = parallaxAmount - distance * i;
            float x = Mathf.Sin(Time.time * periodX) * dist;
            float y = Mathf.Cos(Time.time * periodY) * dist;
            backgroundParallaxObjects[i].position = new Vector3(x, y, 0);
        }
    }
}
