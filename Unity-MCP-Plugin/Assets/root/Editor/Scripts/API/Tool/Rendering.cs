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
using com.IvanMurzak.McpPlugin;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    [McpPluginToolType]
    public partial class Tool_Rendering
    {
        public static class Error
        {
            public static string PipelineTypeMustBeSpecified()
                => "[Error] Pipeline type must be specified ('builtin', 'urp', or 'hdrp').";

            public static string UnknownPipelineType(string pipelineType)
                => $"[Error] Unknown pipeline type: '{pipelineType}'. Supported types are 'builtin', 'urp', 'hdrp'.";

            public static string AssetPathRequiredForPipeline(string pipelineType)
                => $"[Error] Asset path required for {pipelineType.ToUpper()} pipeline.";

            public static string CouldNotLoadPipelineAsset(string assetPath)
                => $"[Error] Could not load render pipeline asset at path: '{assetPath}'.";

            public static string NoValidLightingSettingsProvided()
                => "[Error] No valid lighting settings provided.";

            public static string NoValidSkyboxSettingsProvided()
                => "[Error] No valid skybox settings provided.";

            public static string NoValidFogSettingsProvided()
                => "[Error] No valid fog settings provided.";

            public static string CouldNotLoadSkyboxMaterial(string skyboxPath)
                => $"[Error] Could not load skybox material at path: '{skyboxPath}'.";

            public static string FailedToStartLightmapBaking()
                => "[Error] Failed to start lightmap baking. Ensure scene has lightmap-enabled objects.";

            public static string InvalidQualityLevel(int level, int maxLevel)
                => $"[Error] Invalid quality level. Must be between 0 and {maxLevel}.";

            public static string FailedOperation(string operation, string message)
                => $"[Error] Failed to {operation}: {message}";
        }

        /// <summary>
        /// Converts a Unity Color to a hexadecimal string.
        /// </summary>
        private static string ColorToHex(Color color)
            => "#" + ColorUtility.ToHtmlStringRGB(color);

        /// <summary>
        /// Converts a hexadecimal string to a Unity Color.
        /// </summary>
        private static Color HexToColor(string hex)
        {
            if (!ColorUtility.TryParseHtmlString(hex, out Color color))
                throw new ArgumentException($"Invalid color hex string: '{hex}'");
            return color;
        }
    }
}
