using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowModel : MonoBehaviour {

    public GameObject modelToFollow;
    public Vector3 lastModelLocation;

    private void Awake() {
        Vector3 instantiatedModelLocation = gameObject.transform.parent.transform.parent.gameObject.transform.GetChild(0).gameObject.transform.localPosition;
        if (gameObject.CompareTag("ModelMenuToggle")) {
            Debug.Log("Moving toggle");
            instantiatedModelLocation.y += -0.24f;
            transform.localPosition = instantiatedModelLocation;
        }
        else {
            instantiatedModelLocation.x += 0.7f;
            //destinationLocation.z = 1.96f;
            transform.localPosition = instantiatedModelLocation;
        }
    }

    public void setStartingPoint() {
        Vector3 instantiatedModelLocation = gameObject.transform.parent.transform.parent.gameObject.transform.GetChild(0).gameObject.transform.localPosition;
        if (gameObject.CompareTag("ModelMenuToggle")) {
            Debug.Log("Moving toggle");
            instantiatedModelLocation.y += -0.24f;
            transform.localPosition = instantiatedModelLocation;
        }
        else {
            instantiatedModelLocation.x += 0.7f;
            //destinationLocation.z = 1.96f;
            transform.localPosition = instantiatedModelLocation;
        }
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Rooted to:\t" + gameObject.transform.parent.transform.parent.gameObject.transform.GetChild(0).gameObject.name);
        modelToFollow = gameObject.transform.parent.transform.parent.gameObject.transform.GetChild(0).gameObject;
        lastModelLocation = modelToFollow.gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 destinationLocation = modelToFollow.transform.localPosition;
        if (modelToFollow.transform.localPosition != lastModelLocation) {
            if (gameObject.CompareTag("ModelMenuToggle")) {
                Debug.Log("Moving toggle");
                destinationLocation.y += -0.24f;
                transform.localPosition = destinationLocation;
            }
            else {
                destinationLocation.x += 0.7f;
                //destinationLocation.z = 1.96f;
                transform.localPosition = destinationLocation;
            }
            lastModelLocation = modelToFollow.transform.localPosition;
        }
        
	}
}
