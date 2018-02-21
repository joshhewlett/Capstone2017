using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ThumbnailSelection : MonoBehaviour, IInputClickHandler, IFocusable {

    private ModalMenuManager MenuManager;
    public GameObject thumbnailOutline;
    public bool outlineSelection;
    public bool displayThumbnailName;
    /*
     * When clicked, pass reference of thumbnail to our 
     * quick menu manager to interact with Web API.
     */ 
    public void OnInputClicked(InputClickedEventData eventData) {
        MenuManager.loadMenuItem(gameObject);
    }

    // Use this for initialization
    void Start () {
        outlineSelection = true;
        displayThumbnailName = false;
        thumbnailOutline.SetActive(false);
        MenuManager = gameObject.transform.parent.GetComponentInParent<ModalMenuManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Upon user gaze, outline thumbnail to give feeling of 
     * responsiveness.
     */ 
    public void OnFocusEnter() {
        thumbnailOutline.SetActive(true);
    }

    /*
     * Revert outline when user gazes away from thumbnail.
     */ 
    public void OnFocusExit() {
        thumbnailOutline.SetActive(false);
    }

}
