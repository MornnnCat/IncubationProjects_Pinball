using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Laser : MonoBehaviour
{
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public PinballReflect pinballReflect;
    public ComputeFog computeFog;

    [Header("哪些层不会被射线检测")] public LayerMask laserMask;

    private readonly List<Vector2> _laserPointList = new();
    private int maxHitCount;
    private Vector2 offset = Vector2.zero;

    private void Start()
    {
        maxHitCount = pinballReflect.GetHitCountToStop();
    }

    private void Update()
    {
        CreateLaser();
        UpdateLineRenderer();
    }

    [HideInInspector] public Vector2[] hitPoints;
    private void CreateLaser()
    {
        _laserPointList.Clear();
        Vector2 startPoint = firePoint.position;
        Vector2 direction = firePoint.up;

        _laserPointList.Add(startPoint);

        hitPoints = new Vector2[maxHitCount];
        for (int i = 0; i < maxHitCount; i++)
        {
            var hitInfo = Physics2D.Raycast(startPoint, direction, Mathf.Infinity, ~ laserMask);
            if (!hitInfo.collider/* || hitInfo.collider.CompareTag("ChangePos")*/) break;
            hitPoints[i] = hitInfo.point;
            direction = Vector2.Reflect(direction, hitInfo.normal);
            startPoint = hitInfo.point + direction * 0.01f;
        }

        int reflections = 0;
        foreach (Vector2 h in hitPoints)
        {
            if (computeFog.IsInFog(h)) break;
            _laserPointList.Add(h);
        }
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