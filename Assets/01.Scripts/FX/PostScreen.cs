using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEditor;

public class PostScreen : ScriptableRendererFeature
{

    class PostPass : ScriptableRenderPass
    {
        private Settings defaultSettings;
        private Material material;

        private RenderTextureDescriptor rtDesc;
        private RTHandle rtHandle;

        private readonly int colorAID = Shader.PropertyToID("_color_a");
        private readonly int colorBID = Shader.PropertyToID("_color_b");

        public PostPass(Material material,Settings settings)
        {
            this.material = material;
            this.defaultSettings = settings;

            rtDesc = new RenderTextureDescriptor(Screen.width, Screen.height, RenderTextureFormat.Default, 0);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();

            RTHandle cameraTargetHandle =
                renderingData.cameraData.renderer.cameraColorTargetHandle;

            UpdateSettings();

            Blit(cmd, cameraTargetHandle, rtHandle, material, 0);
            Blit(cmd, rtHandle, cameraTargetHandle, material, 1);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public void Dispose()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                Object.Destroy(material);
            }
            else
            {
                Object.DestroyImmediate(material);
            }
#else
            Object.Destroy(material);
#endif

            if (rtHandle != null) rtHandle.Release();
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            rtDesc.width = cameraTextureDescriptor.width;
            rtDesc.height = cameraTextureDescriptor.height;

            RenderingUtils.ReAllocateIfNeeded(ref rtHandle, rtDesc);
        }

        private void UpdateSettings()
        {
            if (material == null) return;

            material.SetColor(colorAID, defaultSettings.colorA);
            material.SetColor(colorBID, defaultSettings.colorB);
        }
    }

    [System.Serializable]
    public class Settings
    {
        public Color colorA;
        public Color colorB;
    }

    public Settings settings = new Settings();
    [SerializeField] private Shader shader;
    private Material material;
    private PostPass postPass;

    public override void Create()
    {
        if (shader == null) return;
        material = new Material(shader);

        postPass = new PostPass(material, settings);
        postPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(postPass);
    }
}