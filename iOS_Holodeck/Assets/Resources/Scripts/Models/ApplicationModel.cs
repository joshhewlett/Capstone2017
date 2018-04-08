using System.Collections.Generic;
using UnityEngine;
public static class ApplicationModel  
 {
    static public string presentationId = "";
    static public PresentationObject presentation = new PresentationObject();
    static public Dictionary<string, GameObject> polyObjects = new Dictionary<string, GameObject>();
    static public bool loadedModels = false;

    static public void reset() {
        presentation = new PresentationObject();
        presentationId = "";
        polyObjects = new Dictionary<string, GameObject>();
        loadedModels = false;
    }
 }