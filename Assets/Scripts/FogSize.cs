using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSize : MonoBehaviour
{

    public Transform fog;
    private Camera _camera;

    [ContextMenu("对齐相机")]
    public void SetSize()
    {
        _camera = Camera.main;
        fog.transform.localScale =
            new Vector2(_camera.orthographicSize * 2f * _camera.aspect, _camera.orthographicSize * 2f);

    }


    [ContextMenu("生成纹理")]
    public void GenerateTexture()
    {
        // 创建一个宽度为128、高度为128的Texture2D对象
        Texture2D texture = new Texture2D(320, 180);
 
        // 填充整个纹理为纯色
        Color[] pixels = new Color[texture.width * texture.height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.black; // 使用红色填充所有像素
        }
        texture.SetPixels(pixels); // 设置纹理的像素
        texture.Apply(); // 应用修改
 
        // 将生成的纹理保存为PNG文件
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Textures/111.png", bytes);
    }
}
