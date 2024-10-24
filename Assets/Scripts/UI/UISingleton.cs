using UnityEngine;
using UnityEngine.Events;

public static class UISingleton
{
    private static int _bullets;
    public static int Bullets
    {
        get => _bullets;
        set { _bullets = value; OnAmmoChanged.Invoke(); }
    }

    public static readonly UnityEvent OnAmmoChanged = new();
    public static readonly UnityEvent OnPlayerSpawned = new();
    public static readonly UnityEvent OnPlayerDeath = new();

    public static Vector2 playerScreenPosition;
}
