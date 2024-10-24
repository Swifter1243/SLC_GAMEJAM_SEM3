using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPassagePair : MonoBehaviour, IResettable
{
    public Passage passage;
    public Switch passageSwitch;

    private void Awake()
    {
        passageSwitch.onTurnedOn.AddListener(OnSwitchTurnedOn);
    }

    private void OnSwitchTurnedOn()
    {
        passage.Disable();
    }

    public void Reset()
    {
        passageSwitch.Reset();
        passage.Reset();
    }


}
