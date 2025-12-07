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
using UnityEngine.Rendering;

namespace com.IvanMurzak.Unity.MCP.Editor.Tests
{
    public partial class TestToolRendering
    {
        [Test]
        public void ConfigureLighting_AmbientIntensity_SetsCorrectly()
        {
            // Arrange
            var originalIntensity = RenderSettings.ambientIntensity;
            var targetIntensity = 0.75f;
            
            try
            {
                // Act
                var result = _tool.ConfigureLighting(ambientIntensity: targetIntensity);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetIntensity, RenderSettings.ambientIntensity, 0.001f,
                    "Ambient intensity should be set correctly.");
                Assert.IsTrue(result!.Contains("Ambient intensity set to"), 
                    "Result should confirm ambient intensity change.");
            }
            finally
            {
                // Restore original value
                RenderSettings.ambientIntensity = originalIntensity;
            }
        }

        [Test]
        public void ConfigureLighting_ReflectionIntensity_SetsCorrectly()
        {
            // Arrange
            var originalIntensity = RenderSettings.reflectionIntensity;
            var targetIntensity = 0.5f;
            
            try
            {
                // Act
                var result = _tool.ConfigureLighting(reflectionIntensity: targetIntensity);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetIntensity, RenderSettings.reflectionIntensity, 0.001f,
                    "Reflection intensity should be set correctly.");
                Assert.IsTrue(result!.Contains("Reflection intensity set to"), 
                    "Result should confirm reflection intensity change.");
            }
            finally
            {
                // Restore original value
                RenderSettings.reflectionIntensity = originalIntensity;
            }
        }

        [Test]
        public void ConfigureLighting_AmbientMode_SetsCorrectly()
        {
            // Arrange
            var originalMode = RenderSettings.ambientMode;
            var targetMode = AmbientMode.Flat;
            
            try
            {
                // Act
                var result = _tool.ConfigureLighting(ambientMode: targetMode);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetMode, RenderSettings.ambientMode,
                    "Ambient mode should be set correctly.");
                Assert.IsTrue(result!.Contains("Ambient mode set to"), 
                    "Result should confirm ambient mode change.");
            }
            finally
            {
                // Restore original value
                RenderSettings.ambientMode = originalMode;
            }
        }

        [Test]
        public void ConfigureLighting_AmbientSkyColor_SetsCorrectly()
        {
            // Arrange
            var originalColor = RenderSettings.ambientSkyColor;
            var targetColorHex = "#FF5500";
            
            try
            {
                // Act
                var result = _tool.ConfigureLighting(ambientSkyColor: targetColorHex);

                // Assert
                ResultValidation(result);
                Assert.IsTrue(result!.Contains("Ambient sky color set to"), 
                    "Result should confirm ambient sky color change.");
            }
            finally
            {
                // Restore original value
                RenderSettings.ambientSkyColor = originalColor;
            }
        }

        [Test]
        public void ConfigureLighting_MultipleSettings_SetsAllCorrectly()
        {
            // Arrange
            var originalAmbientIntensity = RenderSettings.ambientIntensity;
            var originalReflectionIntensity = RenderSettings.reflectionIntensity;
            
            try
            {
                // Act
                var result = _tool.ConfigureLighting(
                    ambientIntensity: 0.6f,
                    reflectionIntensity: 0.4f);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(0.6f, RenderSettings.ambientIntensity, 0.001f);
                Assert.AreEqual(0.4f, RenderSettings.reflectionIntensity, 0.001f);
            }
            finally
            {
                // Restore original values
                RenderSettings.ambientIntensity = originalAmbientIntensity;
                RenderSettings.reflectionIntensity = originalReflectionIntensity;
            }
        }

        [Test]
        public void ConfigureLighting_NoParameters_ReturnsError()
        {
            // Act
            var result = _tool.ConfigureLighting();

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("No valid lighting settings provided"), 
                "Result should indicate no valid settings were provided.");
        }
    }
}
