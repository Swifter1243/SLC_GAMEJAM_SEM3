using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public int currentLevelIndex = 0;
    public float spacing = 42;
    public float transitionTime = 2;
    public Camera mainCamera;

    private Level _lastLevel;
    private Level _currentLevel;
    private float _transitionElapsed = 0;

    private void Start()
    {
        Level level = CreateLevel(currentLevelIndex);
        _lastLevel = level;
        _currentLevel = level;
        level.StartGameplay();
    }

    private Level CreateLevel(int levelIndex)
    {
        Level newLevel = Instantiate(levels[levelIndex]);
        newLevel.onLevelComplete.AddListener(NextLevel);
        return newLevel;
    }

    private IEnumerator TransitionUpdate()
    {
        _transitionElapsed = 0;

        while (true)
        {
            _transitionElapsed += Time.deltaTime;
            float t = _transitionElapsed / transitionTime;
            Vector2 a = _lastLevel.transform.position;
            Vector2 b = _currentLevel.transform.position;
            Vector2 c = Vector2.Lerp(a, b, t);
            mainCamera.transform.position = new Vector3(c.x, c.y, mainCamera.transform.position.z);

            if (_transitionElapsed >= transitionTime)
            {
                mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
                _currentLevel.transform.position = new Vector2(0, 0);
                _currentLevel.StartGameplay();
                Destroy(_lastLevel.gameObject);
                break;
            }

            yield return null;
        }
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        _lastLevel = _currentLevel;
        _currentLevel = CreateLevel(currentLevelIndex);
        _currentLevel.transform.position = new Vector3(spacing, 0, 0);

        StartCoroutine(TransitionUpdate());
    }
}
