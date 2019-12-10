using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSize : MonoBehaviour
{
    [SerializeField] private float armScale = 1f;
    [SerializeField] private GameObject leftArm;
    [SerializeField] private GameObject rightArm;

    void Start()
    {
        leftArm.transform.localScale = new Vector3(armScale, armScale, armScale);
        rightArm.transform.localScale = new Vector3(armScale, armScale, armScale);
    }

    public float getScale()
    {
        return armScale;
    }
}
