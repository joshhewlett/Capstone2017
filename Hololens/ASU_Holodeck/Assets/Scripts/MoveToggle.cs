using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToggle : MenuToggle {


    public override void OnInputClicked(InputClickedEventData eventData) {
        // The parent of this menu game object is the menu billboard, and the parent of that is the game object
        // model we want to manipulate.
        rootModel.GetComponent<MoveManipulation>().MoveManipulationEnabled = toggleMove;
        rootModel.GetComponent<RotationManipulation>().RotationManipulationEnabled = false;
        rootModel.GetComponent<ScaleManipulation>().ScaleManipulationEnabled = false;
        Debug.Log("Drag status:\t" + rootModel.GetComponent<MoveManipulation>().MoveManipulationEnabled);
    }

	// Update is called once per frame
	void Update () {

    }

    public void Start() {
        // Rerieve parent of toggle object, which is gameobject of manipulation itself.
        // TODO: Fix null error.
        rootModel = gameObject.transform.parent.transform.parent.transform.parent.gameObject;
    }
    
    public override void OnInputUp(InputEventData eventData) {
        //base.OnInputUp(eventData);
        toggleMove = !toggleMove;
    }

    public virtual void OnInputDown(InputEventData eventData) {
        // The parent of this menu game object is the menu billboard, and the parent of that is the game object
        // model we want to manipulate.
        //rootModel.GetComponent<HandDraggable>().SetDragging(toggleMove);
        //Debug.Log("Drag status:\t" + rootModel.GetComponent<HandDraggable>().IsDraggingEnabled);
    }

    /**
     * When user looks away, it'll be after they tap, so we check that they tapped then we 
     * set the toggle to opposite value because they are now working on moving object.
     */
    public override void OnFocusExit() {
        //toggleMove = !toggleMove; 
    }
}
