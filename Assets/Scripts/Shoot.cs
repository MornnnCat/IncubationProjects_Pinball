using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;


    private void OnShoot()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Instantiate(bulletPrefab, firePoint.position,Quaternion.identity);
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