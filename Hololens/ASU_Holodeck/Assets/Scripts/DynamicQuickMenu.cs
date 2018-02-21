using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuickMenu : DynamicMenu {

    /**
     * Grabbing the Renderer assigned to this game object, and assigning the unfocused color to have its currently 
     * assigned material color, so we never overwrite this.
     */
    public override void Start() {
        defaultMaterial = GetComponent<Renderer>().material;
        highlightSelectionMaterial = Resources.Load("Materials/SubMenuOutline", typeof(Material)) as Material;
        menuOptions.SetActive(false);           // Set to false by default because edit menu must be toggled on.
    }
    
    /**
     * Additionally want to turn off children game objects.
     */ 
    public override void OnInputClicked(InputClickedEventData eventData) {
        //this.GetComponent<HandDraggable>().SetDragging(false);
        toggleMenuUpDown++;
        if (toggleMenuUpDown % 2 == 0) { 
            menuOptions.SetActive(false);
        }
        else {                                // otherwise trigger menu
            menuOptions.SetActive(true);
        }
    }

    /**
    * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
    * This method will start thread and change color back to 'highlighted' state since user looks at this game object.
    */
    public override void OnFocusEnter() {
    }

    /**
     * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
     * This method will stop thread and return color back to original state since user looks away.
     */
    public override void OnFocusExit() {
    }

}
