using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCursor : MonoBehaviour
{
    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, GetAngle());
    }

    public float GetAngle()
    {
        Vector2 toCursor = GetVectorToCursor();
        return Mathf.Atan2(toCursor.y, toCursor.x) * Mathf.Rad2Deg;
    }

    public Vector2 GetVectorToCursor()
    {
        Vector3 cursorWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 toCursor = cursorWorld - transform.position;
        
        return toCursor;
    }
}
