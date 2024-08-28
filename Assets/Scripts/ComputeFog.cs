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
    void Start()
    {
        fogTexture = new RenderTexture(256, 256, 32, RenderTextureFormat.ARGB32);
        fogTexture.enableRandomWrite = true;
        fogTexture.Create();
        kernal = computeFog.FindKernel("CSMain");
        
        
        material.SetTexture("_BaseMap", fogTexture);
    }
    
    void Update()
    {
        computeFog.SetTexture(kernal, "Fog", fogTexture);
        computeFog.SetTexture(kernal, "Trail", trailTexture);
        computeFog.Dispatch(kernal, 32, 32, 1);
    }
}
