using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class TweenManager : MonoBehaviour, IInputClickHandler {

    public GameObject rootModel;
    public GameObject[] keypointCollection;
    private GameObject currentKeypointIteration;
    public  int currentKeypointIndex;
    public bool pressedPlay;
    private float lerpSpeed = 1.0f;
    private float startTime, journeyLength;

	// Use this for initialization
	void Start () {
        pressedPlay = false;
        rootModel = gameObject.transform.parent.transform.parent.transform.parent.gameObject;
	}

    public void OnInputClicked(InputClickedEventData eventData) {
        // Grab reference to amount of children game objects to populate the tweening procedure array.
        keypointCollection = new GameObject[gameObject.transform.parent.transform.GetChild(3).gameObject.transform.childCount];
        // Populate array of keypoints.
        for (int jojo = 0; jojo < keypointCollection.Length; jojo++) {
            keypointCollection[jojo] = gameObject.transform.parent.transform.GetChild(3).gameObject.transform.GetChild(jojo).gameObject;
        }
        startTime = Time.time;
        pressedPlay = true;
        currentKeypointIteration = keypointCollection[0];
        currentKeypointIndex = 0;
    }

    // Update is called once per frame
    void Update () {
		if (pressedPlay) {
            rootModel.transform.position = Vector3.Lerp(rootModel.transform.position, currentKeypointIteration.transform.position, 0.02f);
        }
        // If we have reached last keypoint, stop lerping procedure.
        if (rootModel.transform.localPosition == keypointCollection[keypointCollection.Length].transform.localPosition) {
            pressedPlay = false;
        }
	}

    void UpdateLerpProcedure() {
        

    }
}
