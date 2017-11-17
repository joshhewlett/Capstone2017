using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ScaleManipulation : MonoBehaviour, IManipulationHandler
{
    public bool ScaleManipulationEnabled = false;
    private Vector3 previousTransform;

    public float MAX_X_SCALE = 1.2f,
                 MAX_Y_SCALE = 1.2f,
                 MAX_Z_SCALE = 1.2f,
                 MIN_X_SCALE = 0.1f,
                 MIN_Y_SCALE = 0.1f,
                 MIN_Z_SCALE = 0.1f;

    public float ScaleSpeed = 0.2f;
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
        previousTransform = gameObject.transform.localScale;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (ScaleManipulationEnabled)
        {
            transform.localScale = rangedScaleCalculator(eventData.CumulativeDelta, previousTransform);
            // Re-assign our previous transform so as long as manipulation continues
            // we can pickup where we left off with transforming values.
            previousTransform = transform.localScale;
        }
    }

    private Vector3 rangedScaleCalculator(Vector3 manipulationData, Vector3 prevScale)
    {
        Vector3 tempScale = transform.localScale;
        Vector3 tempPrevScale = previousTransform;

        // We use the negative z value because pointing outward gives a negative z and we want
        // that to be treated as a positive z in world scale.
        tempPrevScale.x = tempPrevScale.x + (-manipulationData.z * 2.0f);
        tempPrevScale.y = tempPrevScale.y + (-manipulationData.z * 2.0f);
        tempPrevScale.z = tempPrevScale.z + (-manipulationData.z * 2.0f);
        tempScale = tempPrevScale;

        tempScale.x = Mathf.Clamp(tempScale.x, MIN_X_SCALE, MAX_X_SCALE);
        tempScale.y = Mathf.Clamp(tempScale.y, MIN_Y_SCALE, MAX_Y_SCALE);
        tempScale.z = Mathf.Clamp(tempScale.z, MIN_Z_SCALE, MAX_Z_SCALE);

        return tempScale;
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {

    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {

    }
}
