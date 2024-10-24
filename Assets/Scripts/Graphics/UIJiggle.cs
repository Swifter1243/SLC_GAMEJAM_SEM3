using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIJiggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float amplitude = 1;
    public float period = 1;
    public float jiggleTime = 1;
    public float jiggleEdgePrevention = 0.3f;

    private float _jiggleTimeElapsed = 0;
    private float _lastJiggleTime = 0;
    private Coroutine _coroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Jiggle();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Jiggle();
    }

    private void Jiggle()
    {
        float timeSinceLastJiggleTime = Time.time - _lastJiggleTime;

        if (timeSinceLastJiggleTime > jiggleEdgePrevention)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _lastJiggleTime = Time.time;
            _coroutine = StartCoroutine(JiggleCoroutine());
        }
    }

    private IEnumerator JiggleCoroutine()
    {
        _jiggleTimeElapsed = 0;

        while (true)
        {
            _jiggleTimeElapsed += Time.deltaTime;

            if (_jiggleTimeElapsed >= jiggleTime)
            {
                transform.localScale = Vector3.one;
                break;
            }

            float t = 1 - _jiggleTimeElapsed / jiggleTime;
            float s = 1 + Mathf.Sin(_jiggleTimeElapsed * period) * amplitude * t;
            transform.localScale = new Vector3(s, s, s);

            yield return null;
        }
    }
}
