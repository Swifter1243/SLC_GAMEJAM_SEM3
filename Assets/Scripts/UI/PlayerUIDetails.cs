using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUIDetails : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeTime = 0.6f;

    private float _animationTime = 0;

    private void Awake()
    {
        UISingleton.OnPlayerDeath.AddListener(OnPlayerDeath);
        UISingleton.OnPlayerSpawned.AddListener(OnPlayerSpawned);
        StartCoroutine(ShowCoroutine());
    }

    private void OnPlayerSpawned()
    {
        UpdateScreenPosition();
        StopAllCoroutines();
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        _animationTime = 0;

        while (true)
        {
            _animationTime += Time.deltaTime;

            if (_animationTime >= fadeTime)
            {
                canvasGroup.alpha = 1;
                break;
            }

            float t = _animationTime / fadeTime;
            canvasGroup.alpha = t;

            yield return null;
        }
    }

    private void OnPlayerDeath()
    {
        UpdateScreenPosition();
        StopAllCoroutines();
        StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        _animationTime = 0;

        while (true)
        {
            _animationTime += Time.deltaTime;

            if (_animationTime >= fadeTime)
            {
                canvasGroup.alpha = 0;
                break;
            }

            float t = 1 - _animationTime / fadeTime;
            canvasGroup.alpha = t;

            yield return null;
        }
    }

    private void Update()
    {
        UpdateScreenPosition();
    }

    private void UpdateScreenPosition()
    {
        transform.position = UISingleton.playerScreenPosition;
    }
}
