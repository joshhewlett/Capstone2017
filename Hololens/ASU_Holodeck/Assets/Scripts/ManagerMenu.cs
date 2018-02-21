using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

//TODO: Determine how we need to break up the functionality between this
// panel and the drop-down panel
public class ManagerMenu : MonoBehaviour, IInputClickHandler {

    private enum MenuOptions
    {
        Objects,
        Slides,
        Presentations,
        Options
    };

    public bool isVisible;
    private GameObject thumbnailIcon, slideThumbnail;
    public GameObject DropDownPanel; 
    public GazeManager hololensGaze;
    // Retrieve the Object Generator component from the Input Manager.
    public ObjectGenerator objectGeneratorInstance;
    
    // Array of slide thumbnails
    private GameObject[] presentationSelections;
    private GameObject[] slideSelections;

    void Start() {
        isVisible = false;
        // Ensure panel is turned off when menu is instantiated
        DropDownPanel.SetActive(false);
        // Load reference to thumbnail prefab so we can instantiate these for each user request of 
        // menu population.
        thumbnailIcon = Resources.Load("Prefabs/ThumbnailObject", typeof(GameObject)) as GameObject;
        slideThumbnail = Resources.Load("Prefabs/SlideThumbnail", typeof(GameObject)) as GameObject;

        updateMenu();
    }

    void Update() {

    }

    /**
     * TODO: Need to integrate API call for loading set of presentations.
     */ 
    public void loadPresentationSelection() {
        // assign an array of several slides.
        presentationSelections = new GameObject[4];
        int counterInstance = 1;
        // Foreach child thumbnail in the slide thumbnails parent object.
        foreach (Transform child in gameObject.transform.GetChild(1).GetChild(1).gameObject.transform) {
            //Debug.Log(child.gameObject.name);
            child.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = counterInstance.ToString();
            ++counterInstance;
        }
    }

    /*
     * Load presentation from the network layer to be populated
     * in the scene loader. This will give us the respective number of 
     * slides.
     */ 
    public void loadPresentation(string presentationIdentifier) {
        
    }

    /*
     * Load slide from the Scene Loader.
     */ 
    public void loadSlide(int slideNumber) {
        
    }

    public void loadSlideSelection() {
        // assign an array of several slides.
        slideSelections = new GameObject[4];
        int counterInstance = 1;
        // Foreach child thumbnail in the slide thumbnails parent object.
        foreach (Transform child in gameObject.transform.GetChild(1).GetChild(1).gameObject.transform) {
            //Debug.Log(child.gameObject.name);
            child.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = counterInstance.ToString();
            ++counterInstance;
        }
    }

    /*
     * Retrieve poly asset from poly manager using the model id.
     */ 
    private void loadAssets(string assetID) {

    }

    public void RequestObject(GameObject iconSelection) {
        objectGeneratorInstance.InstantiateGameObject(iconSelection, new Vector3(1f, 0f, 1f), new Vector3(0f, 0f, 0f), new Vector3(0.22f, 0.22f, 0.22f));
    }

    // Update is called once per frame
    public void updateMenu() {
        // Opposite of actual menu item selected because we will toggle buttons off to 
        // toggle manipulation icons on.
        
    }

    //TODO: Determine if this needs to go into the drop-down class or not
    private void ChangePanel(MenuOptions selection) {
        switch(selection){
            case MenuOptions.Objects:
                // Grab reference to drop down panel, and enable asset thumbnails for viewing.
                gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                // Disable the slide thumbnails.
                gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                DropDownPanel.SetActive(true);
                break;
            case MenuOptions.Slides:
                gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                loadSlideSelection();
                // Enable the slide thumbnails.
                gameObject.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                DropDownPanel.SetActive(true);
                break;
            case MenuOptions.Presentations:
                gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                loadPresentationSelection();
                DropDownPanel.SetActive(true);
                break;
            case MenuOptions.Options:
                break;

        }
    }

    /**
     * Rather than having a visible bool trait, we can manually turn the visibility on or off based on user input.
     * To check if our panel is active, we reference the game object (assigned before runtime to this script)
     * and call gameObject.isActiveInHeirarchy
     * 
     * The method uses the hololens gaze to determine if we have a game object collision with the raycast gaze.
     * This in turn will give us the game object collisions information, such as the name of the object.
     */
    public void OnInputClicked(InputClickedEventData eventData) {
        // Set menu panel in mode of object selection.
        if (hololensGaze.HitObject.name == "Object Button") {
            ChangePanel(MenuOptions.Objects);
        }
        else if (hololensGaze.HitObject.name == "Slide Button") {            
            ChangePanel(MenuOptions.Slides);
        }
        else if (hololensGaze.HitObject.name == "Presentation Button") {            
            ChangePanel(MenuOptions.Presentations);
        }
        else if (hololensGaze.HitObject.name == "Cancel Button") {            
            DropDownPanel.SetActive(false);
        }
    }

}