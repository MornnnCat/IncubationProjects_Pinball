using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker_ChangeSpeed : MonoBehaviour
{
    public float speed = 2.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PinballReflect>().moveDir *= speed;
    }
}
