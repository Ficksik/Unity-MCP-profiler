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
            "Rendering_GetLightingInfo",
            Title = "Get Lighting Information"
        )]
        [Description("Gets current lighting, fog, and skybox settings.")]
        public string GetLightingInfo()
        => MainThread.Instance.Run(() =>
        {
            var sb = new StringBuilder();
            sb.AppendLine("[Success] Retrieved lighting information.");
            sb.AppendLine();

            // Ambient Settings
            sb.AppendLine("# Ambient Settings");
            sb.AppendLine($"- ambientMode: {RenderSettings.ambientMode}");
            sb.AppendLine($"- ambientIntensity: {RenderSettings.ambientIntensity}");
            sb.AppendLine($"- ambientSkyColor: {ColorToHex(RenderSettings.ambientSkyColor)}");
            sb.AppendLine($"- ambientEquatorColor: {ColorToHex(RenderSettings.ambientEquatorColor)}");
            sb.AppendLine($"- ambientGroundColor: {ColorToHex(RenderSettings.ambientGroundColor)}");
            sb.AppendLine();

            // GI Settings
            sb.AppendLine("# Global Illumination");
            sb.AppendLine($"- realtimeGIEnabled: {Lightmapping.realtimeGI}");
            sb.AppendLine($"- bakedGIEnabled: {Lightmapping.bakedGI}");
            sb.AppendLine($"- lightmapper: {LightmapEditorSettings.lightmapper}");
            sb.AppendLine();

            // Reflection Settings
            sb.AppendLine("# Reflection Settings");
            sb.AppendLine($"- defaultReflectionMode: {RenderSettings.defaultReflectionMode}");
            sb.AppendLine($"- reflectionIntensity: {RenderSettings.reflectionIntensity}");
            sb.AppendLine();

            // Fog Settings
            sb.AppendLine("# Fog Settings");
            sb.AppendLine($"- fogEnabled: {RenderSettings.fog}");
            sb.AppendLine($"- fogMode: {RenderSettings.fogMode}");
            sb.AppendLine($"- fogColor: {ColorToHex(RenderSettings.fogColor)}");
            sb.AppendLine($"- fogDensity: {RenderSettings.fogDensity}");
            sb.AppendLine($"- fogStartDistance: {RenderSettings.fogStartDistance}");
            sb.AppendLine($"- fogEndDistance: {RenderSettings.fogEndDistance}");
            sb.AppendLine();

            // Skybox Settings
            sb.AppendLine("# Skybox Settings");
            sb.AppendLine($"- skybox: {(RenderSettings.skybox != null ? RenderSettings.skybox.name : "null")}");
            sb.AppendLine($"- skyboxPath: {(RenderSettings.skybox != null ? AssetDatabase.GetAssetPath(RenderSettings.skybox) : "null")}");

            return sb.ToString();
        });
    }
}
