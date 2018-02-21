using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

/**
 * Class is assigned to the create button on tween menu for 3d model.
 * This script creates a keypoint prefab for the user to tap to place and perform manipulations
 * so a tween procedure (lerp) can be invoked in a sychronous manner.
 */ 
public class CreateKeypoint : MonoBehaviour, IInputClickHandler {
    private GameObject keypointPrefab;

    public void OnInputClicked(InputClickedEventData eventData) {
        // Instantiate keypoint with parent reference to tween menu item, this way
        // when the game object menu is active, the tweening objects are active, when tweening isn't active then
        // the respective game objects won't be active.
        GameObject kp = Instantiate(keypointPrefab, gameObject.transform.parent);
        // Ensure keypoint menu is not enabled until tap to place is complete.
        kp.transform.GetChild(0).gameObject.SetActive(false);
        kp.AddComponent<TapToPlace>();
        kp.GetComponent<TapToPlace>().IsBeingPlaced = true;
        kp.tag = "KeypointManipulationType";
        kp.AddComponent<SpatialManipulation>();
        kp.GetComponent<SpatialManipulation>().manipulationSelection = kp.transform.GetChild(0).gameObject;
        kp.AddComponent<KeypointManipulationMenu>();

    }

    // Use this for initialization
    void Start () {
        // Set gameobject keypoint prefab to be loaded from Resources folder.
        keypointPrefab = Resources.Load("Prefabs/Keypoint", typeof(GameObject)) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
