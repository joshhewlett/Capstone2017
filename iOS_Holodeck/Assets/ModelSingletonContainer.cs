using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class ModelSingletonContainer : MonoBehaviour {

    private static ModelSingletonContainer _instance;

    public static ModelSingletonContainer Instance { get { return _instance;}}
    public Dictionary<string, GameObject> polyObjects = new Dictionary<string, GameObject>(); 

    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

}