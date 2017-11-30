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
        if (toggleMenuUpDown % 2 == 0) {    // If not triggered stop
            // Here we turn off all menu options which will allow us to 
            // start with a fresh state if we close away quick menu and re-open it.
            // TODO: Fix this toggling.
            /*foreach (Transform child in menuOptions.transform) {
                child.gameObject.SetActive(false);
            }*/
            menuOptions.SetActive(false);
        }
        else {                                // otherwise trigger menu
            menuOptions.SetActive(true);
        }
    }

}
