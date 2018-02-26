using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using PolyToolkit;
using UnityEngine;

public class GooglePolyButton : MonoBehaviour, IInputClickHandler {

    public ModalMenuManager panelManager;
    public PolyManager pManager;

    public void OnInputClicked(InputClickedEventData eventData) {
        panelManager.QuickMenuPanel = ModalMenuManager.PanelModes.Poly;
        panelManager.transitionModalSegue(panelManager.QuickMenuPanel);
        pManager.displayThumbnailSet();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
