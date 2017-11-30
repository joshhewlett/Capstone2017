using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicMenu : MonoBehaviour, IInputClickHandler, IFocusable {
 
    [Tooltip("This is the toggle game object that we want to air tap to invoke sub menu")]
    public GameObject menuOptions;          // This will instantiate the billboard 3d text.
    [Space(10)]
    protected Material highlightSelectionMaterial;
    protected Material defaultMaterial;
    public int toggleMenuUpDown = 0;
    //private Coroutine coroutine;


    /**
     * Grabbing the Renderer assigned to this game object, and assigning the unfocused color to have its currently 
     * assigned material color, so we never overwrite this.
     */
    public virtual void Start () {
        defaultMaterial = GetComponent<Renderer>().material;
        // https://answers.unity.com/questions/13356/how-can-i-assign-materials-using-c-code.html
        //highlightSelectionMaterial = Resources.Load("Materials/Outline_Material", typeof(Material)) as Material;
        //unfocusedColor = defaultMaterial.GetColor("_Color");
        menuOptions.SetActive(false);           // Set to false by default because edit menu must be toggled on.
    }

    /**
     * First time we click we instantiate menu, second time we click it dissapears.
     * TODO: Make changes to have menu animate itself up and down based on...Gaze?.
     */ 
    public virtual void OnInputClicked(InputClickedEventData eventData) {
        //this.GetComponent<HandDraggable>().SetDragging(false);
        toggleMenuUpDown++;
        if (toggleMenuUpDown % 2 == 0) {    // If not triggered stop
            menuOptions.SetActive(false);
        } else {                                // otherwise trigger menu
            menuOptions.SetActive(true);
        }
    }


    /**
    * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
    * This method will start thread and change color back to 'highlighted' state since user looks at this game object.
    */
    public virtual void OnFocusEnter() {
        gameObject.GetComponent<Renderer>().material = highlightSelectionMaterial;
        /*StopAllCoroutines();
        coroutine = StartCoroutine("InstantiateMenu");*/
    }

    /**
     * Method impleneted from IFocusable interface. Retrieving data from GazeManager apart of InputManager.
     * This method will stop thread and return color back to original state since user looks away.
     */
    public virtual void OnFocusExit() {
        // If we aren't interacting with game object.
        if (!(menuOptions.activeInHierarchy)) {
            gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }
    }


    /*IEnumerator InstantiateMenu() {
        toggleMenuUpDown++;
        if (toggleMenuUpDown % 2 == 0) {    // If not triggered stop
            menuOptions.SetActive(false);
            //yield return new WaitForSeconds(5);
        } else {                                // otherwise trigger menu
            menuOptions.SetActive(true);
        }
        yield return null;
    }*/
}
