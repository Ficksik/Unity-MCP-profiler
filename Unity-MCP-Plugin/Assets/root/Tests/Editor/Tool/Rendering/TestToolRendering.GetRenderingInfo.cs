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
        public void GetRenderingInfo_ReturnsValidData()
        {
            // Act
            var result = _tool.GetRenderingInfo();

            // Assert
            ResultValidationExpected(result,
                "# Render Pipeline",
                "renderPipeline:",
                "# Graphics Settings",
                "graphicsTier:",
                "colorSpace:",
                "# Quality Settings",
                "currentQualityLevel:",
                "qualityLevelName:");
        }

        [Test]
        public void GetRenderingInfo_ContainsRenderPipelineInfo()
        {
            // Act
            var result = _tool.GetRenderingInfo();

            // Assert
            ResultValidation(result);
            Assert.IsTrue(
                result!.Contains("Built-in Render Pipeline") || result.Contains("renderPipelineAsset:"),
                "Result should contain render pipeline information.");
        }
    }
}
