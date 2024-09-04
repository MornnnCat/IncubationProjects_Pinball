using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballReflect : MonoBehaviour
{
    public Vector2 moveDir;
    [Range(0.996f, 1.0f)] public float drag = 1.0f;
    public bool isMoving = true;
    
    private int hitCount = 0;
    [Header("碰到几次碰撞体后停止")] public int hitCountToStop = 3;
    [Header("子弹移动速度")] public float pinballMoveSpeed = 10f;

    private ComputeFog computeFog;
    private Shoot shoot;


    private void Start()
    {
        moveDir = GameObject.Find("FirePoint").transform.up.normalized;
        computeFog = GameObject.Find("ScriptsHolder").GetComponent<ComputeFog>();
        // 射击脚本
        shoot = GameObject.Find("Turret").GetComponent<Shoot>();
        moveDir *= shoot.shootPower;
    }

    private void FixedUpdate()
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
        drag = computeFog.IsInFog(transform.position) ? 0.993f : 1.002f;
        /*if (computeFog.IsInFog(transform.position))
        {
            drag = 0.993f;
        }
        else
        {
            drag = 1.0f;
        }*/
        
        moveDir *= drag;
        if (Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y) < 0.1f)
        {
            Stop();
        }

        transform.Translate(moveDir * (pinballMoveSpeed * Time.deltaTime));
    }

    private void Stop()
    {
        moveDir = Vector2.zero;
        isMoving = false;
        computeFog.LastRoundFogTexture();
        Destroy(gameObject);
        // 销毁的时候，重新开启射击功能
        shoot.SetCanShoot(true);
    }

    public int GetHitCountToStop() => hitCountToStop;
}