using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class SlidesThumbnailSelection : MonoBehaviour, IInputClickHandler {
    private CapstoneSlideManager capSlideManager;

    public void OnInputClicked(InputClickedEventData eventData) {
        // TODO: Load presentation ID for grabbing slides.
        // Subtract 1 because front end UI is not 0 index based,
        // whereas backend is.
        capSlideManager.LoadSlide((int.Parse(gameObject.transform.GetChild(0).GetComponent<TextMesh>().text) - 1));
    }

    // Use this for initialization
    void Start() {
        // Parente x 2 is the QuickMenu object itself, which contains our manager scripts
        // for interfacing with web api.
        capSlideManager = gameObject.transform.parent.parent.parent.GetComponentInParent<CapstoneSlideManager>();
    }

    // Update is called once per frame
    void Update() {

    }
}
