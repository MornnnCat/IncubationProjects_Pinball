using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker_Treasure : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Get Treasure!");
        gameObject.SetActive(false);
    }
}
