using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MoveManipulation : MonoBehaviour, IManipulationHandler {
    public bool MoveManipulationEnabled = false;

    public float MoveSpeed = 0.1f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnManipulationStarted(ManipulationEventData eventData)
    {

    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (MoveManipulationEnabled)
        {
            Vector3 handMovement = eventData.CumulativeDelta * MoveSpeed;
            gameObject.transform.localPosition += handMovement;
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {

    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {

    }
}
