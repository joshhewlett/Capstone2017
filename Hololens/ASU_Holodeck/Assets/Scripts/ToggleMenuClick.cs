using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ToggleMenuClick : MonoBehaviour, IInputHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputDown(InputEventData eventData) {
        
    }

    public void OnInputUp(InputEventData eventData) {
        this.GetComponent<HandDraggable>().SetDragging(false);
    }
}
