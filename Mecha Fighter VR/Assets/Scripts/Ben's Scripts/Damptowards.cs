using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damptowards : MonoBehaviour
{
	public Transform target;
	public float time;
	
	Vector3 currentVelocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, time);
    }
}
