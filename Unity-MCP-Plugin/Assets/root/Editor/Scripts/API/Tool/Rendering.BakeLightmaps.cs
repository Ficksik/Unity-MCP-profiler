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
using System.Text;
using com.IvanMurzak.McpPlugin;
using com.IvanMurzak.ReflectorNet.Utils;
using UnityEditor;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_BakeLightmaps",
            Title = "Bake Lightmaps"
        )]
        [Description("Starts the asynchronous lightmap baking process. Use 'Rendering_GetLightmapStatus' to check progress.")]
        public string BakeLightmaps
        (
            [Description("If true, clears the GI cache before baking. Defaults to false.")]
            bool clearCache = false
        )
        => MainThread.Instance.Run(() =>
        {
            try
            {
                if (clearCache)
                {
                    Lightmapping.Clear();
                    Lightmapping.ClearDiskCache();
                }

                if (!Lightmapping.BakeAsync())
                    return Error.FailedToStartLightmapBaking();

                var sb = new StringBuilder();
                sb.AppendLine("[Success] Lightmap baking started.");
                sb.AppendLine($"- status: baking");
                sb.AppendLine($"- clearedCache: {clearCache}");
                sb.AppendLine();
                sb.AppendLine("Use 'Rendering_GetLightmapStatus' to check baking progress.");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return Error.FailedOperation("bake lightmaps", ex.Message);
            }
        });
    }
}
