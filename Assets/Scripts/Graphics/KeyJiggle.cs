using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyJiggle : MonoBehaviour
{
    private const float JIGGLE_FREQ_BASE = 2f;
    private const float JIGGLE_FREQ_SCALE = 1f;
    private const float JIGGLE_MAGNITUDE_BASE = 10f;
    private const float JIGGLE_MAGNITUDE_SCALE = 30f;

    private float phaseX, phaseY, phaseZ;
    private float scaleX, scaleY, scaleZ;
    private float magnitudeX, magnitudeY, magnitudeZ;

    private Quaternion initialRotation;

    void Start()
    {
        phaseX = Random.value * Mathf.PI;
        phaseY = Random.value * Mathf.PI;
        phaseZ = Random.value * Mathf.PI;

        scaleX = JIGGLE_FREQ_BASE + Random.value * JIGGLE_FREQ_SCALE;
        scaleY = JIGGLE_FREQ_BASE + Random.value * JIGGLE_FREQ_SCALE;
        scaleZ = JIGGLE_FREQ_BASE + Random.value * JIGGLE_FREQ_SCALE;

        magnitudeX = JIGGLE_MAGNITUDE_BASE + Random.value * JIGGLE_MAGNITUDE_SCALE;
        magnitudeY = JIGGLE_MAGNITUDE_BASE + Random.value * JIGGLE_MAGNITUDE_SCALE;
        magnitudeZ = JIGGLE_MAGNITUDE_BASE + Random.value * JIGGLE_MAGNITUDE_SCALE;

        initialRotation = transform.localRotation;
    }
    void Update()
    {
        transform.localRotation = initialRotation * Quaternion.Euler(
            magnitudeX * Mathf.Sin(phaseX + scaleX * Time.time),
            magnitudeY * Mathf.Sin(phaseY + scaleY * Time.time),
            magnitudeZ * Mathf.Sin(phaseZ + scaleZ * Time.time)
            );
    }
}
