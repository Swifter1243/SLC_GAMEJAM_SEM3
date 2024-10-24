using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public LevelManager levelManager;
    public TMP_Text bulletsText;
    public Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartLevel);
    }

    private void RestartLevel()
    {
        levelManager.GetCurrentLevel().Reset();
    }

    private void Update()
    {
        Level currentLevel = levelManager.GetCurrentLevel();
        float? bulletsLeft = currentLevel.GetBulletsLeft();

        if (bulletsLeft.HasValue)
        {
            bulletsText.text = "Bullets Left: " + bulletsLeft.Value;
        }
        else
        {
            bulletsText.text = "Bullets Left: idk";
        }
    }
}
