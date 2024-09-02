using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker_ChangeDirection : MonoBehaviour
{
    public Vector2 direction;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PinballReflect>().moveDir = 
            direction.normalized * other.GetComponent<PinballReflect>().moveDir.magnitude;
    }
}
