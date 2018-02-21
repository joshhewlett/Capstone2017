using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalMenuManager : MonoBehaviour {

    public TextMesh quickMenuHeader;
    public GameObject thumbnails;
    public GameObject backButton;
    public GameObject GooglePoly;
    public PolyManager polyManager;
    public Material thumbnailPrefab;
    public CapstoneSlideManager lectureManager;

    public enum PanelModes {
        Presentations,
        Slides,
        Objects,
        Poly
    };

    public PanelModes QuickMenuPanel;

    // Use this for initialization
    void Start () {
        thumbnailPrefab = Resources.Load("Materials/Thumbnail", typeof(Material)) as Material;
        QuickMenuPanel = PanelModes.Presentations;
        backButton.SetActive(false);
        GooglePoly.SetActive(false);
        polyManager.enabled = false;
	}

    public void backtracePanelView() {
        // Go back a state in the enum.
        QuickMenuPanel = QuickMenuPanel - 1;
        transitionModalSegue(QuickMenuPanel);
    }


    /**
     * Method should take in thumbnail pressed as parameter and transition it 
     * to the next state of the panel for rendering.
     * This method will read thumbnail information based on state of panel, and do respective
     * calls/interactions with BackEnd Manager scripts via API Hook calls.
     */
    public void loadMenuItem(GameObject thumbnail) {
        switch(QuickMenuPanel) {
            case PanelModes.Presentations:
                // Extract text from thumbnail and use to load presentaiton.
                string presentationQuery = thumbnail.GetComponentInChildren<TextMesh>().text;
                lectureManager.LoadPresentation(presentationQuery);
                // Transition to next state.
                QuickMenuPanel = PanelModes.Slides;
                break;
            case PanelModes.Slides:
                // Function will extract the text from the thumbnail
                // and use it for method calls on api endpoints.
                string slideQuery = thumbnail.GetComponentInChildren<TextMesh>().text;
                // Subtract 1 because JSON data from Web API is 0 indexed, whereas
                // frontend UI is not.
                lectureManager.LoadSlide((int.Parse(slideQuery) - 1));
                QuickMenuPanel = PanelModes.Objects;
                break;
            case PanelModes.Objects:

                break;
            case PanelModes.Poly:

                break;
        }
        transitionModalSegue(QuickMenuPanel);
    }

    /**
     * Responsible for invoking animation of sweeping thumbnails, and 
     * changing header text and thumbnail icons to match new panel page.
     * 
     * TODO: Address backbutton being pressed.
     */ 
    public void transitionModalSegue(PanelModes newPanelMode) {
        switch (QuickMenuPanel) {
            case PanelModes.Presentations:
                quickMenuHeader.text = "PRESENTATION";
                backButton.SetActive(false);
                GooglePoly.SetActive(false);
                foreach (Transform thumbnail in thumbnails.transform) {
                    thumbnail.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.79f, 0.56f, 0.28f, 1));
                }
                break;
            case PanelModes.Slides:
                quickMenuHeader.text = "SLIDES";
                backButton.SetActive(true);
                GooglePoly.SetActive(false);
                // change thumbnail colors to acknolwedge difference
                // in thumbnails for user, iterate through thumbnails.
                foreach(Transform thumbnail in thumbnails.transform) {
                    // Re-assign material since going to poly/object menu changes thumbnail 2d texture.
                    thumbnail.gameObject.GetComponent<Renderer>().material = thumbnailPrefab;
                    thumbnail.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.6f, 0.19f, 0.19f, 1));
                }

                break;
            case PanelModes.Objects:
                quickMenuHeader.text = "OBJECTS";
                GooglePoly.SetActive(true);
                polyManager.enabled = false;
                // change thumbnails to be 2d texture images.
                foreach (Transform thumbnail in thumbnails.transform) {
                    // Disable highlighting of items when browsing models, rather display names on gaze.
                    thumbnail.gameObject.GetComponent<Renderer>().material = thumbnailPrefab;
                    thumbnail.GetChild(0).gameObject.SetActive(true);
                    thumbnail.gameObject.GetComponent<ThumbnailSelection>().outlineSelection = false;
                }

                break;
            case PanelModes.Poly:
                GooglePoly.SetActive(false);
                polyManager.enabled = true;
                quickMenuHeader.text = "GOOGLE POLY";
                // TODO: Show search bar, render thumbnails with featured content.

                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
