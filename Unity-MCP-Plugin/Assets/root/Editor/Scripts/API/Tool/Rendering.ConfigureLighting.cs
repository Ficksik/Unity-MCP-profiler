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
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_ConfigureLighting",
            Title = "Configure Lighting Settings"
        )]
        [Description(@"Configures ambient, Global Illumination (GI), and reflection settings.
Available ambient modes: 'Skybox', 'Trilight', 'Flat', 'Custom'.")]
        public string ConfigureLighting
        (
            [Description("Ambient lighting mode: 'Skybox', 'Trilight', 'Flat', 'Custom'.")]
            AmbientMode? ambientMode = null,
            [Description("Intensity of ambient light (0.0 - 8.0).")]
            float? ambientIntensity = null,
            [Description("Hex color string for ambient sky color (e.g., '#FFFFFF').")]
            string? ambientSkyColor = null,
            [Description("Hex color string for ambient equator color (Trilight mode).")]
            string? ambientEquatorColor = null,
            [Description("Hex color string for ambient ground color (Trilight mode).")]
            string? ambientGroundColor = null,
            [Description("Enable/disable Realtime Global Illumination.")]
            bool? realtimeGIEnabled = null,
            [Description("Enable/disable Baked Global Illumination.")]
            bool? bakedGIEnabled = null,
            [Description("Intensity of reflection probes (0.0 - 1.0).")]
            float? reflectionIntensity = null
        )
        => MainThread.Instance.Run(() =>
        {
            try
            {
                var changes = new List<string>();

                if (ambientMode.HasValue)
                {
                    RenderSettings.ambientMode = ambientMode.Value;
                    changes.Add($"Ambient mode set to {ambientMode.Value}");
                }

                if (ambientIntensity.HasValue)
                {
                    RenderSettings.ambientIntensity = ambientIntensity.Value;
                    changes.Add($"Ambient intensity set to {ambientIntensity.Value}");
                }

                if (!string.IsNullOrEmpty(ambientSkyColor))
                {
                    RenderSettings.ambientSkyColor = HexToColor(ambientSkyColor);
                    changes.Add($"Ambient sky color set to {ambientSkyColor}");
                }

                if (!string.IsNullOrEmpty(ambientEquatorColor))
                {
                    RenderSettings.ambientEquatorColor = HexToColor(ambientEquatorColor);
                    changes.Add($"Ambient equator color set to {ambientEquatorColor}");
                }

                if (!string.IsNullOrEmpty(ambientGroundColor))
                {
                    RenderSettings.ambientGroundColor = HexToColor(ambientGroundColor);
                    changes.Add($"Ambient ground color set to {ambientGroundColor}");
                }

                if (realtimeGIEnabled.HasValue)
                {
                    Lightmapping.realtimeGI = realtimeGIEnabled.Value;
                    changes.Add($"Realtime GI {(realtimeGIEnabled.Value ? "enabled" : "disabled")}");
                }

                if (bakedGIEnabled.HasValue)
                {
                    Lightmapping.bakedGI = bakedGIEnabled.Value;
                    changes.Add($"Baked GI {(bakedGIEnabled.Value ? "enabled" : "disabled")}");
                }

                if (reflectionIntensity.HasValue)
                {
                    RenderSettings.reflectionIntensity = reflectionIntensity.Value;
                    changes.Add($"Reflection intensity set to {reflectionIntensity.Value}");
                }

                if (changes.Count == 0)
                    return Error.NoValidLightingSettingsProvided();

                return $"[Success] Lighting configured:\n- {string.Join("\n- ", changes)}";
            }
            catch (Exception ex)
            {
                return Error.FailedOperation("configure lighting", ex.Message);
            }
        });
    }
}
