using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRelativePosition : MonoBehaviour
{	
	public Transform parent;
	public Transform child;
	
	private Vector3 restPosition;
	
	void Start() { restPosition = transform.localPosition; }
	
    // Update is called once per frame
    void Update()
    {
        Matrix4x4 worldToParent = parent.worldToLocalMatrix;
		
		Vector3 relativePosition = worldToParent.MultiplyPoint(child.position);
		
		transform.localPosition = restPosition + relativePosition;
    }
}
