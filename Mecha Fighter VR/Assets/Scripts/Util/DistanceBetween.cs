using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBetween : MonoBehaviour
{
	public Transform other;
	public float distance;

    // Update is called once per frame
    void OnDrawGizmos()
    {
		if (other == null) return;
        distance = Vector3.Distance(other.position, transform.position);
    }
}
