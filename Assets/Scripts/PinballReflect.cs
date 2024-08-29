using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballReflect : MonoBehaviour
{
    public Vector2 moveDir;
    [Range(0.999f, 1.0f)] public float drag = 1.0f;
    public bool isMoving = true;
    private int hitCount = 0;
    [Header("碰到几次碰撞体后停止")] public int hitCountToStop = 3;

    // TODO: 后续碰到迷雾后 一点点减少速度
    [Header("子弹移动速度")] 
    public float pinballMoveSpeed = 10f;

    public ComputeFog computeFog;

    private void Start()
    {
        Debug.Log("2");
        moveDir = GameObject.Find("FirePoint").transform.up * pinballMoveSpeed;
    }

    private void Update()
    {
        if (isMoving) Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveDir = Vector2.Reflect(moveDir, collision.contacts[0].normal);
        hitCount++;
        if (hitCount >= hitCountToStop)
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
        computeFog.LastRoundFogTexture();
    }

    public int GetHitCountToStop() => hitCountToStop;
}