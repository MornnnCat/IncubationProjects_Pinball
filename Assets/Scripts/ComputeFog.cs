using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeFog : MonoBehaviour
{
    private RenderTexture fogTexture;
    public RenderTexture trailTexture;
    public ComputeShader computeFog;
    private int kernal;
    public Material material;
    public Texture2D startFog;
    public RenderTexture lastRoundFogTexture;
    void Start()
    {
        Init();
        LastRoundFogTexture();
    }
    
    void Update()
    {
        ComputeFogTexture();
    }
    private void Init()
    {
        fogTexture = new RenderTexture(320, 180, 32, RenderTextureFormat.ARGB32);
        fogTexture.enableRandomWrite = true;
        fogTexture.Create();
        
        //调用CSMain函数
        kernal = computeFog.FindKernel("CSMain");
        
        //调用InitFog函数
        int initKernal = computeFog.FindKernel("InitFog");
        computeFog.SetTexture(initKernal, "Fog", fogTexture);
        computeFog.SetTexture(initKernal, "StartFog", startFog);
        computeFog.Dispatch(initKernal, 32, 32, 1);
        
        material.SetTexture("_BaseMap", fogTexture);
    }

    private void ComputeFogTexture()
    {
        computeFog.SetTexture(kernal, "Fog", fogTexture);
        computeFog.SetTexture(kernal, "Trail", trailTexture);
        computeFog.Dispatch(kernal, 32, 32, 1);
    }
    
    
    //公共函数
    
    public void LastRoundFogTexture()
    {
        int roundUpdateKernal = computeFog.FindKernel("RoundUpdate");
        computeFog.SetTexture(roundUpdateKernal, "Fog", fogTexture);
        computeFog.SetTexture(roundUpdateKernal, "LastRoundFogTexture", lastRoundFogTexture);
        computeFog.Dispatch(roundUpdateKernal, 32, 32, 1);
    }

    /*public bool IsInFog(Vector2 worldPos)
    {
        lastRoundFogTexture.c
    }*/
}
