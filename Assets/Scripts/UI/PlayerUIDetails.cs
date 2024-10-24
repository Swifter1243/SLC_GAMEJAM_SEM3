using System;
using UnityEngine;

public class PlayerUIDetails : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
        UISingleton.OnPlayerDeath.AddListener(OnPlayerDeath);
        UISingleton.OnPlayerSpawned.AddListener(OnPlayerSpawned);
    }

    private void OnPlayerSpawned()
    {
        gameObject.SetActive(true);
        UpdateScreenPosition();
    }

    private void OnPlayerDeath()
    {
        gameObject.SetActive(false);
        UpdateScreenPosition();
    }

    private void Update()
    {
        UpdateScreenPosition();
    }

    private void UpdateScreenPosition()
    {
        transform.position = UISingleton.playerScreenPosition;
    }
}
