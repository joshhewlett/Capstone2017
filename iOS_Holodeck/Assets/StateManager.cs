using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
		ModelObject[] models = ApplicationModel.presentation.slides[0].models;

        List<string> keyList = new List<string>(ApplicationModel.polyObjects.Keys);
        string[] keys = keyList.ToArray();


		foreach(ModelObject model in models){
			GameObject go;
			bool success = ApplicationModel.polyObjects.TryGetValue("assets/" + model.poly_id, out go);
			
			if (success){
                GameObject newGo = Instantiate(go);
				newGo.transform.parent = gameObject.transform;
                newGo.transform.localPosition = model.position;
                newGo.transform.localEulerAngles = model.rotation;
                newGo.transform.localScale = model.scale;
                newGo.name = model.id + "";
				newGo.SetActive(true);
			}
		}

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
	}
}
