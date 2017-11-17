using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGameObject : MonoBehaviour, IInputClickHandler, IFocusable {

    [Tooltip("Color selection choice for changing object during runtime as a coroutine.")]
    public Color focusedColor;
    private Color unfocusedColor;
    public bool gameObjectSelected;
    /*public float fadeInTime = 1;            // Want separate values for fade in and fade out to serialize the state of the transition
    public float fadeOutTime = 1;           // via coroutine calls.*/

    // Default coloring and material
    private Material defaultMaterial;
    private Color defaultColor;

    private float progress;                 // Variable used for setting hue of color transition.
    private Coroutine coroutine;


    /**
     * Grabbing the Renderer assigned to this game object, and assigning the unfocused color to have its currently 
     * assigned material color, so we never overwrite this.
     */
    void Start() {
        defaultMaterial = GetComponent<Renderer>().material;
        gameObjectSelected = false;
        defaultColor = defaultMaterial.GetColor("_Color");
    }

    /**
     * First time we click we instantiate menu, second time we click it dissapears.
     * TODO: Make changes to have menu animate itself up and down based on...Gaze?.
     */
    public void OnInputClicked(InputClickedEventData eventData) {
        gameObjectSelected = !gameObjectSelected;
        if (gameObjectSelected) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
        }
        //StopAllCoroutines();
        //coroutine = StartCoroutine("InstantiateMenu");
    }
    

    /**
    * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
    * This method will start thread and change color back to 'highlighted' state since user looks at this game object.
    */
    public void OnFocusEnter() {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        /*StopAllCoroutines();
        coroutine = StartCoroutine("InstantiateMenu");*/
    }

    /**
     * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
     * This method will stop thread and return color back to original state since user looks away.
     */
    public void OnFocusExit() {
        // If we haven't selected the gameobject.
        if ((gameObjectSelected)) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
        }
        /*StopAllCoroutines();
        coroutine = StartCoroutine("InstantiateMenu");*/
    }

}
