using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class MenuBar : MonoBehaviour, IInputClickHandler {

    public bool isVisible;
    public GazeManager hololensGaze;
    public GameObject MenuPanel;

    [Tooltip("Turning items on and off within this menu itself.")]
    public GameObject[] MenuSelectionOptions;
    [Tooltip("Game Object array list of menu items to toggle on/off whenever clicking this button.")]
    public GameObject[] ManipulationSelectionOptions;

    // Use this for initialization
    void Start() {
        isVisible = false;
        // Ensure panel is turned off when menu is instantiated.
        MenuPanel.SetActive(false);
        updateMenu();
    }

    public void Awake() {
        
    }

    // Update is called once per frame
    public void updateMenu() {
        // Opposite of actual menu item selected because we will toggle buttons off to 
        // toggle manipulation icons on.
        
    }

    /**
     * Rather than having a visible bool trait, we can manually turn the visibility on or off based on user input.
     * To check if our panel is active, we reference the game object (assigned before runtime to this script)
     * and call gameObject.isActiveInHeirarchy
     * 
     * The method uses the hololens gaze to determine if we have a game object collision with the raycast gaze.
     * This in turn will give us the game object collisions information, such as the name of the object.
     */
    public void OnInputClicked(InputClickedEventData eventData) {

        // Set menu panel in mode of object selection.
        if (hololensGaze.HitObject.name == "Object Button") {
            MenuPanel.SetActive(true);
            // set menu panel mode here
        } else if (hololensGaze.HitObject.name == "Slide Button") {
            MenuPanel.SetActive(true);
            // set menu panel mode here
        } else if (hololensGaze.HitObject.name == "Presentation Button") {
            MenuPanel.SetActive(true);
            // set menu panel mode here
        } else if (hololensGaze.HitObject.name == "Cancel Button") {
            MenuPanel.SetActive(false);
        }
    }
}
