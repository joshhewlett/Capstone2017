using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Tests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToggle : MenuToggle {

    public override void OnInputClicked(InputClickedEventData eventData) {
        // The parent of this menu game object is the menu billboard, and the parent of that is the game object
        // model we want to manipulate.
        rootModel.GetComponent<RotationManipulation>().RotationManipulationEnabled= toggleRotate;
        rootModel.GetComponent<MoveManipulation>().MoveManipulationEnabled = false;
        rootModel.GetComponent<ScaleManipulation>().ScaleManipulationEnabled = false;
        Debug.Log("Drag status:\t" + rootModel.GetComponent<RotationManipulation>().RotationManipulationEnabled);
    }

    // Update is called once per frame
    void Update() {

    }

    public override void OnInputUp(InputEventData eventData) {
        //base.OnInputUp(eventData);
        toggleRotate = !toggleRotate;
    }

    public override void OnFocusEnter() {

    }

    /**
     * When user looks away, it'll be after they tap, so we check that they tapped then we 
     * set the toggle to opposite value because they are now working on moving object.
     */
    public override void OnFocusExit() {
        //toggleRotate = !toggleRotate;
    }
}
