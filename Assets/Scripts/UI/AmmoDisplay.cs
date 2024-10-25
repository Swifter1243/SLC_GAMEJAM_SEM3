using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    public int splits = 2;
    public float inset = 0.1f;
    public RectTransform splitPrefab;
    public RectTransform spacing;
    public RectTransform splitsParent;

    private RectTransform _rt;

    /*private void OnValidate()
    {
        UpdateSplits();
    }*/

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        UpdateSplits();
    }

    private void UpdateSplits()
    {
        foreach (RectTransform rectTransform in splitsParent)
        {
            Destroy(rectTransform.gameObject);
        }

        float totalScale = _rt.sizeDelta.x;
        float a = Mathf.Lerp(-totalScale * 0.5f, totalScale * 0.5f, inset);
        float b = Mathf.Lerp(-totalScale * 0.5f, totalScale * 0.5f, 1 - inset);

        float gap = totalScale * (1 - inset * 2);
        float splitScale = (gap * 2) / (splits - 1);

        for (int i = splits - 2; i >= 0; i--)
        {
            RectTransform split = Instantiate(splitPrefab, splitsParent);
            float t = i / (splits - 1f);

            float x = Mathf.Lerp(a, b, t);
            split.transform.localPosition = new Vector2(x, 0);
            split.sizeDelta = new Vector2(splitScale, 1);
        }
    }
}
