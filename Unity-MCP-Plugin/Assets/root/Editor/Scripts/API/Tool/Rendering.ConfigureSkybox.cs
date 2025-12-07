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

namespace com.IvanMurzak.Unity.MCP.Editor.API
{
    public partial class Tool_Rendering
    {
        [McpPluginTool
        (
            "Rendering_ConfigureSkybox",
            Title = "Configure Skybox"
        )]
        [Description("Sets the skybox material and sun source.")]
        public string ConfigureSkybox
        (
            [Description("The project-relative path to a skybox material (e.g., 'Assets/Materials/MySkybox.mat').")]
            string? skyboxPath = null,
            [Description("The name of the GameObject with the main directional light to be used as the sun.")]
            string? sunSource = null
        )
        => MainThread.Instance.Run(() =>
        {
            try
            {
                var changes = new List<string>();

                if (!string.IsNullOrEmpty(skyboxPath))
                {
                    var material = AssetDatabase.LoadAssetAtPath<Material>(skyboxPath);
                    if (material == null)
                        return Error.CouldNotLoadSkyboxMaterial(skyboxPath);

                    RenderSettings.skybox = material;
                    changes.Add($"Skybox set to '{material.name}'");
                }

                if (!string.IsNullOrEmpty(sunSource))
                {
                    var sunGameObject = GameObject.Find(sunSource);
                    if (sunGameObject != null)
                    {
                        var light = sunGameObject.GetComponent<Light>();
                        if (light != null)
                        {
                            RenderSettings.sun = light;
                            changes.Add($"Sun source set to '{sunSource}'");
                        }
                        else
                        {
                            changes.Add($"Warning: GameObject '{sunSource}' found but has no Light component");
                        }
                    }
                    else
                    {
                        changes.Add($"Warning: GameObject '{sunSource}' not found");
                    }
                }

                if (changes.Count == 0)
                    return Error.NoValidSkyboxSettingsProvided();

                return $"[Success] Skybox configured:\n- {string.Join("\n- ", changes)}";
            }
            catch (Exception ex)
            {
                return Error.FailedOperation("configure skybox", ex.Message);
            }
        });
    }
}
