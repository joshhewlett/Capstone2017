using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ObjectPreviousPage : PageSelection {

    public override void OnInputClicked(InputClickedEventData eventData) {
        if (currentIndexLoaded - 4 < -1) {
            return;
        }
        currentIndexLoaded -= 4;
        Debug.Log("Prev iteration:\t" + currentIndexLoaded);
        for (int jojo = 3; jojo > -1; jojo--) {
            // Retrieve new texture to assign.
            Texture2D tempText = objectThumbnails[currentIndexLoaded];
            thumbnailIcons[jojo].GetComponent<Renderer>().material.mainTexture = tempText;
            thumbnailIcons[jojo].name = tempText.name;
            --currentIndexLoaded;
        }
        currentIndexLoaded += 4;
    }
}
