using UnityEngine;
using System.Collections.Generic;

public class TransformObject
{
    public TransformObject(object[] args)
    {
        Dictionary<string, object> dict = (Dictionary<string, object>)args[0];

        parseModel(dict);

        parseScale(dict);


        object positionObj;
        dict.TryGetValue("position", out positionObj);
        Dictionary<string, object> position = (Dictionary<string, object>)positionObj;

        parsePosition(position);

        object rotationObj;
        dict.TryGetValue("rotation", out rotationObj);
        Dictionary<string, object> rotation = (Dictionary<string, object>)rotationObj;

        parseRotation(rotation);
    }

    private void parseModel(Dictionary<string, object> dict){
        object model;
        dict.TryGetValue("model", out model);
        this.model = model.ToString();
    }

    private void parseScale(Dictionary<string, object> dict){
        object scale;
        dict.TryGetValue("scale", out scale);
        this.scale = new Vector3((float)(double)scale, (float)(double)scale, (float)(double)scale);
    }

    private void parsePosition(Dictionary<string, object> dict){
        object x, y, z;
        dict.TryGetValue("x", out x);
        dict.TryGetValue("y", out y);
        dict.TryGetValue("z", out z);
        this.position = new Vector3((float)(double)x, (float)(double)y, (float)(double)z);
    }

    private void parseRotation(Dictionary<string, object> dict){
        object x, y, z;
        dict.TryGetValue("x", out x);
        dict.TryGetValue("y", out y);
        dict.TryGetValue("z", out z);
        this.rotation = new Vector3((float)(double)x, (float)(double)y, (float)(double)z);
    }

    private Vector3 position;

    public Vector3 Position
    {
        get
        {
            return position;
        }
    }

    private Vector3 rotation;

    public Vector3 Rotation
    {
        get
        {
            return rotation;
        }
    }

    private Vector3 scale;

    public Vector3 Scale
    {
        get
        {
            return scale;
        }
    }

    private string model;

    public string Model
    {
        get
        {
            return model;
        }
    }
}
