using System.Collections.Generic;
using UnityEngine;

public class CameraRenderer : MonoBehaviour
{
    RenderTexture grainRT;

    [Range(0f, 20f)]
    public float intensity = 10f;

    [Range(1, 1000)]
    public int width = 600;

    [Range(1, 1000)]
    public int height = 600;

    [Range(1, 100)]
    public int interval = 1;

    private int frame = 1;

    void OnRenderImage(RenderTexture src, RenderTexture des)
    {
        if ((frame++) % interval != 0)
        {
            return;
        }
        frame = 1;

        if (grainRT == null || !grainRT.IsCreated() || grainRT.width != width || grainRT.height != height)
        {
            Destroy(grainRT);
            grainRT = new RenderTexture(width, height, 0);
            grainRT.Create();
        }

        Material grainMat = GetMaterial("Custom/Grain");
        grainMat.SetFloat(Shader.PropertyToID("_Random"), Random.value);
        Graphics.Blit(grainRT, grainRT, grainMat);

        Material outputMat = GetMaterial("Custom/Output");
        outputMat.SetTexture(Shader.PropertyToID("_GrainTex"), grainRT);
        outputMat.SetFloat(Shader.PropertyToID("_Intensity"), intensity);
        Graphics.Blit(src, des, outputMat, 0);
    }

    private Dictionary<string, Material> matCache = new Dictionary<string, Material>();

    Material GetMaterial(string shaderName)
    {
        Material mat;
        if (!matCache.TryGetValue(shaderName, out mat))
        {
            Shader shd = Shader.Find(shaderName);
            mat = new Material(shd);
            matCache.Add(shaderName, mat);
        }
        return mat;
    }
}
