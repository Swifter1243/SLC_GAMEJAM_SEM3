using System;
using UnityEngine;

public class Passage : MonoBehaviour, IResettable
{
    public void Reset()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
