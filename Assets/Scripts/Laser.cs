using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public PinballReflect pinballReflect;

    [Header("哪些层不会被射线检测")] public LayerMask laserMask;

    private readonly List<Vector2> _laserPointList = new();

    private void Update()
    {
        CreateLaser();
        UpdateLineRenderer();
    }

    private void CreateLaser()
    {
        _laserPointList.Clear();
        Vector2 startPoint = firePoint.position;
        Vector2 direction = firePoint.up;

        _laserPointList.Add(startPoint);

        int reflections = 0;
        do
        {
            var hitInfo = Physics2D.Raycast(startPoint, direction, Mathf.Infinity, ~ laserMask);
            if (!hitInfo.collider) break;

            _laserPointList.Add(hitInfo.point);
            direction = Vector2.Reflect(direction, hitInfo.normal);
            startPoint = hitInfo.point + direction * 0.01f;

            reflections++;
        } while (reflections < pinballReflect.GetHitCountToStop());
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.positionCount = _laserPointList.Count;
        for (var i = 0; i < _laserPointList.Count; i++)
        {
            lineRenderer.SetPosition(i, _laserPointList[i]);
        }
    }
}