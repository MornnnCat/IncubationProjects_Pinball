using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSize : MonoBehaviour
{

    public Transform fog;
    private Camera _camera;

    [ContextMenu("对齐相机")]
    public void SetSize()
    {
        _camera = Camera.main;
        fog.transform.localScale =
            new Vector2(_camera.orthographicSize * 2f * _camera.aspect, _camera.orthographicSize * 2f);

    }
}
