using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;


public class ThumbnailIcons : MonoBehaviour, IInputClickHandler {

    private GazeManager hololensGaze;
    // Use this for initialization
    void Start () {
        //hololensGaze = gameObject.transform.parent.transform.parent.GetComponent<MenuPanel>().hololensGaze;
        hololensGaze = gameObject.transform.parent.transform.parent.transform.parent.GetComponent<ManagerMenu>().hololensGaze;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData) {
        // If we are gazing at manipulations button, then trigger manipulation.
        if (hololensGaze.HitObject.tag == "ThumbnailIcon") {
            // Retrieve game object from object generator.
            //gameObject.transform.parent.transform.parent.GetComponent<MenuPanel>().RequestObject(gameObject);
            gameObject.transform.parent.transform.parent.transform.parent.GetComponent<ManagerMenu>().RequestObject(gameObject);
            // Turn off menu bar to shift focus to imported game object.
            gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
            gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        }
    }
}
