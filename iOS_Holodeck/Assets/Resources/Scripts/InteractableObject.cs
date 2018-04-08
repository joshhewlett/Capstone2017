using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using UnityStandardAssets.CrossPlatformInput;


public class InteractableObject : MonoBehaviour {

	public Text debugger;
	public bool movementEnabled, rotationEnabled, newTouch;
	// Position transform of game object for manipulation.
	public float x_coordinate, 
				y_coordinate, 
				z_coordinate, 
				x_rotation, 
				y_rotation,
				scale_analog,
				touchTime;

	// Use this for initialization
	void Start () {
		movementEnabled = rotationEnabled = newTouch = false;
	}
	
	// Update is called once per frame
	void Update () {	
		checkGaze ();	
//		checkDoubleTap ();
		int d_tapOccurence = 0;
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				// Per finger, increment occurence of double tap.
				if (Input.GetTouch (i).tapCount == 2) {
					d_tapOccurence++;
				}
			}
		}
		// if 2 finger double tap.
		if (d_tapOccurence == 2) {
//			debugger.text = (debugger.text == "DOUBLE TAP") ? "NOT" : "DOUBLE TAP";
			// toggle rotation
			rotationEnabled = !rotationEnabled;
			movementEnabled = false;
//			movementEnabled = rotationEnabled ? false : true;
		}

		if (!rotationEnabled && movementEnabled) {
			checkJoyStickManipulation ();
		} else if (rotationEnabled) {
			checkRotationScaleManipulation ();
		}
//		debugger.text = gameObject.name + ":\t"  + movementEnabled;
	}


	public void checkRotationScaleManipulation() {
		Vector3 tempScale = gameObject.transform.localScale;
		Vector3 tempRotation = gameObject.transform.localEulerAngles;

		x_rotation = (CrossPlatformInputManager.GetAxis ("Horizontal") * 0.2f);
		y_rotation = (CrossPlatformInputManager.GetAxis ("Vertical") * 0.2f);
		scale_analog = (CrossPlatformInputManager.GetAxis ("S_Horizontal") * 0.2f);

		// 1:1:1 scaling
		tempScale.x += (scale_analog); 
		tempScale.y += (scale_analog); 
		tempScale.z += (scale_analog); 

		tempRotation.y += (y_rotation);
		tempRotation.x += (x_rotation);

		gameObject.transform.localScale = tempScale;
		gameObject.transform.localRotation = Quaternion.Euler(tempRotation.x, tempRotation.y, tempRotation.z);
	}

	public void checkJoyStickManipulation() {
		x_coordinate = (CrossPlatformInputManager.GetAxis ("Horizontal") * 0.03f);
		y_coordinate = (CrossPlatformInputManager.GetAxis ("S_Vertical") * 0.03f);
		z_coordinate = (CrossPlatformInputManager.GetAxis ("Vertical") * 0.03f);
		// Move game object up or forward and back, up and down will be separate joystick.
		Vector3 movement = new Vector3 (x_coordinate, y_coordinate, z_coordinate);
		//		debugger.text = "JOYSTICKS: " + x_coordinate + "\nY: " + y_coordinate + " Z: " + z_coordinate; 
		transform.localPosition += movement;	
	}


	public void checkGaze() {
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;

		if (Physics.Raycast (headPosition, gazeDirection, out hitInfo)) {
			// If we are colliding with a poly import, and it's the respective game object this
			// script is assigned too.
//			if (hitInfo.collider.gameObject.CompareTag ("PolyImport")) {				
			if (hitInfo.collider.gameObject.CompareTag ("PolyImport") &&
			    hitInfo.collider.gameObject == gameObject) {				
				//				checkUserDrag ();
				var touch = Input.GetTouch (0);
				// If touching poly imported object, allow for joystick manipulation.
				if (touch.phase == TouchPhase.Began) {
					movementEnabled = !movementEnabled;
					// Enable joysticks by grabbing joystick reference from ARKit script.
				}
			} else {
//				// Turn off joystick and disable manipulation when selecting something else
//				// for interaction.
//				var touch = Input.GetTouch (0);
//				// If touching poly imported object, allow for joystick manipulation.
//				if (touch.phase == TouchPhase.Began) {
//					movementEnabled = false;
//					// Enable joysticks by grabbing joystick reference from ARKit script.
//				}
			}
		}
	}

	/*
	 * Screen and Viewport represent same area - on screen, but they have different coordinate systems (IIRC):
	 * Screen: from [0,0] to [Screen.width, Screen.height]
	 * Viewport: from [0,0] to [1,1]
	 * World point is a point in world space.
	 */ 
	void checkUserDrag() {
		var touch = Input.GetTouch (0);
		if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
			// Maybe do worldspace and convert to local space with world space touch coordinates,
			// local space to hit object.
			Vector3 touchedPos = Camera.main.ScreenToViewportPoint (touch.position);
//			debugger.text = "DRAG POS:" + touchedPos.x + "\n" + touchedPos.y + " " + touchedPos.z;
			// lerp and set the position of the current object to that of the touch, but smoothly over time.
			// TODO: Don't lerp, append x y z results to transform object itself to rotate around.
			// TODO: Add touch gesture to elevate object up and down.
			transform.localPosition = Vector3.Lerp(transform.localPosition, touchedPos, Time.deltaTime);

		}
	}
}
