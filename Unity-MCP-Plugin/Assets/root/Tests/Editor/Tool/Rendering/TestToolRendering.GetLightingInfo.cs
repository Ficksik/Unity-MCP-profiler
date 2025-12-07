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

namespace com.IvanMurzak.Unity.MCP.Editor.Tests
{
    public partial class TestToolRendering
    {
        [Test]
        public void GetLightingInfo_ReturnsValidData()
        {
            // Act
            var result = _tool.GetLightingInfo();

            // Assert
            ResultValidationExpected(result,
                "# Ambient Settings",
                "ambientMode:",
                "ambientIntensity:",
                "# Global Illumination",
                "realtimeGIEnabled:",
                "bakedGIEnabled:",
                "# Reflection Settings",
                "reflectionIntensity:",
                "# Fog Settings",
                "fogEnabled:",
                "fogMode:",
                "# Skybox Settings");
        }

        [Test]
        public void GetLightingInfo_ContainsAmbientColors()
        {
            // Act
            var result = _tool.GetLightingInfo();

            // Assert
            ResultValidation(result);
            Assert.IsTrue(result!.Contains("ambientSkyColor:"), "Result should contain ambient sky color.");
            Assert.IsTrue(result.Contains("ambientEquatorColor:"), "Result should contain ambient equator color.");
            Assert.IsTrue(result.Contains("ambientGroundColor:"), "Result should contain ambient ground color.");
        }

        [Test]
        public void GetLightingInfo_ContainsFogSettings()
        {
            // Act
            var result = _tool.GetLightingInfo();

            // Assert
            ResultValidation(result);
            Assert.IsTrue(result!.Contains("fogColor:"), "Result should contain fog color.");
            Assert.IsTrue(result.Contains("fogDensity:"), "Result should contain fog density.");
            Assert.IsTrue(result.Contains("fogStartDistance:"), "Result should contain fog start distance.");
            Assert.IsTrue(result.Contains("fogEndDistance:"), "Result should contain fog end distance.");
        }
    }
}
