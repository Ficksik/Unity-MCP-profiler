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
using System.Collections.Generic;
using System.ComponentModel;
using com.IvanMurzak.McpPlugin;
using com.IvanMurzak.ReflectorNet.Utils;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_ConfigureFog",
            Title = "Configure Fog"
        )]
        [Description(@"Configures global fog settings.
Available fog modes: 'Linear', 'Exponential', 'ExponentialSquared'.")]
        public string ConfigureFog
        (
            [Description("Enable/disable fog.")]
            bool? enabled = null,
            [Description("Fog mode: 'Linear', 'Exponential', 'ExponentialSquared'.")]
            FogMode? mode = null,
            [Description("Hex color string for fog color (e.g., '#808080').")]
            string? color = null,
            [Description("Fog density (for Exponential and ExponentialSquared modes).")]
            float? density = null,
            [Description("Start distance (for Linear mode).")]
            float? startDistance = null,
            [Description("End distance (for Linear mode).")]
            float? endDistance = null
        )
        => MainThread.Instance.Run(() =>
        {
            try
            {
                var changes = new List<string>();

                if (enabled.HasValue)
                {
                    RenderSettings.fog = enabled.Value;
                    changes.Add($"Fog {(enabled.Value ? "enabled" : "disabled")}");
                }

                if (mode.HasValue)
                {
                    RenderSettings.fogMode = mode.Value;
                    changes.Add($"Fog mode set to {mode.Value}");
                }

                if (!string.IsNullOrEmpty(color))
                {
                    RenderSettings.fogColor = HexToColor(color);
                    changes.Add($"Fog color set to {color}");
                }

                if (density.HasValue)
                {
                    RenderSettings.fogDensity = density.Value;
                    changes.Add($"Fog density set to {density.Value}");
                }

                if (startDistance.HasValue)
                {
                    RenderSettings.fogStartDistance = startDistance.Value;
                    changes.Add($"Fog start distance set to {startDistance.Value}");
                }

                if (endDistance.HasValue)
                {
                    RenderSettings.fogEndDistance = endDistance.Value;
                    changes.Add($"Fog end distance set to {endDistance.Value}");
                }

                if (changes.Count == 0)
                    return Error.NoValidFogSettingsProvided();

                return $"[Success] Fog configured:\n- {string.Join("\n- ", changes)}";
            }
            catch (Exception ex)
            {
                return Error.FailedOperation("configure fog", ex.Message);
            }
        });
    }
}
