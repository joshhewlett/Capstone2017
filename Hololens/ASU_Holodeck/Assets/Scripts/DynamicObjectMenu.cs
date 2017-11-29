using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectMenu : DynamicMenu {

    /**
     * Grabbing the Renderer assigned to this game object, and assigning the unfocused color to have its currently 
     * assigned material color, so we never overwrite this.
     */
    public virtual void Start() {
        defaultMaterial = GetComponent<Renderer>().material;
        // https://answers.unity.com/questions/13356/how-can-i-assign-materials-using-c-code.html
        highlightSelectionMaterial = Resources.Load("Materials/Outline_Material", typeof(Material)) as Material;
        //unfocusedColor = defaultMaterial.GetColor("_Color");
        menuOptions.SetActive(false);           // Set to false by default because edit menu must be toggled on.
    }
    
}
