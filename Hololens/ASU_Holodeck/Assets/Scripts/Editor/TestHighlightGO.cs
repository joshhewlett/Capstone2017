using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using HoloToolkit.Unity;
using HoloToolkit.Unity.Tests;
using HoloToolkit.Unity.InputModule;

public class TestHighlightGO : MonoBehaviour
{
    [SetUp]
    public void ClearScene()
    {
        TestUtils.ClearScene();
    }

    [Test]
    public void ClickObject()
    {
        //Object under test
        var gameObject = new GameObject();
        // Input Pointer for use in Event Data Generation
        var inputSrc = new InputSourcePointer();

        //TODO: Add more
    }
}