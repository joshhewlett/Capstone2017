using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSelection : MonoBehaviour, IInputClickHandler, ISourceStateHandler, IInputHandler {

    public HandDraggable handDrag;
	// Use this for initialization
	void Start () {
        handDrag = GetComponent<HandDraggable>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("State of hand drag\t" + this.GetComponent<HandDraggable>().IsDraggingEnabled);
    }

    /**
     * Upon release.
     */ 
    public void OnInputUp(InputEventData eventData) {
        Debug.Log("input up");
        //handDrag.SetDragging(true);
        //this.GetComponent<HandDraggable>().SetDragging(true);
        //Debug.Log("State of hand drag\t" + this.GetComponent<HandDraggable>().IsDraggingEnabled);
    }

    public void OnInputDown(InputEventData eventData) {
        Debug.Log("User drag");
        handDrag.SetDragging(true);
        //throw new NotImplementedException();
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        //throw new System.NotImplementedException();
        handDrag.SetDragging(false);
        Debug.Log("User click");
    }

    public void OnSourceDetected(SourceStateEventData eventData) {
        throw new System.NotImplementedException();
    }

    public void OnSourceLost(SourceStateEventData eventData) {
        //throw new System.NotImplementedException();
        Debug.Log("Lost source");
    }
}
