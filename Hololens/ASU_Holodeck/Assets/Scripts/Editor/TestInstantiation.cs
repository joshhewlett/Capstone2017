using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestInstantiation : MonoBehaviour {

  [Test]
  public void CreateObject(){
    var TestObject = new GameObject();
    Assert.That(TestObject, !(Is.Null));
  }
}
