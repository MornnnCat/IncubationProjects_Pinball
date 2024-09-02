using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker_ChangePosition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Transform>().position = GetComponentsInChildren<Transform>()[1].position;
    }
}
