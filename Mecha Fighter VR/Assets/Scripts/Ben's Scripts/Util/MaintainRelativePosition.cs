using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRelativePosition : MonoBehaviour
{
	[Header("Between")]
	public Transform parent;
	public Transform child;

    // Update is called once per frame
    void LateUpdate()
    {
        var parent2child = child.position - parent.position;
		Vector3 localRelativePosition = parent.transform.InverseTransformDirection(parent2child).normalized;
		var magnitude = Vector3.Distance(child.position, parent.position);
		transform.localPosition = localRelativePosition * magnitude;
    }
}
