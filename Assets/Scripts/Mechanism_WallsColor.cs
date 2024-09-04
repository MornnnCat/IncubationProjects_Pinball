using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mechanism_WallsColor : MonoBehaviour
{
    [SerializeField] private GameObject wallParent;
    public Color[] wallsColors;
    [SerializeField] private GameObject colorShow;
    private int currentColorIndex;
    public Transform currentColorShow;

    private void Start()
    {
        GameObject[] colorsShowers = new GameObject[wallsColors.Length];
        for (int i = 0; i < wallsColors.Length; i++)
        {
            colorsShowers[i] = Instantiate(colorShow, 
                new Vector3(colorShow.transform.position.x + i,colorShow.transform.position.y,0), Quaternion.identity);
            colorsShowers[i].GetComponent<SpriteRenderer>().color = wallsColors[i];
        }
    }

    public void ChangeWallsColor()
    {
        currentColorIndex = Random.Range(0, wallsColors.Length);
        Color color = wallsColors[currentColorIndex];
        foreach (SpriteRenderer wall in wallParent.GetComponentsInChildren<SpriteRenderer>())
        {
            wall.color = color;
        }
    }

    public bool CompareWallsColor(int colorIndex)
    {
        return colorIndex == currentColorIndex;
    }
}
