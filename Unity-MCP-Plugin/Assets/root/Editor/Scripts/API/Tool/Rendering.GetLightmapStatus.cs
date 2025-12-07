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

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_GetLightmapStatus",
            Title = "Get Lightmap Status"
        )]
        [Description("Gets the current status of lightmap baking (e.g., progress, isRunning).")]
        public string GetLightmapStatus()
        => MainThread.Instance.Run(() =>
        {
            var isRunning = Lightmapping.isRunning;
            var buildProgress = Lightmapping.buildProgress;
            var lightmaps = LightmapSettings.lightmaps;
            var lightmapCount = lightmaps?.Length ?? 0;

            var sb = new StringBuilder();
            sb.AppendLine("[Success] Retrieved lightmap status.");
            sb.AppendLine();
            sb.AppendLine("# Baking Status");
            sb.AppendLine($"- isRunning: {isRunning}");
            sb.AppendLine($"- buildProgress: {buildProgress:P1}");
            sb.AppendLine($"- lightmapCount: {lightmapCount}");

            return sb.ToString();
        });
    }
}
