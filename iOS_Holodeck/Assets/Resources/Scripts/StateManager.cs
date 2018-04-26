using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.XR.iOS;

public class StateManager : MonoBehaviour {

	List<GameObject> gameObjects = new List<GameObject>();
	bool haveInitialized = false;
    public GameObject ar_scene;
    SocketController socketController;

	public void Start()
	{
        ar_scene = gameObject.transform.GetChild(0).gameObject;
	}

	public void Update() {
		if (ApplicationModel.loadedModels && !haveInitialized){
			haveInitialized = true;
			initialize();
		}
	}

	public void addObjectToScene(GameObject go){
		gameObjects.Add(go);
		go.SetActive(true);
	}

	public void initialize(){

		Debug.Log("Got all models");
        Debug.Log("Presentation id: " + ApplicationModel.presentationId);
        // foreach (KeyValuePair<string, GameObject> g in ApplicationModel.polyObjects){
        //     Debug.Log(g.Value);
        // }

        int slideId = -1;
        // TODO check out this loop. Is this the behavior we want?
        foreach(SlideObject slide in ApplicationModel.presentation.slides){
            if (slide.sequence == 0){
                slideId = slide.id;
            }
        }

        // Add models from active slide to scene and 
        // initially set them to inactive
        // addModelsFromSlide(slideId);

        // Set up socket controller
        // TODO how is this working?
        socketController = new SocketController();


        // Add functionality for parsing a transform update
        socketController.addTransformUpdateListener((object[] args) => {
            TransformObject modelTransform = new TransformObject(args);
            GameObject go = GameObject.Find(modelTransform.Model);
            Debug.Log("Received transform update for model " + modelTransform.Model);
            if (go != null){
                go.transform.localPosition = modelTransform.Position;
                go.transform.localEulerAngles = modelTransform.Rotation;
                go.transform.localScale = modelTransform.Scale;
            }
        });

        // Handle end of presentation socket event
        socketController.addPresentationEndListener((object[] args) => {
            EndPresentation();
        });

        // Handle a slide change in presentation
        socketController.addSlideChangedListener((object[] args) => {
            object slideNumObj = args[0];
            string slideNumStr = (string)slideNumObj;
            int slideNum = int.Parse(slideNumStr);
            Debug.Log("Slide changed: " + slideNum);
            // Remove all models
            removeAllModels();

            // Add all new slide models
            addModelsFromSlide(slideNum);
        });

        socketController.getInstance();
	}

    // Add models from given slide
    public void addModelsFromSlide(int slideNum){
        Debug.Log("Adding models for slide: " + slideNum);
        Debug.Log("Application Model slides " + ApplicationModel.presentation.slides);
        ModelObject[] models = null;

        // Set models to the array of models for the selected slide
        foreach (SlideObject slide in ApplicationModel.presentation.slides){
            if (slide.id == slideNum){
                models = slide.models;
                Debug.Log("Found models for slide");
                break;
            }
        }
        Debug.Log("Models are: " + models);

        if(models == null){
            return;
        }
        // List<string> keyList = new List<string>(ApplicationModel.polyObjects.Keys);
        // string[] keys = keyList.ToArray();

        Debug.Log("Models: " + models.Length);
        foreach (ModelObject model in models){
            Debug.Log("Got game object?");
            Debug.Log("PolyID: " + model.poly_id);
            Debug.Log("Model id: " + model.id);

            // Get game object representing poly object  
            GameObject go = GameObject.Find("/PresentationContainer/PolyModels/" + model.poly_id);

            Debug.Log("Got Poly prefab " + go);
            // Instantiate a new GameObject if 'go' was found
            if (go != null) {
                Debug.Log("yeah.. we got it");
                GameObject newGo = Instantiate(go);
                newGo.transform.parent = ar_scene.transform;
                newGo.transform.localPosition = model.position;
                newGo.transform.localEulerAngles = model.rotation;
                newGo.transform.localScale = go.transform.localScale * .1f;
                newGo.name = model.id + "";
                newGo.SetActive(true);
            }
        }
        //ar_scene.GetComponent<UnityARHitTestExample>().SetARScene(true);
    }

    public void removeAllModels() {
        Debug.Log("Removing all models");
        foreach (Transform child in ar_scene.transform)
        {
            Debug.Log("Removing model");
            GameObject childGameObject = child.gameObject;
            Destroy(childGameObject);
        }
        Debug.Log("Done removing models");
    }

    // Perform all necessary reset actions
    public void EndPresentation() {
        Debug.Log("ENDING PRESENTATION");

        // Remove all models from scene (maybe not necessary?)
        removeAllModels();
        ApplicationModel.reset();
        // Destroy socket instance
        // Use scene controller to go back to main screen
        socketController.disconnect();
        haveInitialized = false;
        SceneManager.LoadScene("PresentationIDInput");
    }
}
