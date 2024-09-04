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

    // TODO: 后续碰到迷雾后 一点点减少速度
    [Header("子弹移动速度")]
    public float pinballMoveSpeed = 10f;

    private ComputeFog computeFog;
    private GameObject scriptsHolder;
    //private Mechanism_WallsColor changeWallsColor;
    //private Transform viewRange;
    //private SpriteRenderer pinBallSprite;
    
    private void Start()
    {
        moveDir = GameObject.Find("FirePoint").transform.up.normalized;
        scriptsHolder = GameObject.Find("ScriptsHolder");
        computeFog = scriptsHolder.GetComponent<ComputeFog>();
        //changeWallsColor = scriptsHolder.GetComponent<Mechanism_WallsColor>();
        //viewRange = GetComponentsInChildren<Transform>()[1];
        //changeWallsColor.ChangeWallsColor();

        //pinBallSprite = GetComponent<SpriteRenderer>();
        //pinBallSprite.color = changeWallsColor.wallsColors[0];
    }

    private void FixedUpdate()
    {
        if (isMoving) Move();
        
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangePinballColor(false);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangePinballColor(true);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D other)
    {
        //wall color compare
        /*if (other.gameObject.CompareTag("Wall"))
        {
            if (changeWallsColor.CompareWallsColor(currentColorIndex))
                CompareColor_Award();
            else
                CompareColor_Punishment();
        }*/
        
        hitCount++;
        if (hitCount >= hitCountToStop)
            Stop();
        else
            moveDir = Vector2.Reflect(moveDir, other.contacts[0].normal);
        
    }

    /*private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
            changeWallsColor.ChangeWallsColor();
    }*/

    //颜色比较奖惩机制
    /*private int sameColorCount = 0;//连击数
    private int currentColorIndex = 0;//当前颜色索引
    private void CompareColor_Award()
    {
        viewRange.localScale *= 1.125f; 
        moveDir *= 1.125f;
        sameColorCount++;
    }
    private void CompareColor_Punishment()
    {
        viewRange.localScale *= 0.825f;
        moveDir *= 0.825f;
        sameColorCount = 0;
    }
    //切换颜色
    private void ChangePinballColor(bool isRight)
    {
        currentColorIndex = isRight? 
            (currentColorIndex + 1) % changeWallsColor.wallsColors.Length : 
            (currentColorIndex - 1 + changeWallsColor.wallsColors.Length)% changeWallsColor.wallsColors.Length;
        changeWallsColor.currentColorShow.position = new Vector3(-16.68f + currentColorIndex, changeWallsColor.currentColorShow.position.y, 0);
        pinBallSprite.color = changeWallsColor.wallsColors[currentColorIndex];
    }*/
    
    
    //移动机制
    private void Move()
    {
        drag = computeFog.IsInFog(transform.position) ? 0.993f : 0.998f;
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
    }

    public int GetHitCountToStop() => hitCountToStop;
}