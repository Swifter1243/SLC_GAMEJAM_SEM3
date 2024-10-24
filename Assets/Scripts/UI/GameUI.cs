using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public LevelManager levelManager;
    public TMP_Text bulletsText;

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
