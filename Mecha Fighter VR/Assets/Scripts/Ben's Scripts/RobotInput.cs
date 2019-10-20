using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class RobotInput : MonoBehaviour
{
	RobotAnimation robotAnimation;
	List<string> TrackedKeyUp = new List<string> { "w", "f", "a", "s", "g" };
	List<string> TrackedKeyDown = new List<string> { "w" };
	
    // Start is called before the first frame update
    void Start()
    {
		robotAnimation = GetComponent<RobotAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
		
        foreach (string key in TrackedKeyUp) {
			if (Input.GetKeyDown(key)) {
				robotAnimation.HandleInput("down " + key);
			}
		}
		foreach (string key in TrackedKeyDown) {
			if (Input.GetKeyUp(key)) {
				robotAnimation.HandleInput("up " + key);
			}
		}
    }
}
