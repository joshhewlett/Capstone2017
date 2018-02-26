using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class BackPanelButton : MonoBehaviour, IInputClickHandler {

    public ModalMenuManager panelManager;

    public void OnInputClicked(InputClickedEventData eventData) {
        panelManager.backtracePanelView();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
