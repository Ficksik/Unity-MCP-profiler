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
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace com.IvanMurzak.Unity.MCP.Editor.Tests
{
    public partial class TestToolRendering
    {
        [Test]
        public void GetLightmapStatus_ReturnsValidData()
        {
            // Act
            var result = _tool.GetLightmapStatus();

            // Assert
            ResultValidationExpected(result,
                "# Baking Status",
                "isRunning:",
                "buildProgress:",
                "lightmapCount:");
        }

        [Test]
        public void GetLightmapStatus_ContainsProgressInfo()
        {
            // Act
            var result = _tool.GetLightmapStatus();

            // Assert
            ResultValidation(result);
            Assert.IsTrue(result!.Contains("%"), "Result should contain progress percentage.");
        }

        [UnityTest]
        public IEnumerator BakeLightmaps_StartsAndCanBeCancelled()
        {
            // Skip this test if lightmapping is already running
            if (Lightmapping.isRunning)
            {
                Assert.Inconclusive("Lightmapping is already running, skipping test.");
                yield break;
            }

            // Act - Start baking
            var startResult = _tool.BakeLightmaps(clearCache: false);

            // The result might be success or error depending on scene setup
            // We just verify it doesn't throw and returns a valid response
            Assert.IsNotNull(startResult, "BakeLightmaps should return a result.");

            yield return null;

            // If baking started successfully, cancel it
            if (startResult!.Contains("[Success]") && Lightmapping.isRunning)
            {
                var cancelResult = _tool.CancelLightmapBake();
                ResultValidation(cancelResult);
                Assert.IsTrue(cancelResult!.Contains("cancelled"), 
                    "Result should confirm baking was cancelled.");
            }

            yield return null;
        }

        [Test]
        public void CancelLightmapBake_WhenNotBaking_StillSucceeds()
        {
            // Skip if baking is running
            if (Lightmapping.isRunning)
            {
                Assert.Inconclusive("Lightmapping is running, skipping test.");
                return;
            }

            // Act
            var result = _tool.CancelLightmapBake();

            // Assert - Should succeed even if nothing was baking
            ResultValidation(result);
        }

        [Test]
        public void BakeLightmaps_WithClearCache_IncludesCacheInfo()
        {
            // Skip if baking is running
            if (Lightmapping.isRunning)
            {
                Assert.Inconclusive("Lightmapping is running, skipping test.");
                return;
            }

            // Act
            var result = _tool.BakeLightmaps(clearCache: true);

            // Assert - Result should mention cache clearing
            Assert.IsNotNull(result, "BakeLightmaps should return a result.");
            
            if (result!.Contains("[Success]"))
            {
                Assert.IsTrue(result.Contains("clearedCache: True"), 
                    "Result should indicate cache was cleared.");
                
                // Cancel to clean up
                _tool.CancelLightmapBake();
            }
        }
    }
}
