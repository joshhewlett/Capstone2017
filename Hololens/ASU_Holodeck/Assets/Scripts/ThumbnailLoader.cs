using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbnailLoader : MonoBehaviour {
    public List<Texture2D> objectThumbnails;
    // Use this for initialization
    void Start() {
        foreach (Texture2D thumbnail in Resources.LoadAll("Thumbnails/PolyImports/", typeof(Texture2D))) {
            objectThumbnails.Add(thumbnail);
        }
    }

    // Update is called once per frame
    void Update() {
        //foreach (Texture2D thumbnail in objectThumbnails) {
            //Debug.Log(thumbnail.name);
        //}
    }
}
