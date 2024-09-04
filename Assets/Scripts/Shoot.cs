using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [Header("最大射击力度")] public float maxShootPower = 2f;
    [Header("最小射击力度")] public float minShootPower = 0.2f;
    [Header("射击力度")] public float shootPower = 1f;
    [Header("射击力度变化步长")] public float shootPowerInterval = 0.2f;
    [Header("子弹的总数")] public int totalBulletCount = 3;
    [Header("子弹已射击次数")] public int currentBulletShots = 0;
    private bool _isCanShoot = true;


    private void Update()
    {
        AdjustShootDir();
        AdjustShootPower();
        OnShoot();
    }

    private void AdjustShootDir()
    {
        var zAngle = Input.GetAxis("Vertical") * Time.deltaTime * 20f;
        firePoint.parent.Rotate(0f, 0f, zAngle);
    }

    private void AdjustShootPower()
    {
        var scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue != 0)
        {
            // scrollValue为 -0.1f 或者 0.1f 
            shootPower += (10 * scrollValue) * shootPowerInterval;
            shootPower = Mathf.Clamp(shootPower, minShootPower, maxShootPower);
        }
    }

    private void OnShoot()
    {
        // 点击鼠标左键
        if (!Input.GetMouseButtonDown(0)) return;

        // 检查子弹是否用完 
        if (currentBulletShots >= totalBulletCount)
        {
            Debug.Log("你的子弹用完了");
            return;
        }

        // 检查是否可以发射
        if (!_isCanShoot)
        {
            Debug.Log("子弹冷却中");
            return;
        }

        // 发射子弹
        ShootBullets();
    }

    private void ShootBullets()
    {
        // 禁止发射
        _isCanShoot = false;
        // 更新发射次数
        currentBulletShots += 1;
        // 发射子弹
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }


    public void SetCanShoot(bool isCanShoot)
    {
        _isCanShoot = isCanShoot;
    }
}