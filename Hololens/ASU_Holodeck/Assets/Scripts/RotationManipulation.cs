using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class RotationManipulation : MonoBehaviour, IManipulationHandler
{
    public bool RotationManipulationEnabled = false;
    private Vector3 previousTransform;

    public float RotationSpeed = 180.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        previousTransform = gameObject.transform.localEulerAngles;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (RotationManipulationEnabled)
        {
            Vector3 handMovement = eventData.CumulativeDelta * RotationSpeed;
            // Rotate Y axis
            gameObject.transform.localRotation = Quaternion.Euler(
                previousTransform.x,
                previousTransform.y + (RotationSpeed * eventData.CumulativeDelta.x),
                previousTransform.z);
            // Rotate X axis
            /*gameObject.transform.localRotation = Quaternion.Euler(
                previousTransform.x + (RotationSpeed * eventData.CumulativeDelta.y),
                previousTransform.y,
                previousTransform.z);
                */
            previousTransform = transform.localEulerAngles;
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {

    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {

    }
}
