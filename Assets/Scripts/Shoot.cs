using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;


    private void OnShoot()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * (bulletSpeed * -1);
    }

    private void AdjustShootDir()
    {
        var zAngle = Input.GetAxis("Vertical") * Time.deltaTime * 20f;
        firePoint.parent.Rotate(0f, 0f, zAngle);
    }

    private void Update()
    {
        AdjustShootDir();
        OnShoot();
    }
}