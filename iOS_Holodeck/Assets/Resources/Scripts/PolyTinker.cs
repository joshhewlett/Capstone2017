// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using PolyToolkit;

/// <summary>
/// Simple example that loads and displays one asset.
/// 
/// This example requests a specific asset and displays it.
/// </summary>
public class PolyTinker : MonoBehaviour {

	// ATTENTION: Before running this example, you must set your API key in Poly Toolkit settings.
	//   1. Click "Poly | Poly Toolkit Settings..."
	//      (or select PolyToolkit/Resources/PtSettings.asset in the editor).
	//   2. Click the "Runtime" tab.
	//   3. Enter your API key in the "Api key" box.
	//
	// This example does not use authentication, so there is no need to fill in a Client ID or Client Secret.

	// Text where we display the current status.
//	public Text statusText;
	public bool editorToggle = false;
	public GameObject thumbnailPrefab;
	public Vector3 previousPlacement = new Vector3(0f, 1.5f, 0.7f);
	public string[] assetTest = new string[4] {
		"assets/8vHYM3zeiB3",
		"assets/aba7TBlKyJE",
		"assets/cnhEejzpYLG",
		"assets/bTcqWpYqeeM"
	};

	private void Start() {
		// Request the asset.
		Debug.Log("Requesting asset...");
		// Store thumbnail prefab reference.
		thumbnailPrefab = Resources.Load ("Prefabs/Thumbnail/Thumbnail", typeof(GameObject)) as GameObject;
	}

	public void GrabAsset(string assetID) {
//		PolyApi.GetAsset("assets/5OP5JSQZZn-", GetAssetCallback);
		PolyApi.GetAsset(assetID, GetAssetCallback);
	}

	public void LoadThumbnails(GameObject thumbnailHolder) {
		// Iterate through all thumbnail objects of thumbnail holder.
		int counter = 0;
		foreach (Transform child in thumbnailHolder.transform) {
			PolyApi.GetAsset(assetTest[counter], GetThumbnailAssetCallback);
			++counter;
		}
	}

	private void GetThumbnailAssetCallback(PolyStatusOr<PolyAsset> result) {
		if (!result.Ok) {
			Debug.LogError("Failed to get assets. Reason: " + result.Status);
			//			statusText.text = "ERROR: " + result.Status;
			return;
		}
		Debug.Log("Successfully got asset!");

		PolyApi.FetchThumbnail (result.Value, fetchThumbnailCallback);
	}

	
	void fetchThumbnailCallback(PolyAsset asset, PolyStatus status) {
		if (!status.ok) {
			Debug.Log("Import failed of thumbnail");
			return;
		}
		// Assign parent as hit object model menu for thumbnails.
		GameObject thumbnailInstance = Instantiate (thumbnailPrefab, transform.parent.GetComponent<ARKitUserTapPlace>().modelMenu.transform);
		if (asset.Url.Contains (assetTest [0].Substring (7))) {
			thumbnailInstance.transform.localPosition = new Vector3 (-0.08700001f, 0f, -0.005999998f);
			thumbnailInstance.transform.localScale = new Vector3 (0.1326987f, 0.1326987f, 0.1326987f);
			thumbnailInstance.gameObject.name = "Thumbnail";
			thumbnailInstance.gameObject.tag = "Thumbnail";
		} else if (asset.Url.Contains (assetTest [1].Substring (7))) {
			thumbnailInstance.transform.localPosition = new Vector3 (0.09199999f, 0f, -0.005999998f);
			thumbnailInstance.transform.localScale = new Vector3 (0.1326987f, 0.1326987f, 0.1326987f);
			thumbnailInstance.gameObject.name = "Thumbnail1";
			thumbnailInstance.gameObject.tag = "Thumbnail";
		} else if (asset.Url.Contains (assetTest [2].Substring (7))) {
			thumbnailInstance.transform.localPosition = new Vector3 (-0.08700001f, -0.164f, -0.005999998f);
			thumbnailInstance.transform.localScale = new Vector3 (0.1326987f, 0.1326987f, 0.1326987f);
			thumbnailInstance.gameObject.name = "Thumbnail2";
			thumbnailInstance.gameObject.tag = "Thumbnail";
		} else if (asset.Url.Contains (assetTest [3].Substring (7))) {
			thumbnailInstance.transform.localPosition = new Vector3 (0.09199999f, -0.164f, -0.005999998f);
			thumbnailInstance.transform.localScale = new Vector3 (0.1326987f, 0.1326987f, 0.1326987f);
			thumbnailInstance.gameObject.name = "Thumbnail3";
			thumbnailInstance.gameObject.tag = "Thumbnail";
		}
//		thumbnailInstance.transform.localPosition = new Vector3 (0f, 3.22f, 0f);
//		thumbnailInstance.transform.localScale = new Vector3 (6f, 6f, 6f);
		thumbnailInstance.GetComponent<Renderer>().material.mainTexture = asset.thumbnailTexture;
	}

	void Update() {
		// TESTING PURPOSES
		if (editorToggle) {
			//PolyApi.GetAsset("assets/5OP5JSQZZn-", GetAssetCallback);
			foreach (string request in assetTest) {
				PolyApi.GetAsset(request, GetAssetCallback);
			}
			editorToggle = false;
		}
	}

	// Callback invoked when the featured assets results are returned.
	private void GetAssetCallback(PolyStatusOr<PolyAsset> result) {
		if (!result.Ok) {
			Debug.LogError("Failed to get assets. Reason: " + result.Status);
			//			statusText.text = "ERROR: " + result.Status;
			return;
		}
		Debug.Log("Successfully got asset!");

		// Set the import options.
		PolyImportOptions options = PolyImportOptions.Default();
		// We want to rescale the imported mesh to a specific size.
		options.rescalingMode = PolyImportOptions.RescalingMode.FIT;
		// The specific size we want assets rescaled to (fit in a 5x5x5 box):
		//		options.desiredSize = 5.0f;
		options.desiredSize = 0.4f;
		// We want the imported assets to be recentered such that their centroid coincides with the origin:
		options.recenter = true;

		//		statusText.text = "Importing...";
		PolyApi.Import(result.Value, options, ImportAssetCallback);
		//		PolyApi.FetchThumbnail (result.Value, fetchThumbnailCallback);

	}

	// Callback invoked when an asset has just been imported.
	private void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result) {
		if (!result.Ok) {
			Debug.LogError("Failed to import asset. :( Reason: " + result.Status);
//			statusText.text = "ERROR: Import failed: " + result.Status;
			return;
		}
		Debug.Log("Successfully imported asset!");

		// Show attribution (asset title and author).
//		statusText.text = asset.displayName + "\nby " + asset.authorName;

		// Here, you would place your object where you want it in your scene, and add any
		// behaviors to it as needed by your app. As an example, let's just make it
		// slowly rotate:
		result.Value.gameObject.SetActive(false);
		// result.Value.gameObject.AddComponent<Rotate>();
		// result.Value.gameObject.AddComponent<BoxCollider>();
		// result.Value.gameObject.AddComponent<InteractableObject>();
		// // Turn off the bool for all interactable objects.
		// result.Value.gameObject.GetComponent<InteractableObject>().movementEnabled = false;
		// result.Value.gameObject.AddComponent<Rigidbody>();
		// // Disable gravity, make kinematic, and allow for continuous collision detection so we 
		// // can alter the motion physics of the respective game object model.
		// result.Value.gameObject.GetComponent<Rigidbody> ().useGravity = false;
		// result.Value.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		// result.Value.gameObject.GetComponent<Rigidbody> ().collisionDetectionMode = CollisionDetectionMode.Continuous;
		// // Add rigidbody
		// result.Value.gameObject.transform.parent = gameObject.transform.parent;
		// result.Value.gameObject.tag = "PolyImport";
		// result.Value.gameObject.transform.localScale = new Vector3(5f, 5.0f, 5.0f);
		// // Move object instantiation away from previous spawn.
		// previousPlacement.z += 0.3f;
		// previousPlacement.x += 0.4f;
		// result.Value.gameObject.transform.localPosition = previousPlacement;
		// // Finished importing allow user to select more models for importing.
		// gameObject.transform.parent.gameObject.GetComponent<ARKitUserTapPlace> ().importing = false;
	}
}