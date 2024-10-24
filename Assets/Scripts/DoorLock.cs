using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorLock : MonoBehaviour
{
    public float openTime = 1;
    public float lockTime = 1;
    public float shakeStrength = 1;
    public float lockStartScale = 1.3f;
    public float lockRotationRange = 30f;

    private Vector3 _initialPosition;
    private float _animationElapsed = 0;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetInitialPosition(Vector3 position)
    {
        _initialPosition = position;
        transform.localPosition = _initialPosition;
    }

    public void Open()
    {
        StartCoroutine(OpenCoroutine());
    }

    public void Close()
    {
        gameObject.SetActive(true);
        StartCoroutine(CloseCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        _animationElapsed = 0;

        while (true)
        {
            _animationElapsed += Time.deltaTime;
            
            float t = _animationElapsed / openTime;
            t = Ease.OutExpo(t);
            transform.localScale = Vector3.Lerp(new Vector3(1,1,1), new Vector3(0,1,1), t);
            transform.localPosition = Vector3.Lerp(_initialPosition, _initialPosition + new Vector3(-0.5f, 0, 0),t);

            if (_animationElapsed >= openTime)
            {
                gameObject.SetActive(false);
                break;
            }
            
            yield return null;
        }
    }
    
    private IEnumerator CloseCoroutine()
    {
        _animationElapsed = 0;

        while (true)
        {
            _animationElapsed += Time.deltaTime;
            
            float t = _animationElapsed / lockTime;

            float randomRange = (1 - t) * shakeStrength;
            Vector3 randomPos = new Vector3(
                Random.Range(-randomRange, randomRange), 
                Random.Range(-randomRange, randomRange), 
                0);
            randomRange = (1 - t) * lockRotationRange;
            Vector3 randomRot = new Vector3(0, 0, Random.Range(-randomRange, randomRange));
            transform.localPosition = _initialPosition + randomPos;
            transform.localRotation = Quaternion.Euler(randomRot);

            float s = Mathf.Lerp(lockStartScale, 1, t);
            transform.localScale = new Vector3(s, s, 1);

            if (_animationElapsed >= lockTime)
            {
                transform.localPosition = _initialPosition;
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;
                break;
            }
            
            yield return null;
        }
    }
}