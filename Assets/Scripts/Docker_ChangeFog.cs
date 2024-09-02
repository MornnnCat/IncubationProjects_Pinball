using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Docker_ChangeFog : MonoBehaviour
{
    public float size = 1.5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponentsInChildren<Transform>()[1].localScale *= size;
    }
}
