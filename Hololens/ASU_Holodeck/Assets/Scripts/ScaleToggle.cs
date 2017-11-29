using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Tests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToggle : MenuToggle
{

    public override void OnInputClicked(InputClickedEventData eventData)
    {
        // The parent of this menu game object is the menu billboard, and the parent of that is the game object
        // model we want to manipulate.
        toggleScale = !toggleScale;
        rootModel.GetComponent<ScaleManipulation>().ScaleManipulationEnabled = toggleScale;
        rootModel.GetComponent<MoveManipulation>().MoveManipulationEnabled = false;
        rootModel.GetComponent<RotationManipulation>().RotationManipulationEnabled = false;
        Debug.Log("Drag status:\t" + rootModel.GetComponent<ScaleManipulation>().ScaleManipulationEnabled);
    }

    /**
     * When user looks away, it'll be after they tap, so we check that they tapped then we 
     * set the toggle to opposite value because they are now working on moving object.
     */
    public override void OnFocusExit()
    {
        //toggleRotate = !toggleRotate;
    }
}
