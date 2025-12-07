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
        public void ConfigureFog_Enabled_SetsCorrectly()
        {
            // Arrange
            var originalEnabled = RenderSettings.fog;
            
            try
            {
                // Act - Enable fog
                var result = _tool.ConfigureFog(enabled: true);

                // Assert
                ResultValidation(result);
                Assert.IsTrue(RenderSettings.fog, "Fog should be enabled.");
                Assert.IsTrue(result!.Contains("Fog enabled"), 
                    "Result should confirm fog enabled.");

                // Act - Disable fog
                result = _tool.ConfigureFog(enabled: false);

                // Assert
                ResultValidation(result);
                Assert.IsFalse(RenderSettings.fog, "Fog should be disabled.");
                Assert.IsTrue(result!.Contains("Fog disabled"), 
                    "Result should confirm fog disabled.");
            }
            finally
            {
                // Restore original value
                RenderSettings.fog = originalEnabled;
            }
        }

        [Test]
        public void ConfigureFog_Mode_SetsCorrectly()
        {
            // Arrange
            var originalMode = RenderSettings.fogMode;
            var targetMode = FogMode.Linear;
            
            try
            {
                // Act
                var result = _tool.ConfigureFog(mode: targetMode);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetMode, RenderSettings.fogMode,
                    "Fog mode should be set correctly.");
                Assert.IsTrue(result!.Contains("Fog mode set to"), 
                    "Result should confirm fog mode change.");
            }
            finally
            {
                // Restore original value
                RenderSettings.fogMode = originalMode;
            }
        }

        [Test]
        public void ConfigureFog_Color_SetsCorrectly()
        {
            // Arrange
            var originalColor = RenderSettings.fogColor;
            var targetColorHex = "#A0B0C0";
            
            try
            {
                // Act
                var result = _tool.ConfigureFog(color: targetColorHex);

                // Assert
                ResultValidation(result);
                Assert.IsTrue(result!.Contains("Fog color set to"), 
                    "Result should confirm fog color change.");
            }
            finally
            {
                // Restore original value
                RenderSettings.fogColor = originalColor;
            }
        }

        [Test]
        public void ConfigureFog_Density_SetsCorrectly()
        {
            // Arrange
            var originalDensity = RenderSettings.fogDensity;
            var targetDensity = 0.05f;
            
            try
            {
                // Act
                var result = _tool.ConfigureFog(density: targetDensity);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetDensity, RenderSettings.fogDensity, 0.001f,
                    "Fog density should be set correctly.");
                Assert.IsTrue(result!.Contains("Fog density set to"), 
                    "Result should confirm fog density change.");
            }
            finally
            {
                // Restore original value
                RenderSettings.fogDensity = originalDensity;
            }
        }

        [Test]
        public void ConfigureFog_LinearDistances_SetsCorrectly()
        {
            // Arrange
            var originalStart = RenderSettings.fogStartDistance;
            var originalEnd = RenderSettings.fogEndDistance;
            var targetStart = 10f;
            var targetEnd = 100f;
            
            try
            {
                // Act
                var result = _tool.ConfigureFog(
                    startDistance: targetStart,
                    endDistance: targetEnd);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetStart, RenderSettings.fogStartDistance, 0.001f,
                    "Fog start distance should be set correctly.");
                Assert.AreEqual(targetEnd, RenderSettings.fogEndDistance, 0.001f,
                    "Fog end distance should be set correctly.");
            }
            finally
            {
                // Restore original values
                RenderSettings.fogStartDistance = originalStart;
                RenderSettings.fogEndDistance = originalEnd;
            }
        }

        [Test]
        public void ConfigureFog_AllSettings_SetsAllCorrectly()
        {
            // Arrange
            var originalEnabled = RenderSettings.fog;
            var originalMode = RenderSettings.fogMode;
            var originalDensity = RenderSettings.fogDensity;
            
            try
            {
                // Act
                var result = _tool.ConfigureFog(
                    enabled: true,
                    mode: FogMode.Exponential,
                    density: 0.03f);

                // Assert
                ResultValidation(result);
                Assert.IsTrue(RenderSettings.fog);
                Assert.AreEqual(FogMode.Exponential, RenderSettings.fogMode);
                Assert.AreEqual(0.03f, RenderSettings.fogDensity, 0.001f);
            }
            finally
            {
                // Restore original values
                RenderSettings.fog = originalEnabled;
                RenderSettings.fogMode = originalMode;
                RenderSettings.fogDensity = originalDensity;
            }
        }

        [Test]
        public void ConfigureFog_NoParameters_ReturnsError()
        {
            // Act
            var result = _tool.ConfigureFog();

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("No valid fog settings provided"), 
                "Result should indicate no valid settings were provided.");
        }
    }
}
