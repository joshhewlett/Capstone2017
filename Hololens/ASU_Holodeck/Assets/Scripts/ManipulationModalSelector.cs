using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ManipulationModalSelector : MonoBehaviour, IInputClickHandler {

    public bool menuItemSelected;
    public GameObject manipulatableObject;
    public GameObject previousMenuSelection;
    public GazeManager hololensGaze;

    [Tooltip("Turning items on and off within this menu itself.")]
    public GameObject ManipulationButton;
    [Tooltip("Game Object array list of menu items to toggle on/off whenever clicking this button.")]
    public GameObject HighlightButton;
    [Tooltip("Game Object array list of tween procedure.")]
    public GameObject DeleteButton;

    private enum MenuType {
        MainMenu,
        Manipulation,
        Highlight,
        Delete,
        NoObject
    };

    public void Update() {
    }
    

    // Update is called once per frame
    private void updateMenu(MenuType selection) {
        // Opposite of actual menu item selected because we will toggle buttons off to 
        // toggle manipulation icons on.
        switch (selection) {
            case MenuType.Manipulation:
                break;
            case MenuType.Highlight:
                break;
            case MenuType.Delete:
                Debug.Log("Delete attempt");
                Destroy(manipulatableObject);
                break;
            case MenuType.MainMenu:
                
                break;
        }
    
    }

    // Use this for initialization
    public void Start () {
        // By default menu item selection is true, upon pressing menu
        // item then our state changes.
        manipulatableObject = gameObject.transform.parent.transform.parent.gameObject;
        //previousMenuSelection = ManipulationButton;
        updateMenu(MenuType.MainMenu);
        // Start tap detection not enabled, grab it from Highest Parent with respect to object.
        gameObject.transform.parent.parent.GetChild(0).GetComponent<TapDetection>().enabled = false;
	}
    


    public void OnInputClicked(InputClickedEventData eventData) {
        // If we are gazing at manipulations button, then trigger manipulation.
        MenuType selection = MenuType.NoObject;
        // If when tapping, we are gazing at a menu button, turn previous menu selection text
        // to white coloring.
        switch (hololensGaze.HitObject.name) {
            case "ManipulationButton":
                // Set previous button selection white, set this button to current selected button, and color red.
                if (previousMenuSelection != hololensGaze.HitObject) {
                    previousMenuSelection.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color = Color.white;
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color =
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color == Color.white ?
                    Color.red :
                    Color.white;
                } else {
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color =
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color == Color.white ?
                    Color.red :
                    Color.white;
                }
                gameObject.transform.parent.parent.GetChild(0).GetComponent<TapDetection>().enabled = true;
                previousMenuSelection = ManipulationButton;
                selection = MenuType.Manipulation;
                break;
            case "HighlightButton":
                if (previousMenuSelection != hololensGaze.HitObject) {
                    previousMenuSelection.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color = Color.white;
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color =
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color == Color.white ?
                    Color.red :
                    Color.white;
                }
                else {
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color =
                    hololensGaze.HitObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>().color == Color.white ?
                    Color.red :
                    Color.white;
                }
                // Disable tap detection, and set manipulation mode to none.
                gameObject.transform.parent.parent.GetChild(0).GetComponent<TapDetection>().enabled = false;
                gameObject.transform.parent.parent.GetChild(0).GetComponent<SpatialManipulation>().manipulationMode = SpatialManipulation.ManipulationType.None;
                
                previousMenuSelection = HighlightButton;
                selection = MenuType.Highlight;
                break;
        }
        updateMenu(selection);
    }
}
