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
        public void SetQualityLevel_ValidLevel_SetsCorrectly()
        {
            // Arrange
            var originalLevel = QualitySettings.GetQualityLevel();
            var targetLevel = 0; // Set to first level
            
            try
            {
                // Act
                var result = _tool.SetQualityLevel(targetLevel);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetLevel, QualitySettings.GetQualityLevel(), 
                    "Quality level should be set correctly.");
                Assert.IsTrue(result!.Contains(QualitySettings.names[targetLevel]), 
                    "Result should contain the new quality level name.");
            }
            finally
            {
                // Restore original level
                QualitySettings.SetQualityLevel(originalLevel, true);
            }
        }

        [Test]
        public void SetQualityLevel_LastLevel_SetsCorrectly()
        {
            // Arrange
            var originalLevel = QualitySettings.GetQualityLevel();
            var targetLevel = QualitySettings.names.Length - 1; // Set to last level
            
            try
            {
                // Act
                var result = _tool.SetQualityLevel(targetLevel);

                // Assert
                ResultValidation(result);
                Assert.AreEqual(targetLevel, QualitySettings.GetQualityLevel(), 
                    "Quality level should be set to last level.");
            }
            finally
            {
                // Restore original level
                QualitySettings.SetQualityLevel(originalLevel, true);
            }
        }

        [Test]
        public void SetQualityLevel_NegativeLevel_ReturnsError()
        {
            // Act
            var result = _tool.SetQualityLevel(-1);

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("Invalid quality level"), 
                "Result should contain invalid quality level error.");
        }

        [Test]
        public void SetQualityLevel_LevelTooHigh_ReturnsError()
        {
            // Arrange
            var invalidLevel = QualitySettings.names.Length + 10;

            // Act
            var result = _tool.SetQualityLevel(invalidLevel);

            // Assert
            ErrorValidation(result);
            Assert.IsTrue(result!.Contains("Invalid quality level"), 
                "Result should contain invalid quality level error.");
        }
    }
}
