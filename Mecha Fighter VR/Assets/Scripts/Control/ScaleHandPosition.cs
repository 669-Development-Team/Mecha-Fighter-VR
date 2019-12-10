using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandPosition : MonoBehaviour
{
    [SerializeField] private GameObject playerRoot;
    [SerializeField] private GameObject headTarget;
    [Tooltip("How far away the chest is from the head")]
    [SerializeField] private Vector3 chestDisplacement;
    private ArmSize armSizeScript;
    private float armSize;

    void Start()
    {
        armSize = playerRoot.GetComponent<ArmSize>().getScale();
    }

    void LateUpdate()
    {
        //The displacement vector between the chest and the hand
        Vector3 displacement = transform.localPosition - headTarget.transform.localPosition;
        //Scale the displacement by the size of the arms
        displacement *= armSize;
        //Add the displacement back on to the arm position
        transform.localPosition = headTarget.transform.localPosition + displacement;
    }
}
