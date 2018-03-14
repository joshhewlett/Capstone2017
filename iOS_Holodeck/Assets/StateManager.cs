using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

	List<GameObject> gameObjects = new List<GameObject>();
	bool haveInitialized = false;

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
        addModelsFromSlide(0);

        SocketController socketController = new SocketController();
        socketController.getInstance();

        socketController.addTransformUpdateListener((object[] args) => {
            ModelTransform modelTransform = new ModelTransform(args);
            GameObject go = GameObject.Find(modelTransform.Model);
            if (go != null){
                go.transform.localPosition = modelTransform.Position;
                go.transform.localEulerAngles = modelTransform.Rotation;
                go.transform.localScale = modelTransform.Scale;
            }
        });

        socketController.addPresentationEndListener((object[] args) => {
            // Remove all models from scene (maybe not necessary?)
            removeAllModels();
            // Destroy socket instance
            // Use scene controller to go back to main screen
            SceneManager.LoadScene("PresentationIDInput");
            socketController.disconnect();
        });

        socketController.addSlideChangedListener((object[] args) => {
            // TODO Parse event data
            // TODO Get new slide number
            int slideNum = 0;

            // Remove all models
            removeAllModels();

            // Add all new slide models
            addModelsFromSlide(slideNum);
        });
	}

    public void addModelsFromSlide(int slideNum){
        ModelObject[] models = ApplicationModel.presentation.slides[slideNum].models;

        List<string> keyList = new List<string>(ApplicationModel.polyObjects.Keys);
        string[] keys = keyList.ToArray();


        foreach (ModelObject model in models)
        {
            if (model.id == 1)
            {
                GameObject go;
                bool success = ApplicationModel.polyObjects.TryGetValue("assets/" + model.poly_id, out go);

                if (success)
                {
                    GameObject newGo = Instantiate(go);
                    newGo.transform.parent = gameObject.transform;
                    newGo.transform.localPosition = model.position;
                    newGo.transform.localEulerAngles = model.rotation;
                    newGo.transform.localScale = model.scale;
                    newGo.name = model.id + "";
                    newGo.SetActive(true);
                }
            }
        }
    }

    public void removeAllModels() {
        foreach (Transform child in transform)
        {
            GameObject childGameObject = child.gameObject;
            Destroy(childGameObject);
        }
    }
}
