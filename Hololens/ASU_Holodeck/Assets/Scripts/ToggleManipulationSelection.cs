using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Examples.InteractiveElements;

public class ToggleManipulationSelection : MonoBehaviour {

    public bool interactiveToggleSelection;
    [Tooltip("Menu Option Items")]
    public GameObject[] menuOptions;

	// Use this for initialization
	void Start () {
        // Iterate through all buttons in the menu canvas and turn them off at beginning.
        foreach (GameObject menuItem in menuOptions) {
            menuItem.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //menuOptions = GameObject.FindGameObjectsWithTag("MenuOption");
       // Iterate through all respectively assigned gameobjects as children to set them active, or inactive dependiing on toggle state.
        interactiveToggleSelection = this.GetComponent<InteractiveToggle>().HasSelection;
        foreach(GameObject menuItem in menuOptions) {
            menuItem.SetActive(interactiveToggleSelection);
        } 
    }
}
