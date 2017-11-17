using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggle : MonoBehaviour, IInputClickHandler, IInputHandler, IFocusable {
    // First time tapping icon will invoke true for respective togggle choices.
    protected bool toggleMove = false;
    protected bool toggleRotate = false;
    protected bool toggleScale = false;
    public GameObject rootModel;

    /**
     * Method made virtual so we can extend this class and override method behaviour.
     */ 
    public virtual void OnInputClicked(InputClickedEventData eventData) {
    }

    // Use this for initialization
    void Start() {
        rootModel = gameObject.transform.parent.transform.parent.gameObject;
    }

    public virtual void OnFocusEnter() {
    }

    /**
     * When user looks away, it'll be after they tap, so we check that they tapped then we 
     * set the toggle to opposite value because they are now working on moving object.
     */
    public virtual void OnFocusExit() {
    }

    public virtual void OnInputDown(InputEventData eventData) {
    }

    public virtual void OnInputUp(InputEventData eventData) {
    }
}
