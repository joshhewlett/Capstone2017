using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using HoloToolkit.Unity;
using HoloToolkit.Unity.Tests;
using HoloToolkit.Unity.InputModule;

public class TestDynamicMenus : MonoBehaviour
{
    [SetUp]
    public void ClearScene()
    {
        TestUtils.ClearScene();
    }

    [Test]
    public void DynamicObjectMenuInit()
    {
        var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Input Pointer for use in Event Data Generation
        var inputSrc = new InputSourcePointer();
        // gameObject.AddComponent<MeshRenderer>();
        var dynamicMenu = gameObject.AddComponent<DynamicObjectMenu>();

        Assert.That(dynamicMenu.isActiveAndEnabled, NUnit.Framework.Is.True);
    }

    [Test]
    public void DynamicObjectMenuToggleOn()
    {
        //var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var gameObject = new GameObject();
        var inputSrc = new InputSourcePointer();
        var clickData = new InputClickedEventData(EventSystem.current);

        var dynamicMenu = gameObject.AddComponent<DynamicObjectMenu>();
        dynamicMenu.toggleMenuUpDown = 0;
        dynamicMenu.menuOptions = new GameObject();

        clickData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, InteractionSourcePressInfo.Select, 1);
        dynamicMenu.OnInputClicked(clickData);

        Assert.That(dynamicMenu.toggleMenuUpDown == 1);
        //TODO: Input code that tests the menu is showing or not
        Assert.That(dynamicMenu.menuOptions.activeInHierarchy, NUnit.Framework.Is.True);
    }
    //TODO: add in following unit tests for other types of menus
    [Test]
    public void DynamicObjectMenuToggleOff() {
        //var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var gameObject = new GameObject();
        var inputSrc = new InputSourcePointer();
        var clickData = new InputClickedEventData(EventSystem.current);

        var dynamicMenu = gameObject.AddComponent<DynamicObjectMenu>();
        dynamicMenu.toggleMenuUpDown = 1;
        dynamicMenu.menuOptions = new GameObject();

        clickData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, InteractionSourcePressInfo.Select, 1);
        dynamicMenu.OnInputClicked(clickData);

        Assert.That(dynamicMenu.toggleMenuUpDown == 2);
        //TODO: Input code that tests the menu is showing or not
        Assert.That(dynamicMenu.menuOptions.activeInHierarchy, NUnit.Framework.Is.False);
    }

    [Test]
    public void DynamicQuickMenuInit() {
        var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Input Pointer for use in Event Data Generation
        var inputSrc = new InputSourcePointer();
        // gameObject.AddComponent<MeshRenderer>();
        var dynamicMenu = gameObject.AddComponent<DynamicQuickMenu>();

        Assert.That(dynamicMenu.isActiveAndEnabled, NUnit.Framework.Is.True);
    }

    [Test]
    public void DynamicQuickMenuToggleOn() {
        //var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var gameObject = new GameObject();
        var inputSrc = new InputSourcePointer();
        var clickData = new InputClickedEventData(EventSystem.current);

        var dynamicMenu = gameObject.AddComponent<DynamicQuickMenu>();
        dynamicMenu.toggleMenuUpDown = 0;
        dynamicMenu.menuOptions = new GameObject();

        clickData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, InteractionSourcePressInfo.Select, 1);
        dynamicMenu.OnInputClicked(clickData);

        Assert.That(dynamicMenu.toggleMenuUpDown == 1);
        //TODO: Input code that tests the menu is showing or not
        Assert.That(dynamicMenu.menuOptions.activeInHierarchy, NUnit.Framework.Is.True);
    }

    [Test]
    public void DynamicQuickMenuToggleOff() {
        //var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var gameObject = new GameObject();
        var inputSrc = new InputSourcePointer();
        var clickData = new InputClickedEventData(EventSystem.current);

        var dynamicMenu = gameObject.AddComponent<DynamicQuickMenu>();
        dynamicMenu.toggleMenuUpDown = 1;
        dynamicMenu.menuOptions = new GameObject();

        clickData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, InteractionSourcePressInfo.Select, 1);
        dynamicMenu.OnInputClicked(clickData);

        Assert.That(dynamicMenu.toggleMenuUpDown == 2);
        //TODO: Input code that tests the menu is showing or not
        Assert.That(dynamicMenu.menuOptions.activeInHierarchy, NUnit.Framework.Is.False);
    }
}