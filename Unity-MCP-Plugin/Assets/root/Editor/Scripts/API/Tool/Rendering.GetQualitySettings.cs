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
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_GetQualitySettings",
            Title = "Get Quality Settings"
        )]
        [Description("Gets the list of all available quality levels.")]
        public string GetQualitySettings()
        => MainThread.Instance.Run(() =>
        {
            var currentLevel = QualitySettings.GetQualityLevel();
            var names = QualitySettings.names;

            var sb = new StringBuilder();
            sb.AppendLine("[Success] Retrieved quality settings.");
            sb.AppendLine();
            sb.AppendLine($"# Current Quality Level");
            sb.AppendLine($"- index: {currentLevel}");
            sb.AppendLine($"- name: {names[currentLevel]}");
            sb.AppendLine();
            sb.AppendLine("# Available Quality Levels");

            for (int i = 0; i < names.Length; i++)
            {
                var isCurrent = i == currentLevel;
                sb.AppendLine($"- [{i}] {names[i]}{(isCurrent ? " (current)" : "")}");
            }

            return sb.ToString();
        });
    }
}
