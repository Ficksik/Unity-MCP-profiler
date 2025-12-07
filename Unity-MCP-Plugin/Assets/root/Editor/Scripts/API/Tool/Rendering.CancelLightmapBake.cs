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

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_CancelLightmapBake",
            Title = "Cancel Lightmap Bake"
        )]
        [Description("Cancels an ongoing lightmap bake.")]
        public string CancelLightmapBake()
        => MainThread.Instance.Run(() =>
        {
            try
            {
                Lightmapping.Cancel();
                return "[Success] Lightmap baking cancelled.";
            }
            catch (Exception ex)
            {
                return Error.FailedOperation("cancel lightmap baking", ex.Message);
            }
        });
    }
}
