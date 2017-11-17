using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Examples.InteractiveElements;

public class NegateToggleCancelation : MonoBehaviour {

    public GameObject toggleSwitch;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // If toggle is enalbed, then edit menu is enabled. With this, we will, every frame,
        // ensure that the toggle menu is always on and enabled.
        if (toggleSwitch.GetComponent<InteractiveToggle>().HasSelection) {
            toggleSwitch.SetActive(true);
        }
    }
}
