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
using NUnit.Framework;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.Tests
{
    public partial class TestToolRendering
    {
        [Test]
        public void ConfigureSkybox_SunSource_WithValidLight_SetsCorrectly()
        {
            // Arrange
            var originalSun = RenderSettings.sun;
            var lightGO = new GameObject("TestSunLight");
            var light = lightGO.AddComponent<Light>();
            light.type = LightType.Directional;
            
            try
            {
                // Act
                var result = _tool.ConfigureSkybox(sunSource: "TestSunLight");

                // Assert
                ResultValidation(result);
                Assert.AreEqual(light, RenderSettings.sun, "Sun source should be set correctly.");
                Assert.IsTrue(result!.Contains("Sun source set to"), 
                    "Result should confirm sun source change.");
            }
            finally
            {
                // Restore original value and cleanup
                RenderSettings.sun = originalSun;
                Object.DestroyImmediate(lightGO);
            }
        }

        [Test]
        public void ConfigureSkybox_SunSource_WithNonexistentObject_ReturnsWarning()
        {
            // Act
            var result = _tool.ConfigureSkybox(sunSource: "NonexistentSunObject_12345");

            // Assert
            ResultValidation(result);
            Assert.IsTrue(result!.Contains("not found") || result.Contains("Warning"), 
                "Result should indicate object was not found.");
        }

        [Test]
        public void ConfigureSkybox_SunSource_WithObjectWithoutLight_ReturnsWarning()
        {
            // Arrange
            var gameObject = new GameObject("TestObjectWithoutLight");
            
            try
            {
                // Act
                var result = _tool.ConfigureSkybox(sunSource: "TestObjectWithoutLight");

                // Assert
                ResultValidation(result);
                Assert.IsTrue(result!.Contains("no Light component") || result.Contains("Warning"), 
                    "Result should indicate object has no Light component.");
            }
            finally
            {
                // Cleanup
                Object.DestroyImmediate(gameObject);
            }
        }

        [Test]
        public void ConfigureSkybox_InvalidSkyboxPath_ReturnsError()
        {
            // Act
            var result = _tool.ConfigureSkybox(skyboxPath: "Assets/NonExistent/FakeSkybox.mat");

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("Could not load skybox material"), 
                "Result should indicate skybox material could not be loaded.");
        }

        [Test]
        public void ConfigureSkybox_NoParameters_ReturnsError()
        {
            // Act
            var result = _tool.ConfigureSkybox();

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("No valid skybox settings provided"), 
                "Result should indicate no valid settings were provided.");
        }
    }
}
