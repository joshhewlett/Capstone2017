using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ManipulationModalSelector : MonoBehaviour, IInputClickHandler, IInputHandler {

    public bool menuItemSelected;
    public GazeManager hololensGaze;

    [Tooltip("Turning items on and off within this menu itself.")]
    public GameObject[] MenuSelectionOptions;
    [Tooltip("Game Object array list of menu items to toggle on/off whenever clicking this button.")]
    public GameObject[] ManipulationSelectionOptions;

    public virtual void OnInputDown(InputEventData eventData) {
        
    }

    /// <summary>
    /// Upon release of air tap gesture on respective menu item game object
    /// menu selection will be toggled and update method will appropriately render
    /// respective menu items.
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnInputUp(InputEventData eventData) {
        //menuItemSelected = !menuItemSelected;
    }

    // Update is called once per frame
    public void updateMenu() {
        // Opposite of actual menu item selected because we will toggle buttons off to 
        // toggle manipulation icons on.
        foreach (GameObject menuItem in ManipulationSelectionOptions) {
            menuItem.SetActive(!menuItemSelected);
        }
        foreach (GameObject menuItem in MenuSelectionOptions) {
            menuItem.SetActive(menuItemSelected);
        }
    }
    // Use this for initialization
    public virtual void Start () {
        // By default menu item selection is true, upon pressing menu
        // item then our state changes.
        menuItemSelected = true;
        updateMenu();
	}

    public virtual void Awake() {
        menuItemSelected = true;
    }


    public void OnInputClicked(InputClickedEventData eventData)
    {
        // If we are gazing at manipulations button, then trigger manipulation.
        if (hololensGaze.HitObject.name == "ManipulationButton")
        {
            menuItemSelected = false;
        } else if (hololensGaze.HitObject.name == "BackButton")
        {
            menuItemSelected = true;
        }
        updateMenu();

    }
}
