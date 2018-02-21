using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapstoneSlideManager : MonoBehaviour {

    private PresentationInfo currentPresentation;
    public CapstoneNetworkManager session;

    //TODO: Make this a singleton


    // Use this for initialization
    void Start() {
        session = gameObject.GetComponent<CapstoneNetworkManager>();
    }
    /**
        Load a presentation based on the ID value that is returned
        
        @param presId [in] presentation ID to load
     */
    public void LoadPresentation(string presId) {
        //CapstoneNetworkManager session = new CapstoneNetworkManager();
        StartCoroutine(GrabPresentationData(presId));
        
    }

    // https://answers.unity.com/questions/228150/hold-or-wait-while-coroutine-finishes.html
    public IEnumerator GrabPresentationData(string presId) {
        string retVal = session.Request(CapstoneNetworkManager.RequestTypes.Load_Project, presId);
        while (session.inProgress) {
            yield return new WaitForSeconds(0.1f);
        }

        if (retVal != "error") {
            currentPresentation = new PresentationInfo();
            ParsePresentationJSON(currentPresentation, retVal);
            /*foreach(SlideInfo sl in currentPresentation.slides) {
                Debug.Log(sl.id);
                foreach (ModelInfo ml in sl.models) {
                    Debug.Log(ml.poly_id);
                }
            }*/
            //LoadSlide(0);
        } else {
            Debug.Log("Error Loading Presentation: %t" + presId);
        }
        yield return new WaitForSeconds(0.1f);
    }

    /**
     * Take in instantiated presentaiton object, assign json string for 
     * parsing via SimpleJSON, this creates full data dependency.
     * 
     * @param presObj - Instantiated presentation session object, presJSON - Retrieved JSON from presentation/all endpoint.
     */ 
    public void ParsePresentationJSON(PresentationInfo presObj, string presJSON) {
        var presentationJSON = SimpleJSON.JSON.Parse(presJSON);
        
        currentPresentation.id = presentationJSON["presentation"]["id"].AsInt;
        currentPresentation.name = presentationJSON["presentation"]["name"].Value;
        currentPresentation.description = presentationJSON["presentation"]["description"].Value;
        currentPresentation.is_live = presentationJSON["presentation"]["is_live"].AsBool;
        // Instantiate array to be of proper size based on quantity of slide objects in presentation.
        currentPresentation.slides = new SlideInfo[presentationJSON["presentation"]["slides"].Count];
        for (int jojo = 0; jojo < currentPresentation.slides.Length; jojo++) {
            // Instantiate a new SlideInfo object, populate its data, and assign it into our Presentation object.
            var slideJsonObj = presentationJSON["presentation"]["slides"][jojo];
            SlideInfo newSlide = new SlideInfo();
            newSlide.id = slideJsonObj["id"].AsInt;
            newSlide.sequence= slideJsonObj["sequence"].AsInt;
            newSlide.createdAt = slideJsonObj["created_at"].Value;
            newSlide.updatedAt= slideJsonObj["updated_at"].Value;
            newSlide.models = new ModelInfo[slideJsonObj["models"].Count];
            // Insert new slide into respective slides array.
            currentPresentation.slides[jojo] = newSlide;
            for (int innerJojo = 0; innerJojo < newSlide.models.Length; innerJojo++) {
                // Instantiate a new SlideInfo object, populate its data, and assign it into our Presentation object.
                var modelJsonObj = slideJsonObj["models"][innerJojo];
                ModelInfo newModel = new ModelInfo();
                newModel.id = modelJsonObj["id"].AsInt;
                newModel.poly_id = modelJsonObj["poly_id"].Value;
                newModel.transform = modelJsonObj["poly_id"].Value;
                newModel.created_at = modelJsonObj["created_at"].Value;
                newModel.updated_at = modelJsonObj["updated_at"].Value;
                newModel.slide_id = modelJsonObj["slide_id"].AsInt;
                // Insert new model into respective models array.
                currentPresentation.slides[jojo].models[innerJojo] = newModel;
            }
        }
    }

    /**
        Load a slide number in the current presenation.
        This should only work after a project has been loaded.
        
        @param slideNum the slide to load in
     */
    public void LoadSlide(int slideNum) {
        if(currentPresentation != null) {
            foreach (ModelInfo obj in currentPresentation.slides[slideNum].models) {
                Debug.Log("Object ID Number:\t" + obj.id + ", Poly ID:\t" + obj.poly_id);
            }
        } else {
            Debug.Log("Error Loading Slide: Current Project Object not Initialized");
        }
    }

    //TODO: Determine how to grab all information and save
    void SavePresentation() {

    }
    

    public class PresentationInfo {
        public int id;
        public string name;
        public string description;
        public bool is_live;
        public int user_id;

        public SlideInfo[] slides;

        public string createdAt;
        public string updatedAt;
    }
    
    public class SlideInfo {
        public int id;
        public int sequence;

        public string createdAt;
        public string updatedAt;
        public int presentation_id;

        public ModelInfo[] models;
    }

    public class ModelInfo {
        public int id;
        public string poly_id;
        public string transform;
        public string created_at;
        public string updated_at;
        public int slide_id;
        //TODO: fill out information that objects have
    }
}
