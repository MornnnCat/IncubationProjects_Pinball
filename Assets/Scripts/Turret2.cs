using System;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public float bulletSpeed = 20f;
    public float minBulletSpeed = 5f;
    public float maxBulletSpeed = 30f;

    public int posCount = 1000;

    // timeStep: 设置时间步长，用于模拟连续时间中的离散更新
    public float timeStep = 0.02f;
    public LayerMask layerMask;
    private List<Vector2> _posList;

    private void CalculatePositions()
    {
        // 初始化位置列表，用于存储计算出的所有位置  
        _posList = new List<Vector2>();

        // 获取发射点的当前位置  
        Vector2 position = firePoint.position;

        // 计算初始速度，这里假设子弹是向上发射的，所以使用firePoint.up并乘以子弹速度（注意负号表示方向相反）  
        Vector2 velocity = firePoint.up * (bulletSpeed * -1);

        // 获取重力加速度的y分量（在Unity的2D物理中，重力通常只在y轴上）  
        float gravity = Physics2D.gravity.y;
        
        float angle = 360 - firePoint.parent.rotation.eulerAngles.x;
        float horizontalSpeed = Mathf.Cos(angle / 180 * Mathf.PI) * bulletSpeed;
        float verticalSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * bulletSpeed;
        Debug.Log("horizontalSpeed==" + horizontalSpeed);
        Debug.Log("verticalSpeed==" + verticalSpeed);

        // 循环posCount次，模拟物体在一段时间内的运动  
        for (int i = 0; i < posCount; i++)
        {
         
            if (i > 1)
            {
                Vector3 dir = _posList[^1] - _posList[^2];
                RaycastHit2D hit = Physics2D.Raycast(_posList[^2], dir, timeStep);
                if (hit.collider != null && hit.collider.CompareTag("Bullet") == false)
                {
                    // _posList[^1] = hit.point;
                    _posList.Add(hit.point);
                    // Debug.Log($"hit={hit.collider.gameObject.name}");
                    break;
                }
            }


            // 计算由于重力影响而在timeStep时间内速度的变化量  
            Vector2 gravityVelocityChange = new Vector2(0f, gravity * timeStep);

            // 更新速度，加上由于重力引起的速度变化  
            velocity += gravityVelocityChange;

            // 使用更新后的速度和timeStep来计算新的位置  
            position += velocity * timeStep;

            // 将计算出的新位置添加到列表中  
            _posList.Add(position);
        }
    }

    private void SetLineRendererPositions()
    {
        if (lineRenderer != null && _posList.Count > 0)
        {
            lineRenderer.positionCount = _posList.Count;
            for (int i = 0; i < _posList.Count; i++)
            {
                lineRenderer.SetPosition(i, _posList[i]);
            }
        }
    }

    private void Shoot()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * (bulletSpeed * -1);
    }

    private void AdjustSpeed()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            bulletSpeed += 5f;
        }
        else if (scroll < 0f)
        {
            bulletSpeed -= 5f;
        }

        bulletSpeed = Mathf.Clamp(bulletSpeed, minBulletSpeed, maxBulletSpeed);
    }

    private void AdjustShootDir()
    {
        float zAngle = Input.GetAxis("Vertical") * Time.deltaTime * 20f;
        firePoint.parent.Rotate(0f, 0f, zAngle);
    }

    private void Update()
    {
        CalculatePositions();
        SetLineRendererPositions();
        AdjustSpeed();
        AdjustShootDir();
        Shoot();
    }
}