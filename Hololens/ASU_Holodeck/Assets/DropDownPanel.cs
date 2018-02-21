using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class DropDownPanel : MonoBehaviour, IInputClickHandler {

    public Animation dropDownPanel;
    public GazeManager holoGaze;
    public GameObject thumbnails;
    public GameObject panelArrowNavigation;
    private bool droppedDown;

    public void OnInputClicked(InputClickedEventData eventData) {
        if (holoGaze.HitObject.CompareTag("QuickMenuHeader")) {
            if (!droppedDown) {
                dropDownPanel.Play("ObjectsPanelSlideDown");
            } else {
                dropDownPanel.Play("ObjectsPanelSlideUp");
            }
            droppedDown = !droppedDown;
        }        
    }

    // Use this for initialization
    void Start () {
        droppedDown = false;
        thumbnails.SetActive(droppedDown);
        panelArrowNavigation.SetActive(droppedDown);
    }
	
	// Update is called once per frame
	void Update () {
		// If menu is dropped down, enable thumbnails and 
        // interactive buttons.
        thumbnails.SetActive(droppedDown);
        panelArrowNavigation.SetActive(droppedDown);
	}
}
