using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class KeypointManipulationMenu : MonoBehaviour, IInputClickHandler {

    private GameObject keypointMenu;

    public void Start() {
        // Menu is only child game object of keypoint
        // so like this we can grab reference to it and toggle it on off when tapping.
        keypointMenu = transform.GetChild(0).gameObject;
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        // Toggle menu on/off upon user tap.
        keypointMenu.SetActive(!keypointMenu.activeInHierarchy);
    }
}
