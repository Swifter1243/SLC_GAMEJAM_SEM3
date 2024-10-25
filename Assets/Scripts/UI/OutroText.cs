using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutroText : MonoBehaviour
{
    public List<string> lines = new List<string>();
    public CanvasGroup canvasGroup;
    public TMP_Text text;
    public float initialWaitTime = 1;
    public float fadeTime = 1;
    public float waitTime = 2;

    private float _elapsedTime = 0;
    private int _currentLine = 0;

    private void Start()
    {
        text.text = lines[_currentLine];
        canvasGroup.alpha = 0;
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(initialWaitTime);

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        _elapsedTime = 0;

        while (true)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > fadeTime)
            {
                canvasGroup.alpha = 1;

                if (_currentLine < lines.Count - 1)
                {
                    StartCoroutine(Wait());
                }

                break;
            }

            float t = _elapsedTime / fadeTime;
            canvasGroup.alpha = t;

            yield return null;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        _elapsedTime = 0;

        while (true)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > fadeTime)
            {
                canvasGroup.alpha = 0;

                StartCoroutine(FadeIn());
                _currentLine++;
                text.text = lines[_currentLine];

                break;
            }

            float t = _elapsedTime / fadeTime;
            canvasGroup.alpha = 1 - t;

            yield return null;
        }
    }
}
