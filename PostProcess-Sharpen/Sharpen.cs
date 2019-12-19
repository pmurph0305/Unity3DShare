using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(SharpenRenderer), PostProcessEvent.AfterStack, "Custom/Sharpen")]
public sealed class Sharpen : PostProcessEffectSettings
{
    [Range(0f, 5f), Tooltip("Sharpen effect intensity.")]
    public FloatParameter sharpen = new FloatParameter { value = 0.5f };
}

public sealed class SharpenRenderer : PostProcessEffectRenderer<Sharpen>
{
    public override void Render(PostProcessRenderContext context)
    {
        
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Sharpen"));
        sheet.properties.SetFloat("_Sharpness", settings.sharpen);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}