using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker_Destination : MonoBehaviour
{
    public Canvas passPanelCanvas;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Score();
    }
    
    private void Score()
    {
        // 展示过关面板
        passPanelCanvas.gameObject.SetActive(true);
        // 时间暂停
        Time.timeScale = 0;
        Debug.Log("Score!");
    }
}
