using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITargetPosition : MonoBehaviour
{
    //The game object to align the UI with
    [SerializeField]
    private GameObject targetObject;
    //The distance to the UI from the player's headset
    [SerializeField]
    private Vector3 UIDisplacement;
    //The minimum distance away from the UI the target has to be to trigger a movement
    [SerializeField]
    private float movementThreshold;
    //Determines wheather the UI is at its target location
    private bool atTarget = false;
    //How close the UI needs to get for it to count as being at the target
    private const float targetTolerance = 0.01f;

    void Update()
    {
        //Remove the x component of the target position
        Vector3 targetPosition = new Vector3(0, targetObject.transform.position.y, targetObject.transform.position.z);
        //Calculate the distance between the UI along the XY plane
        float magnitude = Vector3.Magnitude(targetPosition + UIDisplacement - gameObject.transform.position);

        //If the UI is close enough to the target, set the atTarget boolean to true
        if (magnitude <= targetTolerance)
            atTarget = true;

        //Only move the UI if the target it too far away or if it is currently moving toward the target
        if (magnitude > movementThreshold || atTarget == false)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition + UIDisplacement, 0.05f);
            atTarget = false;
        }
    }
}
