using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineMaskPass : ScriptableRenderPass
{
    private readonly string profilerTag = "Outline Mask Pass";
    private FilteringSettings filteringSettings;
    private ShaderTagId shaderTag = new ShaderTagId("UniversalForward");
    private Material overrideMaterial;
    private RenderTargetIdentifier maskRT;

    public OutlineMaskPass(LayerMask layerMask, Material mat, RenderTargetIdentifier targetRT)
    {
        filteringSettings = new FilteringSettings(RenderQueueRange.opaque, layerMask);
        overrideMaterial = mat;
        maskRT = targetRT;
        renderPassEvent = RenderPassEvent.AfterRenderingPrePasses;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

        // Write into the mask RT
        cmd.SetRenderTarget(maskRT);
        cmd.ClearRenderTarget(true, true, Color.black);

        var drawSettings = CreateDrawingSettings(shaderTag, ref renderingData, SortingCriteria.CommonOpaque);
        drawSettings.overrideMaterial = overrideMaterial;

        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();

        context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filteringSettings);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
