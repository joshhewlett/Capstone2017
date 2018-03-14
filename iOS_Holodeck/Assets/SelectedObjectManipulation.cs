using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityStandardAssets.CrossPlatformInput;

/*
 * Receive object from Interactable Object script
 * to act manipulations upon.
 */ 
public class SelectedObjectManipulation : MonoBehaviour {

	public GameObject selectedModel;
	public bool modelSelected;
	// Position transform of game object for manipulation.
	public float x_coordinate, 
				y_coordinate, 
				z_coordinate, 
				x_rotation, 
				y_rotation,
				scale_analog;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// If toggled spatial manipulation,
		// operate on assigned game object.
		if (modelSelected) {
			checkJoyStickManipulation ();
		}
	}

	public void checkJoyStickManipulation() {
		x_coordinate = (CrossPlatformInputManager.GetAxis ("Horizontal") * 0.03f);
		y_coordinate = (CrossPlatformInputManager.GetAxis ("S_Vertical") * 0.03f);
		z_coordinate = (CrossPlatformInputManager.GetAxis ("Vertical") * 0.03f);
		// Move game object up or forward and back, up and down will be separate joystick.
		Vector3 movement = new Vector3 (x_coordinate, y_coordinate, z_coordinate);
		//		debugger.text = "JOYSTICKS: " + x_coordinate + "\nY: " + y_coordinate + " Z: " + z_coordinate; 
		selectedModel.transform.localPosition += movement;	
	}
}
