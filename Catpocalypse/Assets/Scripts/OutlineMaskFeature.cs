using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineMaskFeature : ScriptableRendererFeature
{
    public LayerMask outlineLayer;
    public Material whiteMaterial;
    public RenderTexture maskRT;

    OutlineMaskPass pass;

    public override void Create()
    {
        pass = new OutlineMaskPass(outlineLayer, whiteMaterial, maskRT);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }
}
