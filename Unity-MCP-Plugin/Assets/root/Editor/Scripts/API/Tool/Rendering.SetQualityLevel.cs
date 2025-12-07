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
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_SetQualityLevel",
            Title = "Set Quality Level"
        )]
        [Description("Sets the active quality level by its index. Use 'Rendering_GetQualitySettings' to see available levels.")]
        public string SetQualityLevel
        (
            [Description("The index of the quality level to set (0-based).")]
            int level
        )
        => MainThread.Instance.Run(() =>
        {
            try
            {
                var names = QualitySettings.names;

                if (level < 0 || level >= names.Length)
                    return Error.InvalidQualityLevel(level, names.Length - 1);

                QualitySettings.SetQualityLevel(level, applyExpensiveChanges: true);

                return $"[Success] Quality level set to [{level}] '{names[level]}'.";
            }
            catch (Exception ex)
            {
                return Error.FailedOperation("set quality level", ex.Message);
            }
        });
    }
}
