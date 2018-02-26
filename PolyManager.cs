using System;
using System.Collections;
using System.Collections.Generic;
using PolyToolkit;
using UnityEngine.Networking;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using PolyToolkitInternal.api_clients.poly_client;
using PolyToolkitInternal;
using System.Text;
using Newtonsoft.Json.Linq;

public class PolyManager : MonoBehaviour {


    public List<PolyAsset> featuredPolys;
    // Used for grabbing one asset at a time.
    public PolyAsset assetImportInstance;
    public List<Texture2D> polyThumbnails;
    public Material thumbnailPrefab;
    public GameObject thumbnails;
    public GazeManager hololensGaze;
    public TextMesh debugger;
    public int thumbnailCounter = 0;
    public GameObject ModelMenuPrefab;
    public UnityWebRequest clientReq;
    public GameObject objectsCollection;

    // Use this for initialization
    void Start() {
        ModelMenuPrefab = Resources.Load("Prefabs/MenuItems/ModelMenu", typeof(GameObject)) as GameObject;
        thumbnailPrefab = Resources.Load("Thumbnails/PolyImports/360_camera", typeof(Material)) as Material;
        // List Assetes takes a PolyListAssetsRequest object which allows for filtering
        // of criteria to search for, and uses a callback to act on the data retrieved from API.
        debugger.text = "Requesting asset...";
        debugger.text += PolyApi.IsInitialized ? "Poly API init" : "NO POLY API";

        string downloadArea = "http://capstone-679aea6d.207c8177.svc.dockerapp.io:3001/testing/asset/5vbJ5vildOq";
        //Debug.Log("URL:\t" + downloadArea);
        //clientReq = UnityWebRequest.Get(downloadArea);
        //StartCoroutine(GetDataResults());
        //PolyApi.GetAsset("assets/5vbJ5vildOq", GetAssetCallback);
        PolyApi.ListAssets(PolyListAssetsRequest.Featured(), ListAssetsCallback);
    }
    

    /*public IEnumerator GetDataResults() {
        //Debug.Log("Before async call");
        // Wait for asychronous response 
        yield return clientReq.SendWebRequest();
        //Debug.Log("After async call");
        if (clientReq.isNetworkError) {
            Debug.Log(clientReq.error);
        } else {
            // Show results as text
            Debug.Log(clientReq.downloadHandler.text);
            // After synch call retrieves data, assign it
            // to local class variable.
            while (clientReq.downloadProgress < 1) {
                //  retval = client.downloadHandler.text;
            }
            string text = clientReq.downloadHandler.text;
            PolyStatus responseStatus = PolyClient.CheckResponseForError(text);
            if (!responseStatus.ok) {
                GetAssetCallback(new PolyStatusOr<PolyAsset>(responseStatus));
                yield return null;
            }
            Action<PolyStatus, PolyAsset> callback = (PolyStatus status, PolyAsset result) => {
                GetAssetCallback(new PolyStatusOr<PolyAsset>(status));
            };
            PolyMainInternal.Instance.DoBackgroundWork(new ParseAssetBackgroundWork(text, callback));
        }
    }*/

 
    /*
     * Method to load 4 series thumbnails from Poly API.
     */
    public void LoadThumbnails() {
        /*for (int jojo = 0; jojo < 4; jojo++) {
            PolyApi.FetchThumbnail(featuredPolys[jojo], GetThumbnailsAssetCallback);
        }*/
        foreach(PolyAsset pAsset in featuredPolys) {
            PolyApi.FetchThumbnail(pAsset, GetThumbnailsAssetCallback);
        }
    }

    /*public void LoadThumbnailsPanel(int changePanelBy) {
        // Increment our counter selection by whatever number selected from arrow navigations.
        thumbnailCounter += changePanelBy;
        Debug.Log(thumbnailCounter);
        foreach (Transform thumbnail in thumbnails.transform) {
            Texture2D clipart = polyThumbnails[thumbnailCounter];
            // TODO: Revert texture to previous state when exiting out of poly
            thumbnail.GetComponent<Renderer>().material = thumbnailPrefab;
            thumbnail.GetComponent<Renderer>().material.mainTexture = clipart;
            thumbnail.GetChild(2).gameObject.GetComponent<TextMesh>().text = featuredPolys[thumbnailCounter].displayName;
            // Assign it the reference of the asset ID
            thumbnail.gameObject.GetComponent<ThumbnailSelection>().polyAssetID = featuredPolys[thumbnailCounter].name;
            thumbnailCounter++;
        }
    }*/

    public void LoadThumbnailsForward() {
        thumbnailCounter += 4;
        Debug.Log(thumbnailCounter);
        foreach (Transform thumbnail in thumbnails.transform) {
            Texture2D clipart = polyThumbnails[thumbnailCounter];
            // TODO: Revert texture to previous state when exiting out of poly
            thumbnail.GetComponent<Renderer>().material = thumbnailPrefab;
            thumbnail.GetComponent<Renderer>().material.mainTexture = clipart;
            thumbnail.GetChild(2).gameObject.GetComponent<TextMesh>().text = featuredPolys[thumbnailCounter].displayName;
            // Assign it the reference of the asset ID
            thumbnail.gameObject.GetComponent<ThumbnailSelection>().polyAssetID = featuredPolys[thumbnailCounter].name;
            thumbnailCounter++;
        }
    }
    public void LoadThumbnailsBack() {
        thumbnailCounter += -4;
        Debug.Log(thumbnailCounter);
        foreach (Transform thumbnail in thumbnails.transform) {
            Texture2D clipart = polyThumbnails[thumbnailCounter];
            // TODO: Revert texture to previous state when exiting out of poly
            thumbnail.GetComponent<Renderer>().material = thumbnailPrefab;
            thumbnail.GetComponent<Renderer>().material.mainTexture = clipart;
            thumbnail.GetChild(2).gameObject.GetComponent<TextMesh>().text = featuredPolys[thumbnailCounter].displayName;
            // Assign it the reference of the asset ID
            thumbnail.gameObject.GetComponent<ThumbnailSelection>().polyAssetID = featuredPolys[thumbnailCounter].name;
            thumbnailCounter--;
        }
    }

    public void displayThumbnailSet() {
        foreach (Transform thumbnail in thumbnails.transform) {
            Texture2D clipart = polyThumbnails[thumbnailCounter];
            // TODO: Revert texture to previous state when exiting out of poly
            thumbnail.GetComponent<Renderer>().material = thumbnailPrefab;
            thumbnail.GetComponent<Renderer>().material.mainTexture = clipart;
            thumbnail.GetChild(2).gameObject.GetComponent<TextMesh>().text = featuredPolys[thumbnailCounter].displayName;
            // Assign it the reference of the asset ID
            thumbnail.gameObject.GetComponent<ThumbnailSelection>().polyAssetID = featuredPolys[thumbnailCounter].name;
            Debug.Log(featuredPolys[thumbnailCounter].displayName);
            thumbnailCounter++;
        }
    }

    /**
     * Poly API callback for grabbing single assets.
     */
    public void GrabAsset(string assetID) {
        PolyApi.GetAsset(assetID, GetAssetCallback);
    }

    /*
     * Asset callback, assigns imported asset to reference.
     */ 
    private void GetAssetCallback(PolyStatusOr<PolyAsset> result) {
        debugger.text += "Try import asset.";
        if (!result.Ok) {
            debugger.text = "Failed to get assets. Reason:\t" + result.Status;
            Debug.LogError("Failed to get assets. Reason:\t" + result.Status);
            return;
        }
        debugger.text = "Successful import of asset.";
        Debug.Log("Successful import of asset.");

        // Set the import options.
        PolyImportOptions options = PolyImportOptions.Default();
        // We want to rescale the imported mesh to a specific size.
        options.rescalingMode = PolyImportOptions.RescalingMode.FIT;
        // The specific size we want assets rescaled to (fit in a 5x5x5 box):
        //        options.desiredSize = 5.0f;
        options.desiredSize = 0.4f;
        // We want the imported assets to be recentered such that their centroid coincides with the origin:
        options.recenter = true;

        //        statusText.text = "Importing...";
        PolyApi.Import(result.Value, options, ImportAssetCallback);
        //        PolyApi.FetchThumbnail (result.Value, fetchThumbnailCallback);

    }

    private void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result) {
        
        if (!result.Ok) {
            debugger.text = "Failed to get assets. Reason:\t" + result.Status;
            Debug.LogError("Failed to get assets. Reason:\t" + result.Status);
            return;
        }
        debugger.text = "Successful import of asset.";
        Debug.Log("Successful import of asset.");

        GameObject modelEncapsulator = new GameObject();
        modelEncapsulator.name = asset.name + ", " + asset.displayName;
        modelEncapsulator.transform.parent = objectsCollection.transform;
        // Set model import as child of model encapsulator.
        result.Value.gameObject.transform.parent = modelEncapsulator.transform;

        result.Value.gameObject.AddComponent<BoxCollider>();
        // Edit size of box collider so a general average collision size can be used to contain itself to the model
        // whilst allowing for collisions with menus.
        // IMPORTANT: Another workaround is to edit unity's physics layer so that collisions with menus have higher precedence
        // over collisions with imported models.
        result.Value.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 0.3f);
        result.Value.gameObject.AddComponent<Rigidbody>();
        result.Value.gameObject.GetComponent<Rigidbody>().useGravity = false;
        result.Value.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        result.Value.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Add manipulation capabilities and properly tag game object for easier lookup.
        //result.Value.gameObject.AddComponent<MeshCollider>();
        result.Value.gameObject.AddComponent<SpatialManipulation>();
        result.Value.gameObject.AddComponent<HoloToolkit.Unity.Interpolator>();
        result.Value.gameObject.AddComponent<TapDetection>();
        // Menu object instantiation section.
        result.Value.gameObject.AddComponent<DynamicObjectMenu>();
        GameObject objectMenu = Instantiate(ModelMenuPrefab, modelEncapsulator.transform) as GameObject;
        GameObject object_mainMenuToggle = objectMenu.transform.GetChild(1).gameObject;     // Grab reference to menu toggle for config
        GameObject object_mainMenu = objectMenu.transform.GetChild(2).gameObject;           // Grab reference to menu panel for config
        object_mainMenu.GetComponent<ManipulationModalSelector>().hololensGaze = hololensGaze;
        // Assign instantiated menu game object as the menu option of dynamic object menu script.
        result.Value.gameObject.GetComponent<DynamicObjectMenu>().menuOptions = object_mainMenuToggle;
        result.Value.gameObject.tag = "ImportedModel";
        // Assign dependency of object main menu with our main menu toggle.
        object_mainMenuToggle.GetComponent<ToggleManipulationSelection>().menuOptions[0] = object_mainMenu;
        object_mainMenuToggle.SetActive(false);     // Toggled upon user gaze.
        //object_mainMenuToggle.transform.localPosition = new Vector3();
        object_mainMenu.SetActive(false);           // Toggled upon user air tap.

        // Enalbe user for tapping to place the game object on initial import.
        result.Value.gameObject.AddComponent<TapToPlace>();
        result.Value.gameObject.GetComponent<TapToPlace>().IsBeingPlaced = true;

    }

    public void LoadThumbnail(string assetID) {
        //PolyApi.FetchThumbnail(PolyApi.GetAsset(assetID, GetAssetCallback), GetThumbnailAssetCallback);
    }

    private void GetThumbnailAssetCallback(PolyAsset asset, PolyStatus status) {
        throw new NotImplementedException();
    }


    /**
     * Callback to associate with poly API for retrieving respective 2D texture
     * image representations of poly models.
     */
    private void GetThumbnailsAssetCallback(PolyAsset asset, PolyStatus status) {
        if (!status.ok) {
            debugger.text = "Failed to get assets. Reason:\t" + status;
            Debug.LogError("Failed to get assets. Reason:\t" + status);
        }
        debugger.text = "Successfully retrieved poly asset thumbnail";
        Debug.Log("Successfully retrieved poly asset thumbnail");
        // Append item to list of textures.
        polyThumbnails.Add(asset.thumbnailTexture);
    }
    

    /**
     * Method takes in PolyListAssetsResult object which gives us a assets List object
     * containing all assets matching criteria from specified request.
     */
    private void ListAssetsCallback(PolyStatusOr<PolyListAssetsResult> result) {
        if (!result.Ok) {
            debugger.text = "Failed to get assets. Reason:\t" + result.Status;
            Debug.LogError("Failed to get assets. Reason:\t" + result.Status);
        }
        debugger.text = "Successfully retrieved poly featured list";
        Debug.Log("Successfully retrieved poly featured list");
        featuredPolys = result.Value.assets;
        // TESTNG PURPOSES
        LoadThumbnails();
        //displayThumbnailSet();
        /*foreach (PolyAsset poly in result.Value.assets) {
            Debug.Log(poly.displayName + ", " + poly.name);
        }*/
    }

}
