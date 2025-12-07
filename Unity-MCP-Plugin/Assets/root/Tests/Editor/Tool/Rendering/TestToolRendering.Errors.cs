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
using com.IvanMurzak.Unity.MCP.Editor.API;
using NUnit.Framework;

namespace com.IvanMurzak.Unity.MCP.Editor.Tests
{
    public partial class TestToolRendering
    {
        [Test]
        public void Error_PipelineTypeMustBeSpecified_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.PipelineTypeMustBeSpecified();

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Pipeline type must be specified"), 
                "Should contain error description.");
        }

        [Test]
        public void Error_UnknownPipelineType_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.UnknownPipelineType("invalid");

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Unknown pipeline type"), "Should contain error description.");
            Assert.IsTrue(result.Contains("invalid"), "Should contain the invalid value.");
        }

        [Test]
        public void Error_AssetPathRequiredForPipeline_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.AssetPathRequiredForPipeline("urp");

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Asset path required"), "Should contain error description.");
            Assert.IsTrue(result.Contains("URP"), "Should contain the pipeline type.");
        }

        [Test]
        public void Error_CouldNotLoadPipelineAsset_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.CouldNotLoadPipelineAsset("Assets/Test.asset");

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Could not load"), "Should contain error description.");
            Assert.IsTrue(result.Contains("Assets/Test.asset"), "Should contain the asset path.");
        }

        [Test]
        public void Error_NoValidLightingSettingsProvided_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.NoValidLightingSettingsProvided();

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("No valid lighting settings provided"), 
                "Should contain error description.");
        }

        [Test]
        public void Error_NoValidSkyboxSettingsProvided_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.NoValidSkyboxSettingsProvided();

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("No valid skybox settings provided"), 
                "Should contain error description.");
        }

        [Test]
        public void Error_NoValidFogSettingsProvided_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.NoValidFogSettingsProvided();

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("No valid fog settings provided"), 
                "Should contain error description.");
        }

        [Test]
        public void Error_CouldNotLoadSkyboxMaterial_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.CouldNotLoadSkyboxMaterial("Assets/Skybox.mat");

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Could not load skybox material"), 
                "Should contain error description.");
            Assert.IsTrue(result.Contains("Assets/Skybox.mat"), "Should contain the path.");
        }

        [Test]
        public void Error_FailedToStartLightmapBaking_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.FailedToStartLightmapBaking();

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Failed to start lightmap baking"), 
                "Should contain error description.");
        }

        [Test]
        public void Error_InvalidQualityLevel_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.InvalidQualityLevel(10, 5);

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Invalid quality level"), "Should contain error description.");
            Assert.IsTrue(result.Contains("0") && result.Contains("5"), 
                "Should contain the valid range.");
        }

        [Test]
        public void Error_FailedOperation_ReturnsCorrectMessage()
        {
            // Act
            var result = Tool_Rendering.Error.FailedOperation("test operation", "some error message");

            // Assert
            Assert.IsTrue(result.Contains("[Error]"), "Should contain error prefix.");
            Assert.IsTrue(result.Contains("Failed to test operation"), 
                "Should contain the operation name.");
            Assert.IsTrue(result.Contains("some error message"), 
                "Should contain the error message.");
        }
    }
}
