using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using HoloToolkit.Unity;
using HoloToolkit.Unity.Tests;
using HoloToolkit.Unity.InputModule;

// This test file tests MoveManipulation.cs
public class TestManipulation : MonoBehaviour
{

    [SetUp]
    public void ClearScene()
    {
        TestUtils.ClearScene();
    }

    [Test]
    public void moveX()
    {
        //Object under test
        var gameObject = new GameObject();
        // Input Pointer for use in Event Data Generation
        var inputSrc = new InputSourcePointer();

        //Benchmark initial location
        var initialX = gameObject.transform.position.x;
        var initialY = gameObject.transform.position.y;
        var initialZ = gameObject.transform.position.z;
        //assigning script under test
        var moveManip = gameObject.AddComponent<MoveManipulation>();

        moveManip.MoveManipulationEnabled = true;
        //creating ManipulationEventData for the manipulation data
        var manipEventData = new ManipulationEventData(EventSystem.current);
        // Setting values within the EventData to match the Input Pointer, 
        // acting on our test game object, and a direction change
        manipEventData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, Vector3.right);

        // call function under test
        moveManip.OnManipulationUpdated(manipEventData);
        // get updated position after test manipulation
        var newX = gameObject.transform.position.x;
        var newY = gameObject.transform.position.y;
        var newZ = gameObject.transform.position.z;
        // verify that X position has changed and Y/Z have not
        Assert.That(initialX != newX);
        Assert.That(initialY == newY);
        Assert.That(initialZ == newZ);
    }

    [Test]
    public void moveY()
    {
        //Object under test
        var gameObject = new GameObject();
        // Input Pointer for use in Event Data Generation
        var inputSrc = new InputSourcePointer();

        //Benchmark initial location
        var initialX = gameObject.transform.position.x;
        var initialY = gameObject.transform.position.y;
        var initialZ = gameObject.transform.position.z;
        //assigning script under test
        var moveManip = gameObject.AddComponent<MoveManipulation>();

        moveManip.MoveManipulationEnabled = true;
        //creating ManipulationEventData for the manipulation data
        var manipEventData = new ManipulationEventData(EventSystem.current);
        // Setting values within the EventData to match the Input Pointer, 
        // acting on our test game object, and a direction change
        manipEventData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, Vector3.up);

        // call function under test
        moveManip.OnManipulationUpdated(manipEventData);
        // get updated position after test manipulation
        var newX = gameObject.transform.position.x;
        var newY = gameObject.transform.position.y;
        var newZ = gameObject.transform.position.z;
        // verify that X position has changed and Y/Z have not
        Assert.That(initialX == newX);
        Assert.That(initialY != newY);
        Assert.That(initialZ == newZ);
    }

    [Test]
    public void moveZ()
    {
        //Object under test
        var gameObject = new GameObject();
        // Input Pointer for use in Event Data Generation
        var inputSrc = new InputSourcePointer();

        //Benchmark initial location
        var initialX = gameObject.transform.position.x;
        var initialY = gameObject.transform.position.y;
        var initialZ = gameObject.transform.position.z;
        //assigning script under test
        var moveManip = gameObject.AddComponent<MoveManipulation>();

        moveManip.MoveManipulationEnabled = true;
        //creating ManipulationEventData for the manipulation data
        var manipEventData = new ManipulationEventData(EventSystem.current);
        // Setting values within the EventData to match the Input Pointer, 
        // acting on our test game object, and a direction change
        manipEventData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, Vector3.forward);

        // call function under test
        moveManip.OnManipulationUpdated(manipEventData);
        // get updated position after test manipulation
        var newX = gameObject.transform.position.x;
        var newY = gameObject.transform.position.y;
        var newZ = gameObject.transform.position.z;
        // verify that X position has changed and Y/Z have not
        Assert.That(initialX == newX);
        Assert.That(initialY == newY);
        Assert.That(initialZ != newZ);
    }

    [Test]
    public void moveFails()
    {
        //Object under test
        var gameObject = new GameObject();
        // Input Pointer for use in Event Data Generation
        var inputSrc = new InputSourcePointer();

        //Benchmark initial location
        var initialX = gameObject.transform.position.x;
        var initialY = gameObject.transform.position.y;
        var initialZ = gameObject.transform.position.z;
        //assigning script under test
        var moveManip = gameObject.AddComponent<MoveManipulation>();

        moveManip.MoveManipulationEnabled = false;
        //creating ManipulationEventData for the manipulation data
        var manipEventData = new ManipulationEventData(EventSystem.current);
        // Setting values within the EventData to match the Input Pointer, 
        // acting on our test game object, and a direction change
        manipEventData.Initialize(inputSrc.InputSource, inputSrc.InputSourceId, gameObject, Vector3.forward);

        // call function under test
        moveManip.OnManipulationUpdated(manipEventData);
        // get updated position after test manipulation
        var newX = gameObject.transform.position.x;
        var newY = gameObject.transform.position.y;
        var newZ = gameObject.transform.position.z;
        // verify that X position has changed and Y/Z have not
        Assert.That(initialX == newX);
        Assert.That(initialY == newY);
        Assert.That(initialZ == newZ);
    }

    //TODO: Get information from Ali about rotation
    // [Test]
    // public void rotateX()
    // {

    // }

    // [Test]
    // public void rotateY()
    // {

    // }

    // [Test]
    // public void rotateZ()
    // {

    // }
}