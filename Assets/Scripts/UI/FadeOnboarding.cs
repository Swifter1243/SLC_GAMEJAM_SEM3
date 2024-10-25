using System;
using System.Collections;
using UnityEngine;

public class FadeOnboarding : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;

    private float _timeElapsed = 0;

    private void Start()
    {
        StartCoroutine(FadeIn());
        UISingleton.OnLevelFinished.AddListener(StartFadeOut);
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        _timeElapsed = 0;

        while (true)
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed >= fadeTime)
            {
                canvasGroup.alpha = 1;
                break;
            }

            float t = _timeElapsed / fadeTime;
            canvasGroup.alpha = t;

            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        _timeElapsed = 0;

        while (true)
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed >= fadeTime)
            {
                canvasGroup.alpha = 0;
                Destroy(gameObject);
                break;
            }

            float t = _timeElapsed / fadeTime;
            canvasGroup.alpha = 1 - t;

            yield return null;
        }
    }
}
