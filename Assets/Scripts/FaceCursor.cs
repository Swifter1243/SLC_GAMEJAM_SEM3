using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCursor : MonoBehaviour
{
    private Camera _camera;

    // Update is called once per frame
    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (_camera)
        {
            Vector2 screen = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            float angle = Mathf.Atan2(screen.y, screen.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
