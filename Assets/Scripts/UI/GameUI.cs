using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class UISingleton
{
	static private int bullets;
	static public int Bullets
	{
		get { return bullets; }
		set { bullets = value; OnAmmoChanged.Invoke(); }
	}

	static public UnityEvent OnAmmoChanged { get; private set; } = new UnityEvent();
}



public class GameUI : MonoBehaviour
{
    public LevelManager levelManager;
    public TMP_Text bulletsText;
    public Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartLevel);
        UISingleton.OnAmmoChanged.AddListener(UpdateAmmo);
    }

    private void RestartLevel()
    {
        levelManager.GetCurrentLevel().Reset();
    }
    private void UpdateAmmo()
	{
        bulletsText.text = $"Bullets Left: {UISingleton.Bullets}";
    }

}
