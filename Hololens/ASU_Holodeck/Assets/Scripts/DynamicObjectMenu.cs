using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectMenu : DynamicMenu
{

    /**
     * Grabbing the Renderer assigned to this game object, and assigning the unfocused color to have its currently 
     * assigned material color, so we never overwrite this.
     */
    public void Update() {
        //Debug.Log("State:\t" + interacting);
    }

}
