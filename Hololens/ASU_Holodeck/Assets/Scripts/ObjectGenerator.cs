using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {

    public string JSON_Response;
    public bool testFlag = false;
    public GameObject objectsCollection;        // encapsulated object to hold instantiated imported models
    public GameObject dummyPrefab;
    public GameObject objectMenuToggle;
    public GameObject objectMenuTheme;
    public GameObject objectMainMenu;
    public GazeManager hololensGaze;


    // Use this for initialization
    void Start() {
        // FOR DEMONSTRATION PURPOSES ONLY
        InstantiateGameObject(new Vector3(0.689f, -0.61f, 2.61f), new Vector3(-89.14201f, 0f, 0f), new Vector3(0.8f, 0.8f, 0.8f));
    }

    /**
     * Method that gets called by Hololens user selection
     * which goes on to use private method API_Call to 
     * make query request for retrieving JSON response.
     */
    public void assignURL(string userRequest) {
        // TODO: Possibly change this flag to PRAGMA commands.
        if (testFlag) {

        }
        else {
            API_Call(userRequest);
        }

    }

    private void API_Call(string endpoint) {
        // The length for the for loop will follow the size of retrieved values.
        for (int i = 0; i < 5; i++) {
            Vector3 newPosition = new Vector3(0f, 0f, 0f);
            Vector3 newRotation = new Vector3(0f, 0f, 0f);
            Vector3 newScale = new Vector3(0f, 0f, 0f);
            //InstantiateGameObject(newPosition, newRotation, newScale);
        }
    }

    /**
     * TODO: Change method to use a JSON extracting object.
     * That or create this object with private properties that correspond
     * to JSON response values.
     */
    private void assignObjectTransformValues(string JSON_Values) {

        Vector3 objectPosition = new Vector3(0.689f, 0.038f, 1.696f);
        Vector3 objectRotation = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 objectScale = new Vector3(1.0f, 1.0f, 1.0f);

        InstantiateGameObject(objectPosition, objectRotation, objectScale);
    }

    /**
     * Method to instantiate game objects with respective prefabs, and transform properties.
     */
    private void InstantiateGameObject(Vector3 pos, Vector3 rot, Vector3 sca) {
        // Instantiate object close to the user, then properly assign values with respect to the user.
        // Therefore, we are using user scale proportions.
        // TODO: JUSTIFY which world scale we want, with respect to user, or something else?
        GameObject importedInstance = Instantiate(dummyPrefab , objectsCollection.transform) as GameObject;
        foreach (Transform child in importedInstance.transform) {
            // Add mesh colliders to child components.
            // These child components will contain the meshes for the model, and need colliders
            // for hololens user interaction.
            child.gameObject.AddComponent<MeshCollider>();
        }
        Debug.Log("Trying to instantiate");
        // assign local transform properties.
        
        //sca.x = sca.y = sca.z = 0.8f;
        importedInstance.transform.localPosition = pos;
        importedInstance.transform.localRotation = Quaternion.identity;
        importedInstance.transform.localEulerAngles = rot;
        importedInstance.transform.localScale = sca;
        importedInstance.AddComponent<MeshFilter>();
        importedInstance.AddComponent<MeshRenderer>();
        importedInstance.AddComponent<MeshCollider>();
        importedInstance.AddComponent<DynamicObjectMenu>();
        importedInstance.AddComponent<MoveManipulation>();
        importedInstance.AddComponent<RotationManipulation>();
        importedInstance.AddComponent<ScaleManipulation>();
        importedInstance.tag = "ImportedModel";

        GameObject object_mainMenuToggle= Instantiate(objectMenuToggle, importedInstance.transform) as GameObject;
        GameObject object_mainMenuTheme = Instantiate(objectMenuTheme, importedInstance.transform) as GameObject;
        GameObject object_mainMenu = Instantiate(objectMainMenu, importedInstance.transform) as GameObject;
        object_mainMenu.GetComponent<NegateToggleCancelation>().toggleSwitch = object_mainMenuToggle;
        object_mainMenu.GetComponent<ManipulationModalSelector>().hololensGaze = hololensGaze;
        // Assign instantiated menu game object as the menu option of dynamic object menu script.
        importedInstance.GetComponent<DynamicObjectMenu>().menuOptions = object_mainMenuToggle;
        // Assign dependency of object main menu with our main menu toggle.
        object_mainMenuToggle.GetComponent<ToggleManipulationSelection>().menuOptions[0] = object_mainMenu;
        object_mainMenuToggle.SetActive(false);     // Toggled upon user gaze.
        object_mainMenu.SetActive(false);           // Toggled upon user air tap.

    }

    /**
     * Method overloaded Instantiate for when we retrieve actual game objects
     * so that we may use the downloaded model as the passed game object for 
     * instantiation call.
     * This will be a per object user request for multiple game object downloads.
     */ 
    private void InstantiateGameObject(GameObject userAsset, 
                                       Vector3 pos,
                                       Vector3 rot,
                                       Vector3 sca) {
        // Instantiate object close to the user, then properly assign values with respect to the user.
        // Therefore, we are using user scale proportions.
        // TODO: JUSTIFY which world scale we want, with respect to user, or something else?
        GameObject importedInstance = Instantiate(userAsset, objectsCollection.transform) as GameObject;
        // Method overloading for when we retrieve actual 3D model objects for content generation.
        // assign local transform properties.
        importedInstance.transform.localPosition = pos;
        importedInstance.transform.localRotation = Quaternion.identity;
        importedInstance.transform.localEulerAngles = rot;
        importedInstance.transform.localScale = sca;
    }
    
    // Update is called once per frame
    void Update() {
        
    }
}