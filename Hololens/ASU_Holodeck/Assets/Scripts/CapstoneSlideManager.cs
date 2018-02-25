using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapstoneSlideManager : MonoBehaviour {

    private PresentationInfo currentPresentation;
    public CapstoneNetworkManager session;
    public GameObject debuggerText;

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

    public void LoadText(string text) {
        debuggerText.GetComponent<TextMesh>().text = "\n" + text;
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
     * @param presObj - Instantiated presentation session object
     * @param presJSON - Retrieved JSON from presentation/all endpoint.
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
            newSlide.sequence = slideJsonObj["sequence"].AsInt;
            newSlide.createdAt = slideJsonObj["created_at"].Value;
            newSlide.updatedAt = slideJsonObj["updated_at"].Value;
            newSlide.models = new ModelInfo[slideJsonObj["models"].Count];
            // Insert new slide into respective slides array.
            currentPresentation.slides[jojo] = newSlide;
            currentPresentation.modifiedSlides[jojo] = newSlide;
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
                currentPresentation.slides[jojo].modifiedModels[innerJojo] = newModel;
                currentPresentation.modifiedSlides[jojo].modifiedModels[innerJojo] = newModel;
            }
        }
        //load all slides into modified list
    }

    /**
        Load a slide number in the current presenation.
        This should only work after a project has been loaded.
        
        @param slideNum the slide to load in
     */
    public void LoadSlide(int slideNum) {
        if (currentPresentation != null) {
            Debug.Log("Parameter:\t" + slideNum);
            Debug.Log("Slides Length:\t" + currentPresentation.slides.Length);

            Debug.Log(currentPresentation.slides[slideNum]);
            foreach (ModelInfo obj in currentPresentation.slides[slideNum].models) {
                //LoadText("\nObject ID Number:\t" + obj.id + ",\nPoly ID:\t" + obj.poly_id);
                Debug.Log("Object ID Number:\t" + obj.id + ", Poly ID:\t" + obj.poly_id);
            }
        } else {
            //LoadText("Error Loading Slide: Current Project Object not Initialized");
            Debug.Log("Error Loading Slide: Current Project Object not Initialized");
        }
    }

    void PresentationToJSON() {
        SimpleJSON.JSONObject presJSON = new SimpleJSON.JSONObject();
        SimpleJSON.JSONArray slidesJSON = new SimpleJSON.JSONArray();

        presJSON.Add("id", currentPresentation.id);
        presJSON.Add("name", currentPresentation.name);
        presJSON.Add("description", currentPresentation.description);
        presJSON.Add("is_live", currentPresentation.is_live);
        presJSON.Add("user_id", currentPresentation.user_id);
        presJSON.Add("slides", slidesJSON);
        presJSON.Add("createdAt", currentPresentation.createdAt);
        presJSON.Add("updatedAt"); //TODO: Determine how to get current time);
        for (int i = 0; i < currentPresentation.slides.Length; ++i) {
            SimpleJSON.JSONObject curSlide = new SimpleJSON.JSONObject();
            SimpleJSON.JSONArray modelsJSON = new SimpleJSON.JSONArray();
            curSlide.Add("id", currentPresentation.slides[i].id);
            curSlide.Add("sequence", currentPresentation.slides[i].sequence);
            curSlide.Add("createdAt", currentPresentation.slides[i].createdAt);
            curSlide.Add("updatedAt"); //TODO: Determine how to get current time);
            curSlide.Add("presentation_id", currentPresentation.slides[i].presentation_id);

            for (int j = 0; j < currentPresentation.slides[i].models.Length; ++j) {
                SimpleJSON.JSONObject curModel = new SimpleJSON.JSONObject();
                curModel.Add("id", currentPresentation.slides[i].models[j].id);
                curModel.Add("poly_id", currentPresentation.slides[i].models[j].poly_id);
                curModel.Add("transform", currentPresentation.slides[i].models[j].transform);
                curModel.Add("created_at", currentPresentation.slides[i].models[j].created_at);
                curModel.Add("updated_at", currentPresentation.slides[i].models[j].updated_at);
                curModel.Add("slide_id", currentPresentation.slides[i].models[j].slide_id);
                modelsJSON.Add(curModel);
            }
            curSlide.Add("models", modelsJSON);
            slidesJSON.Add(curSlide);
        }
        //TODO:
        Debug.Log("Determine how to print out the JSON string");
    }

    //TODO: Verify that when objects are instantiated, they are loaded into presentation manager objects
    void SavePresentation() {

    }

    /**
     * 
     * This method is called whenever an object is moved on the screen.
     * It will ensure that all motions are recorded and can be easily
     * saved to the server.
     * 
     * @param obj               the game object to pull information from
     * @param objID             the object ID to modify within the array
     * @param currentSlide      the slide currently being modified
     */
    void UpdateModelTransform(GameObject obj, int objId, int currentSlide)
    {
        //TODO: determine if this is the best way of doing this
        currentPresentation.modifiedSlides[currentSlide].modifiedModels[objId].modelTrans = obj.transform;
    }

    /**
     * This adds a new slide with default values and a new slide ID
     * 
     * @return the slide number that results after adding a slide
     */
    int AddSlide()
    {
        //previous length of the array
        int prevLength = currentPresentation.modifiedSlides.Count;
        currentPresentation.modifiedSlides.Add(new SlideInfo());
        //previous length is also the address location of the list for the new item
        currentPresentation.modifiedSlides[prevLength].id = prevLength;
        //return new size
        return prevLength + 1;
    }

    /**
     * Delete slide at a specific location
     * 
     * @param slideLocation       The target slide to be deleted.
     */
    void DeleteSlide(int slideLocation)
    {
        //remove target slide
        currentPresentation.modifiedSlides.RemoveAt(slideLocation);
        //for each slide following, update ID to fit new location
        for(int i = slideLocation; i < currentPresentation.modifiedSlides.Count; ++i)
        {
            currentPresentation.modifiedSlides[i].id = i;
        }
    }

    /**
     * Add a model to the current slide
     * 
     * @param model          The model to have its transform data saved.
     * @param polyID         The Google Poly ID that was used to instantiate it.
     * @param slide          The slide that the model has been instantiated in.
     */
    void AddModel(GameObject model, string polyID, int slide)
    {
        //previous length of the array
        int prevLength = currentPresentation.modifiedSlides[slide].modifiedModels.Count;
        currentPresentation.modifiedSlides[slide].modifiedModels.Add(new ModelInfo());
        //previous length is also the address location of the list for the new item
        currentPresentation.modifiedSlides[slide].modifiedModels[prevLength].id = prevLength;
        currentPresentation.modifiedSlides[slide].modifiedModels[prevLength].poly_id = polyID;
        currentPresentation.modifiedSlides[slide].modifiedModels[prevLength].modelTrans = model.transform;
        currentPresentation.modifiedSlides[slide].modifiedModels[prevLength].created_at = ""; //TODO: determine how to save current time
        currentPresentation.modifiedSlides[slide].modifiedModels[prevLength].updated_at = "";
    }

    void DeleteModel(int slideLocation, int modelID)
    {
        //remove target slide
        currentPresentation.modifiedSlides[slideLocation].modifiedModels.RemoveAt(modelID);
        //for each slide following, update ID to fit new location
        for (int i = modelID; i < currentPresentation.modifiedSlides[slideLocation].modifiedModels.Count; ++i)
        {
            currentPresentation.modifiedSlides[slideLocation].modifiedModels[i].id = i;
        }
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

        public List<SlideInfo> modifiedSlides;
    }

    public class SlideInfo {
        public int id;
        public int sequence;

        public string createdAt;
        public string updatedAt;
        public int presentation_id;

        public ModelInfo[] models;

        public List<ModelInfo> modifiedModels;
    }

    public class ModelInfo {
        public int id;
        public string poly_id;
        public string transform;
        public string created_at;
        public string updated_at;
        public int slide_id;

        public UnityEngine.Transform modelTrans;
    }
}
