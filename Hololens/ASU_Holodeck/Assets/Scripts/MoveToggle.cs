using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToggle : MenuToggle {


    public override void OnInputClicked(InputClickedEventData eventData) {
        // The parent of this menu game object is the menu billboard, and the parent of that is the game object
        // model we want to manipulate.
        toggleMove = !toggleMove;
        rootModel.GetComponent<MoveManipulation>().MoveManipulationEnabled = toggleMove;
        rootModel.GetComponent<RotationManipulation>().RotationManipulationEnabled = false;
        rootModel.GetComponent<ScaleManipulation>().ScaleManipulationEnabled = false;
        Debug.Log("Drag status:\t" + rootModel.GetComponent<MoveManipulation>().MoveManipulationEnabled);
    }
    

    public void Start() {
        // Rerieve parent of toggle object, which is gameobject of manipulation itself.
        // TODO: Fix null error.
        rootModel = gameObject.transform.parent.transform.parent.transform.parent.gameObject;
    }

    /**
     * When user looks away, it'll be after they tap, so we check that they tapped then we 
     * set the toggle to opposite value because they are now working on moving object.
     */
    public override void OnFocusExit() {
        //toggleMove = !toggleMove; 
    }
}
