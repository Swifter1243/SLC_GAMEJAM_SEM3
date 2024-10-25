using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayRT : MonoBehaviour
{
    public RenderTexture rt;
    public Camera cam;

    public Vector2Int targetResolution = new Vector2Int(960, 600);

    private int lastWidth;
    private int lastHeight;

	private void Start()
	{
        lastWidth = cam.pixelWidth;
        lastHeight = cam.pixelHeight;

        float ratio = (float)lastWidth / lastHeight;
        if (ratio != (float)rt.width / rt.height) UpdateRT();
    }
	private void OnPreRender()
    {
        if (cam.pixelWidth != lastWidth || cam.pixelHeight != lastHeight)
		{
            UpdateRT();
            lastWidth = cam.pixelWidth;
            lastHeight = cam.pixelHeight;
		}
        cam.targetTexture = rt;
    }
    private void OnPostRender()
    {
        cam.targetTexture = null;
        Graphics.Blit(rt, null as RenderTexture);
    }

    private void UpdateRT()
	{
        if (rt.IsCreated()) rt.Release();

        //float targetRatio = targetResolution.x / targetResolution.y;
        float ratio = (float)cam.pixelWidth / cam.pixelHeight;
        if (ratio >= 0)
        {
            //Expand width first
            rt.height = targetResolution.y;
            rt.width = Mathf.CeilToInt(ratio * targetResolution.y);
        }
        else
        {
            //Expand height first
            rt.width = targetResolution.x;
            rt.height = Mathf.CeilToInt(targetResolution.x / ratio);
        }

        rt.Create();
    }
}
