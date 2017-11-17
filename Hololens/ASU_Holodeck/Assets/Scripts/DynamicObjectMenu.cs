using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectMenu : MonoBehaviour, IInputClickHandler, IFocusable {
 
    public Color focusedColor;
    private Color unfocusedColor;
    public GameObject menuOptions;          // This will instantiate the billboard 3d text.
    [Space(10)]
    public float fadeInTime = 1;            // Want separate values for fade in and fade out to serialize the state of the transition
    public float fadeOutTime = 1;           // via coroutine calls.
    public Material highlightSelectionMaterial;
    private Material defaultMaterial;
    public int toggleMenuUpDown = 0;

    private float progress;                 // Variable used for setting hue of color transition.
    private Coroutine coroutine;


    /**
     * Grabbing the Renderer assigned to this game object, and assigning the unfocused color to have its currently 
     * assigned material color, so we never overwrite this.
     */
    void Start () {
        defaultMaterial = GetComponent<Renderer>().material;
        //unfocusedColor = defaultMaterial.GetColor("_Color");
        menuOptions.SetActive(false);           // Set to false by default because edit menu must be toggled on.
    }

    /**
     * First time we click we instantiate menu, second time we click it dissapears.
     * TODO: Make changes to have menu animate itself up and down based on...Gaze?.
     */ 
    public void OnInputClicked(InputClickedEventData eventData) {
        //this.GetComponent<HandDraggable>().SetDragging(false);
        toggleMenuUpDown++;
        if (toggleMenuUpDown % 2 == 0) {    // If not triggered stop
            menuOptions.SetActive(false);
            //yield return new WaitForSeconds(5);
        } else {                                // otherwise trigger menu
            menuOptions.SetActive(true);
        }
        //StopAllCoroutines();
        //coroutine = StartCoroutine("InstantiateMenu");
    }


    /**
    * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
    * This method will start thread and change color back to 'highlighted' state since user looks at this game object.
    */
    public void OnFocusEnter() {
        gameObject.GetComponent<Renderer>().material = highlightSelectionMaterial;
        /*StopAllCoroutines();
        coroutine = StartCoroutine("InstantiateMenu");*/
    }

    /**
     * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
     * This method will stop thread and return color back to original state since user looks away.
     */
    public void OnFocusExit() {
        // If we aren't interacting with game object.
        if (!(menuOptions.activeInHierarchy)) {
            gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }
        /*StopAllCoroutines();
        coroutine = StartCoroutine("InstantiateMenu");*/
    }


    IEnumerator InstantiateMenu() {
        toggleMenuUpDown++;
        if (toggleMenuUpDown % 2 == 0) {    // If not triggered stop
            menuOptions.SetActive(false);
            //yield return new WaitForSeconds(5);
        } else {                                // otherwise trigger menu
            menuOptions.SetActive(true);
        }

        yield return null;
    }

}
