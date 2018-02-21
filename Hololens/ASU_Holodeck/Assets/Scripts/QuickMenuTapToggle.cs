using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

/*
 * Detect when user taps in blank space to toggle quick menu using
 * IInputHandler interface for listening on input events,
 * additionally referencing InputManager GazeManager
 * to know what the user is looking at.
 */ 
public class QuickMenuTapToggle : MonoBehaviour, IInputHandler {

    public GameObject quickMenuReference;
    public  GazeManager hololensGaze;
    private bool quickMenuToggled;

    /*
     * Detection is invoked on first detected air tap asychronous to 
     * current frame.
     */ 
    public void OnInputDown(InputEventData eventData) {
        // If user isn't gazing at a object of interest, there intent will be to toggle quick menu.
        // Not a fan of checking for null, but hololens FocusManager script has a method
        // TryGetFocusedObject which is suggested from GazeManager for determining if user is
        // staring at actual object. That method invokes a nullable type method returning a 
        // FocusDetails struct. 
        //if (hololensGaze.HitObject == null) {
        if (!(Physics.Raycast(hololensGaze.GazeOrigin, hololensGaze.GazeNormal, 15))) {
            quickMenuToggled = !quickMenuToggled;
            quickMenuReference.SetActive(quickMenuToggled);
        }
    }

    public void OnInputUp(InputEventData eventData) {
        
    }

    // Use this for initialization
    public void Awake () {
        quickMenuToggled = true;
        quickMenuReference.SetActive(true);
        hololensGaze = gameObject.GetComponent<GazeManager>();
	}
	
}
