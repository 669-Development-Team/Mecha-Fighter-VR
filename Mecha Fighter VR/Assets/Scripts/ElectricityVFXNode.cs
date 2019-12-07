using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityVFXNode : MonoBehaviour
{
    public Transform endPoint;
	public bool enabled;
	
	LineRenderer line;
	
	void Start()
	{
		line = GetComponent<LineRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
		if (enabled) {
			line.positionCount = 2;
			line.SetPositions(new Vector3[] { transform.position, endPoint.position });
		}
		else {
			line.positionCount = 0;
		}
    }
}
