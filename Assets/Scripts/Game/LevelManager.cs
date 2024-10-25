using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-200)]
public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public int currentLevelIndex = 0;
    public float spacing = 42;
    public float transitionTime = 2;
    public Camera mainCamera;
    public float outroTime = 4;
    public AudioSource musicSource;

    private Level _lastLevel;
    private Level _currentLevel;
    private float _transitionElapsed = 0;
    private float _lastCameraSize = 0;
    private Vector3 _lastCameraPosition;
    private float _lastMusicSourceVolume;

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
        if (currentLevelIndex == levels.Length - 1)
        {
            StartCoroutine(OutroRoutine());
            return;
        }

        currentLevelIndex++;
        _lastLevel = _currentLevel;
        _currentLevel = CreateLevel(currentLevelIndex);
        _currentLevel.transform.position = new Vector3(spacing, 0, 0);

        StartCoroutine(TransitionUpdate());
    }

    public IEnumerator OutroRoutine()
    {
        _transitionElapsed = 0;
        _lastCameraSize = mainCamera.orthographicSize;
        _lastCameraPosition = mainCamera.transform.position;
        _lastMusicSourceVolume = musicSource.volume;

        while (true)
        {
            _transitionElapsed += Time.deltaTime;

            if (_transitionElapsed >= outroTime)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(2);
                musicSource.volume = 0;
                break;
            }

            float t = _transitionElapsed / outroTime;
            float t2 = Ease.InQuint(t);

            float size = Mathf.Lerp(_lastCameraSize, 0, t2);
            mainCamera.orthographicSize = size;
            Vector3 targetPosition = _currentLevel.door.transform.position + new Vector3(0, 1, 0);
            targetPosition.z = _lastCameraPosition.z;
            Vector3 pos = Vector3.Lerp(_lastCameraPosition, targetPosition, t);
            mainCamera.transform.position = pos;
            musicSource.volume = Mathf.Lerp(_lastMusicSourceVolume, 0, t);

            yield return null;
        }
    }
}
