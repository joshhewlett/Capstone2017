using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ManipulationGazeSelection : MonoBehaviour, IFocusable, IInputClickHandler {

    // Variable to reference parent manipulation object, checks if we stared at a edit option,
    // if so we prohibit our gaze from accidently selecting another edit mode unless intended.
    public bool selectedManipulationType, gazeAway;
    public float gazeTime;
    public GazeManager hololensGaze;
    /**
    * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
    * This method will start thread and change color back to 'highlighted' state since user looks at this game object.
    */
    public virtual void OnFocusEnter() {
        Debug.Log("Gazing");
        gazeTime = Time.time;
    }

    /**
     * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
     * This method will stop thread and return color back to original state since user looks away.
     */
    public virtual void OnFocusExit() {
        // Reset gaze time when looking away.
        gazeTime = 0f;
        gazeAway = !gazeAway;
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        // Iterate through all children objects of manipulation options and change text to white 
        // so we can override selection with user tap.
        foreach(Transform child in gameObject.transform.parent) {
            child.gameObject.GetComponent<TextMesh>().color = Color.white;
        }
        if (gameObject.CompareTag("KeypointManipulationType")) {
            gameObject.transform.parent.transform.parent.gameObject.GetComponent<SpatialManipulation>().selectedEditMode = false;
        }
        else {
            gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetChild(0).gameObject.GetComponent<SpatialManipulation>().selectedEditMode = false;
        }
        gameObject.GetComponent<TextMesh>().color = gameObject.GetComponent<TextMesh>().color == Color.red ? Color.white : Color.red;
        gazeTime = 0f;
    }

    // Grab reference to gaze manager by gazemanager reference assigned to object collection,
    // this is done to overcome high cost of iterating through root objects
    // to find input manager.
    void Start () {
        gazeTime = 0f;
        selectedManipulationType = false;
        gazeAway = false;
        if (gameObject.CompareTag("KeypointManipulationType")) {
            hololensGaze = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<GazeManagerReference>().hololensGaze;
        }
        else {
            hololensGaze = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<GazeManagerReference>().hololensGaze;
        }
	}

    /*
     * Spatial Manipulator script attached to respective game object 
     * will check every frame for one of the menu selections to be colored red, hence
     * we check if the selectedManipulationType is false or this object is red and stared at.
     */ 
    void Update() {
        if (gameObject.CompareTag("KeypointManipulationType")) {
            selectedManipulationType = gameObject.transform.parent.transform.parent.gameObject.GetComponent<SpatialManipulation>().selectedEditMode;
        } else {
            // Reach up 4 parents and grab the first child of the 4th parent. This will be the model object.
            // TODO: Could possibly replace this with recursive calls checking object tags?
            selectedManipulationType = gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<SpatialManipulation>().selectedEditMode;
        }

        if (hololensGaze.HitObject == gameObject) {
            // If gazing for more than 1 seconds at manipulation mode.
            if (selectedManipulationType == false || gameObject.GetComponent<TextMesh>().color == Color.red) {
                if (Time.time - gazeTime > 1.5f) {
                    gazeTime = Time.time;       // Reset time and assign corresponding color toggle.
                    if (!gazeAway) {
                        gameObject.GetComponent<TextMesh>().color = Color.red;
                    } else {
                        gameObject.GetComponent<TextMesh>().color = Color.white;
                    }
                    //gameObject.GetComponent<TextMesh>().color = gameObject.GetComponent<TextMesh>().color == Color.red ? Color.white : Color.red;
                }
            }
        }
    }
}
