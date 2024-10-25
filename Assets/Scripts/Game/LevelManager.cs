using System;
using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-200)]
public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public int currentLevelIndex = 0;
    public float spacing = 42;
    public float transitionTime = 2;

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
            t = Ease.InOutCubic(t);

            Vector2 a = new Vector3(-spacing, 0, 0);
            Vector2 b = Vector2.zero;
            Vector2 c = new Vector3(spacing, 0, 0);

            _lastLevel.transform.position = Vector2.Lerp(b, a, t);
            _currentLevel.transform.position = Vector2.Lerp(c, b, t);

            if (_transitionElapsed >= transitionTime)
            {
                _currentLevel.transform.position = new Vector2(0, 0);
                _currentLevel.StartGameplay();
                Destroy(_lastLevel.gameObject);
                break;
            }

            yield return null;
        }
    }

    public Level GetCurrentLevel()
    {
        return _currentLevel;
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
