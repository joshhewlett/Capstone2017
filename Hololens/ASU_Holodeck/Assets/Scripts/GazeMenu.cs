using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMenu : MonoBehaviour, IInputClickHandler, IFocusable {

    [Tooltip("This is the toggle game object that we want to air tap to invoke sub menu")]
    public GameObject menuOptions;          // This will instantiate the billboard 3d text.
    public int toggleMenuUpDown = 0;
    public bool interacting;
    //private Coroutine coroutine;


    /**
     * Grabbing the Renderer assigned to this game object, and assigning the unfocused color to have its currently 
     * assigned material color, so we never overwrite this.
     */
    public void Start() {
        menuOptions.SetActive(false);           // Set to false by default because edit menu must be toggled on.
        interacting = false;
    }

    /**
     * First time we click we instantiate menu, second time we click it dissapears.
     * TODO: Make changes to have menu animate itself up and down based on...Gaze?.
     */
    public void OnInputClicked(InputClickedEventData eventData) {
        menuOptions.SetActive(true);
        if (menuOptions.activeInHierarchy) {
            interacting = true;
        }
    }

    /**
    * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
    * This method will start thread and change color back to 'highlighted' state since user looks at this game object.
    */
    public void OnFocusEnter() {
        menuOptions.SetActive(true);
        Debug.Log("Gaze set:\t" + menuOptions.activeInHierarchy);
    }

    /**
     * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
     * This method will stop thread and return color back to original state since user looks away.
     */
    public void OnFocusExit() {
        if (!interacting) {
            menuOptions.SetActive(false);
            Debug.Log("Gaze set:\t" + menuOptions.activeInHierarchy);
        }
    }
}
