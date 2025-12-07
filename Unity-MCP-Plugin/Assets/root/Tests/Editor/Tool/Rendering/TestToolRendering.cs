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
using com.IvanMurzak.ReflectorNet;
using com.IvanMurzak.Unity.MCP.Editor.API;
using NUnit.Framework;
using UnityEngine;

namespace com.IvanMurzak.Unity.MCP.Editor.Tests
{
    public partial class TestToolRendering : BaseTest
    {
        protected Tool_Rendering _tool = null!;

        [SetUp]
        public void TestSetUp()
        {
            _tool = new Tool_Rendering();
        }

        protected void ResultValidation(string? result)
        {
            Debug.Log($"[{GetType().GetTypeShortName()}] Result:\n{result}");
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsNotEmpty(result, "Result should not be empty.");
            Assert.IsTrue(result!.Contains("[Success]"), $"Result should contain '[Success]'.\n{result}");
            Assert.IsFalse(result.Contains("[Error]"), $"Result should not contain '[Error]'.\n{result}");
        }

        protected void ResultValidationExpected(string? result, params string[] expectedLines)
        {
            Debug.Log($"[{GetType().GetTypeShortName()}] Result:\n{result}");
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsNotEmpty(result, "Result should not be empty.");
            Assert.IsTrue(result!.Contains("[Success]"), $"Result should contain '[Success]'.\n{result}");

            foreach (var line in expectedLines)
                Assert.IsTrue(result.Contains(line), $"Result should contain expected line: '{line}'.\n{result}");
        }

        protected void ErrorValidation(string? result, string expectedErrorSubstring = "[Error]")
        {
            Debug.Log($"[{GetType().GetTypeShortName()}] Error Result:\n{result}");
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsTrue(result!.Contains(expectedErrorSubstring), 
                $"Result should contain '{expectedErrorSubstring}'.\n{result}");
        }
    }
}
