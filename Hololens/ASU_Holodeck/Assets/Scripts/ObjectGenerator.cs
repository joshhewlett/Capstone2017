using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {

    public GameObject hololensCamera;
    public string JSON_Response;
    public bool testFlag = false;
    public GameObject objectsCollection;        // encapsulated object to hold instantiated imported models
    public GameObject dummyPrefab;
    public GameObject objectMenuToggle;
    public GameObject objectMenuTheme;
    public GameObject objectMainMenu;
    public GazeManager hololensGaze;
    public GameObject ModelMenuPrefab;


    // Use this for initialization
    void Start() {
        // FOR DEMONSTRATION PURPOSES ONLY
        //InstantiateGameObject(new Vector3(0.689f, -0.61f, 2.61f), new Vector3(-89.14201f, 0f, 0f), new Vector3(0.8f, 0.8f, 0.8f));
        ModelMenuPrefab = Resources.Load("Prefabs/MenuItems/ModelMenu", typeof(GameObject)) as GameObject;
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

        //InstantiateGameObject(objectPosition, objectRotation, objectScale);
    }

    

    /**
     * Method overloaded Instantiate for when we retrieve actual game objects
     * so that we may use the downloaded model as the passed game object for 
     * instantiation call.
     * This will be a per object user request for multiple game object downloads.
     */ 
    public void InstantiateGameObject(GameObject userAsset, 
                                       Vector3 pos,
                                       Vector3 rot,
                                       Vector3 sca) {
        // Instantiate object close to the user, then properly assign values with respect to the user.
        // Therefore, we are using user scale proportions.
        // TODO: JUSTIFY which world scale we want, with respect to user, or something else?
        // Create model encsapsulator that houses the model and its respective menu.
        GameObject modelEncapsulator = new GameObject();
        modelEncapsulator.name = userAsset.name + "Model";
        modelEncapsulator.transform.parent = objectsCollection.transform;   // Set coordinate system to object collection.
        // Set imported model parent to that of the model encapsulator.
        // Instantiate GameObject by calling FindPrefab method with the thumbnail icon game object name.
        //GameObject importedInstance = Instantiate(FindPrefab(userAsset.name), objectsCollection.transform) as GameObject;
        GameObject importedInstance = Instantiate(Resources.Load(("Prefabs/PolyModels/"+userAsset.name + "/model"), typeof(GameObject)), modelEncapsulator.transform) as GameObject;
        importedInstance.name = userAsset.name;
        foreach (Transform child in importedInstance.transform) {
            // Add mesh colliders to child components.
            // These child components will contain the meshes for the model, and need colliders
            // for hololens user interaction.
            child.gameObject.AddComponent<MeshCollider>();
        }
        // Method overloading for when we retrieve actual 3D model objects for content generation.
        // assign local transform properties.
        //importedInstance.transform.localPosition = pos;
        // Assign object location in front of the cameras zed axis, same x, y.
        importedInstance.transform.position= new Vector3(Camera.main.transform.localPosition.x + 0.4f,
                                                                Camera.main.transform.localPosition.y,
                                                                Camera.main.transform.localPosition.z + 1.5f);
        importedInstance.transform.rotation= Quaternion.identity;
        importedInstance.transform.eulerAngles= rot;
        importedInstance.transform.localScale = sca;

        // Add manipulation capabilities and properly tag game object for easier lookup.
        importedInstance.AddComponent<MeshCollider>();
        importedInstance.AddComponent<SpatialManipulation>();
        importedInstance.AddComponent<Interpolator>();
        importedInstance.AddComponent<TapDetection>();
        // Menu object instantiation section.
        importedInstance.AddComponent<DynamicObjectMenu>();
        GameObject objectMenu = Instantiate(ModelMenuPrefab, modelEncapsulator.transform) as GameObject;
        GameObject object_mainMenuToggle = objectMenu.transform.GetChild(1).gameObject;     // Grab reference to menu toggle for config
        GameObject object_mainMenu = objectMenu.transform.GetChild(2).gameObject;           // Grab reference to menu panel for config
        object_mainMenu.GetComponent<ManipulationModalSelector>().hololensGaze = hololensGaze;
        // Assign instantiated menu game object as the menu option of dynamic object menu script.
        importedInstance.GetComponent<DynamicObjectMenu>().menuOptions = object_mainMenuToggle;
        importedInstance.tag = "ImportedModel";
        // Assign dependency of object main menu with our main menu toggle.
        object_mainMenuToggle.GetComponent<ToggleManipulationSelection>().menuOptions[0] = object_mainMenu;
        object_mainMenuToggle.SetActive(false);     // Toggled upon user gaze.
        object_mainMenu.SetActive(false);           // Toggled upon user air tap.

        // Enalbe user for tapping to place the game object on initial import.
        importedInstance.AddComponent<TapToPlace>();
        importedInstance.GetComponent<TapToPlace>().IsBeingPlaced = true;

    }

    public GameObject FindPrefab(string objectName) {
        string relativePath = "Prefabs/PanelPrefabs/" + objectName;
        GameObject newPrefab = Resources.Load(relativePath) as GameObject;
        return newPrefab;
    }
    
    // Update is called once per frame
    void Update() {
        
    }
}