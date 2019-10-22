using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public float result;
	public Transform target;
	public RootMotion.FinalIK.LookAtIK aimIK;

    // Update is called once per frame
    void LateUpdate()
    {
		var directionalVector = target.rotation * Vector3.forward;
        result = Vector3.Dot(transform.position - target.position, directionalVector);
		if (result > 0.2f) {
			aimIK.enabled = true;
			aimIK.solver.IKPosition = transform.position;
		}
		else aimIK.enabled = false;
    }
}
