using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

// TODO: Consider implementing IFocusable for OnFocusEnter to determine
// when user is gazing at menu and gazing away, rather than using the HitObject.
public class MoveMenu : MonoBehaviour, IInputHandler {

    public GazeManager hololensGaze;
    private bool checkForMenuMoveTap;


    /*
     * Ensure when input is down, we enable the ability to move,
     * since MoveManipulation script uses the IManipulationHandler interface.
     * The HandDraggable script uses IInputHandler so we can't enable them using the
     * same interface twice, and using Gaze won't work because that will
     * prevent the user from dragging in occurences where the menu moves in their line
     * of sight during dragging.
     * 
     * TODO: Add animation/outline to notify user they are moving menu.
     */ 
    public void OnInputDown(InputEventData eventData) {
        // If user isn't staring at menu, don't toggle tap to place.
        if (!checkForMenuMoveTap)
        {
            return;
        }
        // Enalbe user for tapping to place the game object on initial import.
        gameObject.GetComponent<MoveManipulation>().MoveManipulationEnabled = true;
    }

    /*
     * If user has released Manipulation gesture, disable the TapToPlace script
     */ 
    public void OnInputUp(InputEventData eventData) {
        // If user isn't staring at menu, don't toggle tap to place.
        if (!checkForMenuMoveTap) {
            return;
        }
        // Enalbe user for tapping to place the game object on initial import.
        gameObject.GetComponent<MoveManipulation>().MoveManipulationEnabled = false;
        //gameObject.GetComponent<HandDraggable>().IsDraggingEnabled = !(gameObject.GetComponent<HandDraggable>().IsDraggingEnabled);
    }

    /* Use start instead of awake because we want the user to select a 
     * gameobject for interaction in order to enable the gazemanager reference
     */
    void Start () {
        // Grab Hololens GazeManager reference from the ObjectsCollection reference.
        hololensGaze = gameObject.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<GazeManagerReference>().hololensGaze;
        checkForMenuMoveTap = false;
        gameObject.GetComponent<MoveManipulation>().MoveManipulationEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hololensGaze.HitObject.CompareTag("ObjectMenu")) {
            Debug.Log("Is staring at menu");
            checkForMenuMoveTap = true;
            } else {
            checkForMenuMoveTap = false;
        }
	}
}
