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
using UnityEngine.Rendering;

namespace com.IvanMurzak.Unity.MCP.Editor.Tests
{
    public partial class TestToolRendering
    {
        [Test]
        public void SetRenderPipeline_Builtin_SetsCorrectly()
        {
            // Arrange
            var originalPipeline = GraphicsSettings.renderPipelineAsset;
            
            try
            {
                // Act
                var result = _tool.SetRenderPipeline(Tool_Rendering.RenderPipelineType.builtin);

                // Assert
                ResultValidation(result);
                Assert.IsNull(GraphicsSettings.renderPipelineAsset, 
                    "Render pipeline asset should be null for built-in pipeline.");
                Assert.IsTrue(result!.Contains("Built-in Render Pipeline"), 
                    "Result should confirm switch to built-in pipeline.");
            }
            finally
            {
                // Restore original pipeline
                GraphicsSettings.renderPipelineAsset = originalPipeline;
            }
        }

        [Test]
        public void SetRenderPipeline_URP_WithoutAssetPath_ReturnsError()
        {
            // Act
            var result = _tool.SetRenderPipeline(Tool_Rendering.RenderPipelineType.urp);

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("Asset path required"), 
                "Result should indicate asset path is required.");
        }

        [Test]
        public void SetRenderPipeline_HDRP_WithoutAssetPath_ReturnsError()
        {
            // Act
            var result = _tool.SetRenderPipeline(Tool_Rendering.RenderPipelineType.hdrp);

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("Asset path required"), 
                "Result should indicate asset path is required.");
        }

        [Test]
        public void SetRenderPipeline_URP_WithInvalidAssetPath_ReturnsError()
        {
            // Act
            var result = _tool.SetRenderPipeline(
                Tool_Rendering.RenderPipelineType.urp, 
                assetPath: "Assets/NonExistent/FakeURP.asset");

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("Could not load"), 
                "Result should indicate asset could not be loaded.");
        }

        [Test]
        public void SetRenderPipeline_HDRP_WithInvalidAssetPath_ReturnsError()
        {
            // Act
            var result = _tool.SetRenderPipeline(
                Tool_Rendering.RenderPipelineType.hdrp, 
                assetPath: "Assets/NonExistent/FakeHDRP.asset");

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("Could not load"), 
                "Result should indicate asset could not be loaded.");
        }
    }
}
