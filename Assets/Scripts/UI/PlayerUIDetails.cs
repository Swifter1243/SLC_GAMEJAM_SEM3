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
    }

    private void OnPlayerDeath()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = UISingleton.playerScreenPosition;
    }
}
