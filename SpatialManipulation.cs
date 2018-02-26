using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class SpatialManipulation : MonoBehaviour, IManipulationHandler, IInputClickHandler {

    public ManipulationType manipulationMode;
    public float MoveSpeed = 0.1f;
    private Vector3 previousTransform;
    public GameObject manipulationSelection;
    // Keep reference to menu canvas to prohibit it from being manipulated whilst manipulating object.
    public GameObject menuCanvas;           
    public bool selectedEditMode;

    public float MAX_X_SCALE = 1.7f,
                 MAX_Y_SCALE = 1.7f,
                 MAX_Z_SCALE = 1.7f,
                 MIN_X_SCALE = 0.1f,
                 MIN_Y_SCALE = 0.1f,
                 MIN_Z_SCALE = 0.1f;

    public float RotationSpeed = 15f;

    public enum ManipulationType {
        Move,
        Rotate,
        Scale,
        None,
    };

    // Use this for initialization
    void Start () {
        manipulationMode = ManipulationType.None;
        selectedEditMode = false;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in gameObject.transform) {
            // iterate over children that are meshes of the game object.
            if (child.gameObject.name.Contains("mesh")) {
                continue;
            }
            if (child.gameObject.name == "ObjectMainMenu") {
                // Grab reference to manipulation options game object.
                menuCanvas = child.gameObject;
                //manipulationSelection = child.gameObject.GetComponent<ManipulationModalSelector>().ManipulationSelectionOptions[0];
            }
        }
        
        // If user gaze tap selection is done editing, toggle respectively.
        if (!(gameObject.transform.parent.GetChild(1).GetChild(1).gameObject.GetComponent<ToggleManipulationSelection>().interactiveToggleSelection)) {
            manipulationMode = SpatialManipulation.ManipulationType.None;
        }
        Debug.Log("Set mode:\t" + manipulationMode);
	}

    public void OnManipulationStarted(ManipulationEventData eventData) {
        switch (manipulationMode) {
            case ManipulationType.Move:
                break;
            case ManipulationType.Rotate:
                previousTransform = gameObject.transform.localEulerAngles;
                break;
            case ManipulationType.Scale:
                previousTransform = gameObject.transform.localScale;
                break;
            default:
                break;
        }
    }

    public void OnManipulationUpdated(ManipulationEventData eventData) {
        switch (manipulationMode) {
            case ManipulationType.Move:
                calculateMovement(eventData.CumulativeDelta);
                break;
            case ManipulationType.Rotate:
                calculateRotation(eventData.CumulativeDelta);
                break;
            case ManipulationType.Scale:
                calculateScale(eventData.CumulativeDelta, previousTransform);
                break;
            default:
                break;
        }
    }

    public void calculateMovement(Vector3 gestureDelta) {
        Vector3 handMovement = gestureDelta * MoveSpeed;
        gameObject.transform.localPosition += handMovement;
    }

    public void calculateRotation(Vector3 gestureDelta) {
        Vector3 handMovement = gestureDelta * RotationSpeed;
        // Rotate Y by x axis gesture.
        // Rotate X by y axis gesture.
        // TODO: Use float comparison of gesture to see which direction user is intending
        // of rotating object.
        gameObject.transform.localRotation = Quaternion.Euler(
            previousTransform.x + (RotationSpeed * gestureDelta.y),
            previousTransform.y + (RotationSpeed * gestureDelta.x),
            previousTransform.z);
        previousTransform = transform.localEulerAngles;
    }

    public void calculateScale(Vector3 gestureDelta, Vector3 prevScale) {
        Vector3 tempScale = transform.localScale;
        Vector3 tempPrevScale = previousTransform;

        // We use the negative z value because pointing outward gives a negative z and we want
        // that to be treated as a positive z in world scale.
        tempPrevScale.x = tempPrevScale.x + (-gestureDelta.z * 2.0f);
        tempPrevScale.y = tempPrevScale.y + (-gestureDelta.z * 2.0f);
        tempPrevScale.z = tempPrevScale.z + (-gestureDelta.z * 2.0f);
        tempScale = tempPrevScale;

        tempScale.x = Mathf.Clamp(tempScale.x, MIN_X_SCALE, MAX_X_SCALE);
        tempScale.y = Mathf.Clamp(tempScale.y, MIN_Y_SCALE, MAX_Y_SCALE);
        tempScale.z = Mathf.Clamp(tempScale.z, MIN_Z_SCALE, MAX_Z_SCALE);

        gameObject.transform.localScale = tempScale;
    }

    public void OnManipulationCompleted(ManipulationEventData eventData) {
        
    }

    public void OnManipulationCanceled(ManipulationEventData eventData) {
        
    }

    // TODO: Fix this
    public void OnInputClicked(InputClickedEventData eventData) {
        // Disable the tap to place that starts upon instantiation of game object from menu panel.
        if (gameObject.CompareTag("ImportedModel") && gameObject.GetComponent<TapToPlace>()) {
            gameObject.GetComponent<TapToPlace>().enabled = false;
            // Grab reference to menu
            gameObject.transform.parent.GetChild(1).gameObject.transform.GetChild(1).GetComponent<FollowModel>().setStartingPoint();
            gameObject.transform.parent.GetChild(1).gameObject.transform.GetChild(2).GetComponent<FollowModel>().setStartingPoint();
        } else {
            gameObject.GetComponent<TapToPlace>().enabled = false;
            gameObject.transform.parent.GetChild(1).gameObject.transform.GetChild(1).GetComponent<FollowModel>().setStartingPoint();
            gameObject.transform.parent.GetChild(2).gameObject.transform.GetChild(1).GetComponent<FollowModel>().setStartingPoint();
        }
    }
}
