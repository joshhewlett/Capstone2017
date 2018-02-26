using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyToolkit;

public class PolyThumbnailLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // List Assetes takes a PolyListAssetsRequest object which allows for filtering
        // of criteria to search for, and uses a callback to act on the data retrieved from API.
        PolyApi.ListAssets(PolyListAssetsRequest.Featured(), ListAssetsCallback);
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
        // PolyApi.FetchThumbnail(result.Value, callback);
        foreach (PolyAsset poly in result.Value.assets) {
            Debug.Log(poly.displayName + ", " + poly.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
