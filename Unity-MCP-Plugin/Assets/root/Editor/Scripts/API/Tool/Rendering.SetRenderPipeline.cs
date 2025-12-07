/*
┌──────────────────────────────────────────────────────────────────┐
│  Author: Ivan Murzak (https://github.com/IvanMurzak)             │
│  Repository: GitHub (https://github.com/IvanMurzak/Unity-MCP)    │
│  Copyright (c) 2025 Ivan Murzak                                  │
│  Licensed under the Apache License, Version 2.0.                 │
│  See the LICENSE file in the project root for more information.  │
└──────────────────────────────────────────────────────────────────┘
*/

#nullable enable
using System;
using System.ComponentModel;
using com.IvanMurzak.McpPlugin;
using com.IvanMurzak.ReflectorNet.Utils;
using UnityEditor;
using UnityEngine.Rendering;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        public enum RenderPipelineType
        {
            builtin,
            urp,
            hdrp
        }

        [McpPluginTool
        (
            "Rendering_SetRenderPipeline",
            Title = "Set Render Pipeline"
        )]
        [Description("Switches the active Render Pipeline Asset. Use 'builtin' for Built-in Render Pipeline, 'urp' for Universal Render Pipeline, or 'hdrp' for HD Render Pipeline.")]
        public string SetRenderPipeline
        (
            [Description("The type of render pipeline ('builtin', 'urp', 'hdrp').")]
            RenderPipelineType pipelineType,
            [Description("The project-relative path to a Render Pipeline Asset. Required for 'urp' or 'hdrp'.")]
            string? assetPath = null
        )
        => MainThread.Instance.Run(() =>
        {
            try
            {
                switch (pipelineType)
                {
                    case RenderPipelineType.builtin:
                        GraphicsSettings.renderPipelineAsset = null;
                        return "[Success] Switched to Built-in Render Pipeline.";

                    case RenderPipelineType.urp:
                        if (string.IsNullOrEmpty(assetPath))
                            return Error.AssetPathRequiredForPipeline("urp");

                        var urpAsset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>(assetPath);
                        if (urpAsset == null)
                            return Error.CouldNotLoadPipelineAsset(assetPath);

                        GraphicsSettings.renderPipelineAsset = urpAsset;
                        return $"[Success] Switched to Universal Render Pipeline using asset: '{assetPath}'.";

                    case RenderPipelineType.hdrp:
                        if (string.IsNullOrEmpty(assetPath))
                            return Error.AssetPathRequiredForPipeline("hdrp");

                        var hdrpAsset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>(assetPath);
                        if (hdrpAsset == null)
                            return Error.CouldNotLoadPipelineAsset(assetPath);

                        GraphicsSettings.renderPipelineAsset = hdrpAsset;
                        return $"[Success] Switched to HD Render Pipeline using asset: '{assetPath}'.";

                    default:
                        return Error.UnknownPipelineType(pipelineType.ToString());
                }
            }
            catch (Exception ex)
            {
                return Error.FailedOperation("set render pipeline", ex.Message);
            }
        });
    }
}
