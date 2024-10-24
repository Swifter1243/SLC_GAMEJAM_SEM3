using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRT : MonoBehaviour
{
    public RenderTexture rt;
    public Camera cam;
    void OnPreRender()
    {
        cam.targetTexture = rt;
    }

    private void OnPostRender()
    {
        cam.targetTexture = null;
        Graphics.Blit(rt, null as RenderTexture);
    }
}
