using System;
using System.Collections;
using System.Collections.Generic;
using PolyToolkit;
using UnityEngine;

public class PolyManager : MonoBehaviour {


    public List<PolyAsset> featuredPolys;
    public List<Texture2D> polyThumbnails;
    public Material thumbnailPrefab;
    public GameObject thumbnails;

    // Use this for initialization
    void Start() {
        thumbnailPrefab = Resources.Load("Thumbnails/PolyImports/360_camera", typeof(Material)) as Material;
        // List Assetes takes a PolyListAssetsRequest object which allows for filtering
        // of criteria to search for, and uses a callback to act on the data retrieved from API.
        PolyApi.ListAssets(PolyListAssetsRequest.Featured(), ListAssetsCallback);
    }

    /*
     * Method to load 4 series thumbnails from Poly API.
     */ 
    public void LoadThumbnails() {
        for (int jojo = 0; jojo < 4; jojo++) {
            PolyApi.FetchThumbnail(featuredPolys[jojo], GetThumbnailAssetCallback);
        }
    }


    /**
     * Callback to associate with poly API for retrieving respective 2D texture
     * image representations of poly models.
     */
    private void GetThumbnailAssetCallback(PolyAsset asset, PolyStatus status) {
        if (!status.ok) {
            Debug.LogError("Failed to get assets. Reason:\t" + status);
        }
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
            Debug.LogError("Failed to get assets. Reason:\t" + result.Status);
        }
        Debug.Log("Successfully retrieved poly featured list");
        featuredPolys = result.Value.assets;
        // TESTNG PURPOSES
        LoadThumbnails();
        //displayThumbnailSet();
        /*foreach (PolyAsset poly in result.Value.assets) {
            Debug.Log(poly.displayName + ", " + poly.name);
        }*/
    }

    public void displayThumbnailSet() {
        int counter = 0;

        foreach (Transform thumbnail in thumbnails.transform) {
            Texture2D clipart = polyThumbnails[counter];
            // TODO: Revert texture to previous state when exiting out of poly
            thumbnail.GetComponent<Renderer>().material = thumbnailPrefab;
            thumbnail.GetComponent<Renderer>().material.mainTexture = clipart;
            thumbnail.GetChild(0).gameObject.SetActive(false);
            Debug.Log(featuredPolys[counter].displayName);
            counter++;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
