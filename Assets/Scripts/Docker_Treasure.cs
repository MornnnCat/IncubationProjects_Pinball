using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docker_Treasure : MonoBehaviour
{
    public Text scoreText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        scoreText.text = string.Format("Score : {0}",int.Parse(scoreText.text.Split(' ')[2]) + 1);
        
        gameObject.SetActive(false);
    }
}
