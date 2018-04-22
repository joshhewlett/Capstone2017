using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PolyToolkit;
using UnityEngine.SceneManagement;
using System.Timers;

public class TestPresentationId : MonoBehaviour {

	static int numPolyObjects = 0;
	static Timer aTimer;
	static GameObject iOSLoader = null;
	static bool shouldLoadNextScene = false;

	// Use this for initialization
	void Start () {
		Debug.Log("IN NEW SCENE WITH ID: " + ApplicationModel.presentationId);

		GameObject canvas = GameObject.Find("Canvas");
		iOSLoader = canvas.transform.GetChild(4).gameObject;

		// GameObject go = GameObject.Find("ModelSingletonContainer");
		// (ModelSingletonContainer) go.GetComponent<ModelSingletonContainer>().logObjects();
		// modelContainer = (ModelSingletonContainer) go.GetComponent<ModelSingletonContainer>();

		StartCoroutine(GetPresentationObject());
	}

	void Update() {
		if(shouldLoadNextScene) {
			// ModelSingletonContainer.copyObjects(ApplicationModel.polyObjects);
			SceneManager.LoadScene("iOS_Holo");
		}
	}

	/*
	 * Gets the presentation object from our API with the id specified by
	 * the user input
	 */
	IEnumerator GetPresentationObject(){
        UnityWebRequest presentationJson = UnityWebRequest.Get(ApplicationModel.API_URL + "/presentation/" + ApplicationModel.presentationId + "/all");
		//UnityWebRequest presentationJson = UnityWebRequest.Get("http://capstone-679aea6d.207c8177.svc.dockerapp.io:3001/presentation/" + ApplicationModel.presentationId + "/all");
		// presentationJson.downloadHandler = new DownloadHandlerBuffer();
		yield return presentationJson.SendWebRequest();

		if (presentationJson.isNetworkError || presentationJson.isHttpError){
			// Log error if web request fails
			Debug.Log("Error: " + presentationJson.error);
		} else {
			// Log JSON data returned from API call
			Debug.Log("Successful data: " + presentationJson.downloadHandler.text);

			var jsonObj = SimpleJSON.JSON.Parse(presentationJson.downloadHandler.text);


			/*
			 * Parse WebRequest response and create a presentation model with
			 * all slides and model objects
			 */

			// Create presentation object from JSON
			PresentationObject pres = new PresentationObject();

			pres.id = jsonObj["presentation"]["id"].AsInt;
			pres.name = jsonObj["presentation"]["name"].Value;
			pres.description = jsonObj["presentation"]["description"].Value;

			if(jsonObj["presentation"]["is_live"].Value == "True"){
				pres.is_live = true;
			} else {
				pres.is_live = false;
			}


			if (!pres.is_live){
				// Presentation isn't live.
				// return to main screen. Show some kind of error?
				SceneManager.LoadScene("ErrorPage");
				yield return null;
			}

			pres.user_id = jsonObj["presentation"]["user_id"].AsInt;

			// Create presentation slides from JSON
			SlideObject[] slides = new SlideObject[jsonObj["presentation"]["slides"].Count];

			// Array of unique Poly Object Ids to create Poly object models
			// without creating duplicates
			ArrayList polyIds = new ArrayList();

			for(int i = 0; i < slides.Length; i++){
				SlideObject s = new SlideObject();
				var slideJsonObj = jsonObj["presentation"]["slides"][i];

				s.id = slideJsonObj["id"].AsInt;
				s.sequence = slideJsonObj["sequence"].AsInt;
				s.presentation_id = slideJsonObj["presentation_id"].AsInt;
				s.created_at = slideJsonObj["created_at"].Value;
				s.updated_at = slideJsonObj["updated_at"].Value;

				// Create Slides and nested Models for each slide
				ModelObject[] models = new ModelObject[jsonObj["presentation"]["slides"][i]["models"].Count];
				for(int j = 0; j < models.Length; j++){
					ModelObject m = new ModelObject();
					var modelJsonObj = jsonObj["presentation"]["slides"][i]["models"][j];

					m.id = modelJsonObj["id"].AsInt;
					m.poly_id = modelJsonObj["poly_id"].Value;
                    m.parseTransform(modelJsonObj["transform"]);
					m.created_at = modelJsonObj["created_at"].Value;
					m.updated_at = modelJsonObj["updated_at"].Value;
					m.slide_id = modelJsonObj["slide_id"].AsInt;

					models[j] = m;

					// Add id to polyIds if unique
					if(!polyIds.Contains(m.poly_id)){
						polyIds.Add(m.poly_id);
					}
				}
				s.models = models;

				slides[i] = s;
			}
			pres.slides = slides;
			
			// Log slides and models for debugging
			for(int i = 0; i < slides.Length; i++){
				Debug.Log("Slide object: " + slides[i].id + " " + slides[i].presentation_id);

				for(int j = 0; j < slides[i].models.Length; j++){
					Debug.Log("Model poly id: " + slides[i].models[j].poly_id);
				}
			}

			// Add newly created Presentation to the ApplicationModel for global access
			// and create poly objects
			ApplicationModel.presentation = pres;
			CreatePolyObjects(polyIds);
			numPolyObjects = polyIds.Count;

			aTimer = new System.Timers.Timer(250);
			aTimer.Elapsed += OnTimedEvent;
			aTimer.AutoReset = true;
			aTimer.Enabled = true;
		}
	}

	private void OnTimedEvent(System.Object source, ElapsedEventArgs e){
		int count = ApplicationModel.polyObjects.Count;

		Debug.Log("Current poly objects loaded: " + count);
        if (ApplicationModel.polyObjects.Count == numPolyObjects){
			aTimer.Stop();
       		aTimer.Dispose();
			ApplicationModel.loadedModels = true;
			shouldLoadNextScene = true;
		}
    }


	// Get Poly Asset for each model in Presentation
	void CreatePolyObjects(ArrayList polyIds){
		foreach(String s in polyIds ){
			string asset = "assets/" + s;
			PolyApi.GetAsset(asset, GetAssetCallback);
		}
	}

	// Callback for GetAsset
	// Use result to get Unity objects using Poly.Import
	void GetAssetCallback(PolyStatusOr<PolyAsset> result){
		if(!result.Ok){
			Debug.Log("Error retrieving PolyAsset");
			numPolyObjects--;
			return;
		}

		PolyApi.Import(result.Value, PolyImportOptions.Default(), ImportAssetCallback);
	}

	// Callback for Import
	// Process GameObject and add to ApplicationModel
	void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result){
		if(!result.Ok){
			Debug.Log("Error importing PolyAsset");
			return;
		}

		/*
		 * Set all gameObject properties before creating prefab
		 */

		result.Value.gameObject.SetActive(false);
		result.Value.gameObject.AddComponent<BoxCollider>();
		result.Value.gameObject.AddComponent<InteractableObject>();
		// Turn off the bool for all interactable objects.
		result.Value.gameObject.GetComponent<InteractableObject>().movementEnabled = false;
		result.Value.gameObject.AddComponent<Rigidbody>();
		// Disable gravity, make kinematic, and allow for continuous collision detection so we 
		// can alter the motion physics of the respective game object model.
		result.Value.gameObject.GetComponent<Rigidbody> ().useGravity = false;
		result.Value.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		result.Value.gameObject.GetComponent<Rigidbody> ().collisionDetectionMode = CollisionDetectionMode.Continuous;
		// Add rigidbody
		result.Value.gameObject.transform.parent = gameObject.transform.parent;
		result.Value.gameObject.tag = "PolyImport";
		result.Value.gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		// Move object instantiation away from previous spawn.
		
		result.Value.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		// Finished importing allow user to select more models for importing.

		// Add to ApplicationModel for global access
		var polyObj = Instantiate(result.Value.gameObject, new Vector3(), Quaternion.identity);
		polyObj.transform.parent = ModelSingletonContainer.Instance.transform;
		ApplicationModel.polyObjects.Add(asset.name, polyObj);
	}
}
