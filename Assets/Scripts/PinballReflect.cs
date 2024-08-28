using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballReflect : MonoBehaviour
{
    public Vector2 moveDir;
    [Range(0.999f,1.0f)]public float drag = 1.0f;
    public bool isMoving = true;
    private int hitCount = 0;
    private void Update()
    {
        if (isMoving) Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("111");
        moveDir = Vector2.Reflect(moveDir, collision.contacts[0].normal);
        hitCount++;
        if (hitCount > 30)
        {
            Stop();
        }
    }

    private void Move()
    {
        moveDir *= drag;
        if (Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y) < 0.1f)
        {
            Stop();
        }
        transform.Translate(moveDir * Time.deltaTime); // Move the ball in the direction of the normalized vector
    }
    
    private void Stop()
    {
        moveDir = Vector2.zero;
        isMoving = false;
    }
}
