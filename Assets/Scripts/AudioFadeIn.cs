using System;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour
{
    public float fadeDuration;
    public AudioSource audioSource;

    private float _time;
    private float _originalVolume;

    private void Awake()
    {
        _originalVolume = audioSource.volume;
        audioSource.volume = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        float t = Mathf.Clamp01(_time / fadeDuration);

        audioSource.volume = Mathf.Lerp(0, _originalVolume, t);
    }
}
