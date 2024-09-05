using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurretInfoCanvas : MonoBehaviour
{
    public Slider powerInfoSlider;
    public Text powerInfoValue;

    public Text bulletProportion;

    // 脚本
    private Shoot _shoot;


    private void Start()
    {
        _shoot = GameObject.Find("Turret").GetComponent<Shoot>();
    }

    private void UpdatePowerInfo()
    {
        powerInfoSlider.value = _shoot.shootPower / (_shoot.maxShootPower - _shoot.minShootPower);
        powerInfoValue.text = _shoot.shootPower + "";
    }
    
    private void UpdateBulletInfo()
    {
        bulletProportion.text = _shoot.currentBulletShots  + " / " + _shoot.totalBulletCount;
    }

    private void Update()
    {
        UpdatePowerInfo();
        UpdateBulletInfo();
    }
    public void ReTry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}