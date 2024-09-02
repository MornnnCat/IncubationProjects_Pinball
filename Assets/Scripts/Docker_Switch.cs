using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker_Switch : MonoBehaviour
{
    public GameObject[] switchObjects;
    //private bool triggerLock = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (triggerLock) return;

        foreach (GameObject switchObject in switchObjects)
        {
            switchObject.SetActive(!switchObject.activeSelf);
        }
    }
}
