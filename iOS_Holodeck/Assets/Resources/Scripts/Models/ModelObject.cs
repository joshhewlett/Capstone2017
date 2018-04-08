using UnityEngine;
using SimpleJSON;

public class ModelObject {

    public int id;
    public string poly_id;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public string created_at;
    public string updated_at;
    public int slide_id;

    public void parseTransform(JSONNode transform){
        // var jsonObj = SimpleJSON.JSON.Parse(transform);
        var jsonObj = transform;
        this.position = new Vector3(jsonObj["position"]["x"].AsFloat, jsonObj["position"]["y"].AsFloat, jsonObj["position"]["z"].AsFloat);
        this.rotation = new Vector3(jsonObj["rotation"]["x"].AsFloat, jsonObj["rotation"]["y"].AsFloat, jsonObj["rotation"]["z"].AsFloat);
        this.scale = new Vector3(jsonObj["scale"].AsFloat, jsonObj["scale"].AsFloat, jsonObj["scale"].AsFloat);
    }
}