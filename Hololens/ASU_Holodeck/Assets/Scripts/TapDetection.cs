using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class TapDetection : MonoBehaviour, IInputClickHandler {
    public float timeStarted;
    public int quantityOfTaps;
    public bool timerActive;
    /* Gaze Manager is where the Hololens is staring, we can use this to
     * detect what object the user is gazing at, in this case a manipulatable object
     * so that we may grab the SpatialManipulator script and set the proper action
     * based on # of user taps whilst gazing.
     */ 
    public GazeManager hololensGaze;
    /*
     * Via Hololens API, listen to input events for Air Taps.
     * As soon as a air tap happened, if its the initial one,
     * enable our time, and track when the firs tap has happened.
     * 
     * Increment number of taps thereafter to know when the user
     * has conntinued to append taps from initial.
     */ 
    public void OnInputClicked(InputClickedEventData eventData) {
        if (!timerActive) {
            timerActive = true;
            timeStarted = Time.time;
        }
        quantityOfTaps++;
        Debug.Log("Your input clicked quantity:\t" + eventData.TapCount);
    }

    // Use this for initialization
    void Start() {
        timerActive = false;
        quantityOfTaps = 0;
        timeStarted = 0f;
        // Assign gaze manager reference from objects collection (2 parents).
        hololensGaze = gameObject.transform.parent.parent.gameObject.GetComponent<GazeManagerReference>().hololensGaze;
    }

    /*
     * Per frame detect whether the user has tapped multiple times to toggle between
     * manipulation modes.
     */ 
    public void Update() {
        // If toggled for manipulations.
        if (gameObject.GetComponent<DynamicObjectMenu>().menuOptions.gameObject.GetComponent<ToggleManipulationSelection>().interactiveToggleSelection) {
            // If timer is active and quantity of taps fits time threshold
            if (quantityOfTaps > 0 && timerActive && (Time.time - timeStarted > 1f)) {
                // Otherwise if we pass time threshold set everything false.
                switch (quantityOfTaps) {
                    case 1:
                        // TODO: Disable enumerate manipulation mode when interacting with another object,
                        // so possibly keep reference to previously manipulated object.
                        Debug.Log("Move mode");
                        // Set the game object to move manipulation mode.
                        Debug.Log("Name:\t" + hololensGaze.HitObject.name);
                        if (hololensGaze.HitObject.CompareTag("ImportedModel")) {
                            hololensGaze.HitObject.GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.Move;
                        } else {
                            hololensGaze.HitObject.transform.parent.GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.Move;
                        }
                        break;
                    case 2:
                        Debug.Log("Rotate Mode");
                        // Set the game object to rotate manipulation mode.
                        if (hololensGaze.HitObject.CompareTag("ImportedModel")) {
                            hololensGaze.HitObject.GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.Rotate;
                        }
                        else {
                            hololensGaze.HitObject.transform.parent.GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.Rotate;
                        }
                        break;
                    case 3:
                        Debug.Log("Scale Mode");
                        // Set the game object to scale manipulation mode.
                        if (hololensGaze.HitObject.CompareTag("ImportedModel")) {
                            hololensGaze.HitObject.GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.Scale;
                        }
                        else {
                            hololensGaze.HitObject.transform.parent.GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.Scale;
                        }
                        break;
                    default:
                        // Anything else past 3, consider as disabling manipulations on corresponding object.
                        hololensGaze.HitObject.GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.None;
                        break;
                }
                //Debug.Log("Quantity of taps after 1 second:\t" + quantityOfTaps);
                // Reset variables tracking tap count.
                timerActive = false;
                quantityOfTaps = 0;
            }
            // Debug.Log("Quantity of taps:\t" + quantityOfTaps);
        }
    }
}
