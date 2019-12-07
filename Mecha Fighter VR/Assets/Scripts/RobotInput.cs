using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using Valve.VR;

public class RobotInput : MonoBehaviour
{
	public SteamVR_Action_Boolean WalkOnOff;
	public SteamVR_ActionSet steamVR_ActionSet;

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
		if (WalkOnOff.GetState(SteamVR_Input_Sources.Any)){
				robotAnimation.HandleInput("down w");
		}
		else{
			robotAnimation.HandleInput("up w");
		}
		
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
