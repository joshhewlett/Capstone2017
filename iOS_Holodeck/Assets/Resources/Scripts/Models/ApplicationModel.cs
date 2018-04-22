using System.Collections.Generic;
using UnityEngine;
public static class ApplicationModel  
 {
    static public string presentationId = "";
    static public PresentationObject presentation = new PresentationObject();
    static public Dictionary<string, GameObject> polyObjects = new Dictionary<string, GameObject>();
    static public bool loadedModels = false;
    static public string API_URL = "http://capstone-679aea6d.207c8177.svc.dockerapp.io:3001";
    //static public string API_URL = "http://localhost:3000";

    static public void reset() {
        presentation = new PresentationObject();
        presentationId = "";
        polyObjects = new Dictionary<string, GameObject>();
        loadedModels = false;
    }
 }