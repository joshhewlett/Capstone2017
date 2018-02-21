using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ObjectNextPage : PageSelection {

    public override void OnInputClicked(InputClickedEventData eventData) {
        Debug.Log("Next iteration:\t" + currentIndexLoaded);
        // check if index+4 isn't > than length,
        // this was we can load next 4 thumbnails.
        if (currentIndexLoaded+4 > objectThumbnails.Count) {
            return;
        }
        foreach(GameObject thumbnail in thumbnailIcons) {
            ++currentIndexLoaded;
            // Retrieve new texture to assign.
            Texture2D tempText = objectThumbnails[currentIndexLoaded];
            thumbnail.GetComponent<Renderer>().material.mainTexture = tempText;
            thumbnail.name = tempText.name;
        }
        Debug.Log("Next after iteration:\t" + currentIndexLoaded);
    } 
}
