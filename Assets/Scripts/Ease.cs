using UnityEngine;

// https://easings.net
public class Ease
{
    public static float InOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    public static float OutExpo(float x)
    {
        return Mathf.Approximately(x, 1.0f) ? 1 : 1 - Mathf.Pow(2, -10 * x);
    }
}