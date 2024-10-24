using System;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IResettable
{
    private bool _on = false;
    public Transform leverPivot;
    public UnityEvent onTurnedOn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Constants.LAYER_PLAYER)
        {
            TurnOn();
        }

        if (other.gameObject.layer == Constants.LAYER_BULLET)
        {
            Destroy(other.gameObject);
            TurnOn();
        }
    }

    private void TurnOn()
    {
        if (_on)
        {
            return;
        }

        leverPivot.rotation = Quaternion.Euler(0, 0, -45);
        onTurnedOn.Invoke();
        _on = true;
    }

    public void Reset()
    {
        leverPivot.rotation = Quaternion.Euler(0, 0, 45);
        _on = false;
    }
}
