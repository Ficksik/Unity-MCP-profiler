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
using System.ComponentModel;
using System.Text;
using com.IvanMurzak.McpPlugin;
using com.IvanMurzak.ReflectorNet.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_GetRenderingInfo",
            Title = "Get Rendering Information"
        )]
        [Description("Gets current render pipeline, quality, and graphics settings.")]
        public string GetRenderingInfo()
        => MainThread.Instance.Run(() =>
        {
            var currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
            var currentQualityLevel = QualitySettings.GetQualityLevel();

            var sb = new StringBuilder();
            sb.AppendLine("[Success] Retrieved rendering information.");
            sb.AppendLine();
            sb.AppendLine("# Render Pipeline");
            sb.AppendLine($"- renderPipeline: {(currentRenderPipeline != null ? currentRenderPipeline.name : "Built-in Render Pipeline")}");
            sb.AppendLine($"- renderPipelineAsset: {(currentRenderPipeline != null ? AssetDatabase.GetAssetPath(currentRenderPipeline) : "null")}");
            sb.AppendLine();
            sb.AppendLine("# Graphics Settings");
            sb.AppendLine($"- graphicsTier: {Graphics.activeTier}");
            sb.AppendLine($"- colorSpace: {PlayerSettings.colorSpace}");
            sb.AppendLine($"- hdrEnabled: {PlayerSettings.useHDRDisplay}");
            sb.AppendLine();
            sb.AppendLine("# Quality Settings");
            sb.AppendLine($"- currentQualityLevel: {currentQualityLevel}");
            sb.AppendLine($"- qualityLevelName: {QualitySettings.names[currentQualityLevel]}");

            return sb.ToString();
        });
    }
}
