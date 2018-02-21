using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public abstract class ArrowNavigation : MonoBehaviour, IInputClickHandler {

    public GameObject thumbnails;
    public int firstThumbnail;
    public int adjustmentValue;

    /**
     * Iterate through each thumbnail in set of thumbnails
     * and increment/decrement numerical representation based
     * on button pressed.
     * Adjustment Value will be overrided on respective buttons, therefore 
     * we will add it to thumbnail text, and assign as +/- based on direction.
     */
    public virtual void OnInputClicked(InputClickedEventData eventData) {
        foreach (Transform thumbnail in thumbnails.transform) {
            thumbnail.GetComponentInChildren<TextMesh>().text =
                (int.Parse(thumbnail.GetComponentInChildren<TextMesh>().text)
                + adjustmentValue).ToString();
        }
    }
    
    public virtual void Update() {
        firstThumbnail = int.Parse(thumbnails.transform.GetChild(0).GetComponentInChildren<TextMesh>().text);
    }

    public virtual void setAdjustmentValue(int adjustment) {
        adjustmentValue = adjustment;
    }
	
}
