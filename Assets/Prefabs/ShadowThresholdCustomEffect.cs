using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowThresholdCustomEffect : MonoBehaviour
{
    public Material shadowMaterial;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, shadowMaterial);
    }
}
