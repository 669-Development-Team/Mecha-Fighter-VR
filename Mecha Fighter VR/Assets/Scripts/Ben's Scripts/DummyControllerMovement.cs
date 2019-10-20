using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyControllerMovement : MonoBehaviour
{
	public float timeOffset;
	public float timeMultiplier;
	public float radius;
	
	Vector3 originalPos;
	
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 circularPosition = new Vector3(Mathf.Cos(Mathf.Cos(Time.time) * (Time.time * timeMultiplier + timeOffset)), Mathf.Sin(Time.time * timeMultiplier + timeOffset), 0) * radius;
		transform.localPosition = originalPos + circularPosition;
    }
}
