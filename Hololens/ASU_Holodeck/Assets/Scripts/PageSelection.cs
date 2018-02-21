using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public abstract class PageSelection : MonoBehaviour, IInputClickHandler {
    public List<Texture2D> objectThumbnails;
    public GameObject[] thumbnailIcons;
    public static int currentIndexLoaded;

    public virtual void OnInputClicked(InputClickedEventData eventData) {
        
    }

    public virtual void Awake() {
        objectThumbnails = gameObject.transform.parent.gameObject.GetComponent<ThumbnailLoader>().objectThumbnails;
    }
    // Use this for initialization
    public virtual void Start() {
        // 0 index based
        currentIndexLoaded = 3;
        objectThumbnails = gameObject.transform.parent.gameObject.GetComponent<ThumbnailLoader>().objectThumbnails;
    }

    // Update is called once per frame
    public virtual void Update() {

    }
}
