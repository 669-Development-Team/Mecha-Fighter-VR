using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GestureHandlerV2 : MonoBehaviour
{
	public Transform headTransform;
	public Transform leftControllerTransform;
	public Transform rightControllerTransform;
	public SteamVR_Action_Boolean grip;

	private Dictionary<int, GestureAbility> abilityMap;
	private GestureTrackingNode leftTracker;
    private GestureTrackingNode rightTracker;

    // Start is called before the first frame update
    void Start()
    {
		abilityMap = new Dictionary<int, GestureAbility>();
		
		foreach (GestureAbility gestureAbility in GetComponents<GestureAbility>()) {
			
			int gestureHash = makeGestureHash(gestureAbility.leftGesture, gestureAbility.rightGesture);
			
			abilityMap.Add(gestureHash, gestureAbility);
		}
		
		leftTracker  = new GestureTrackingNode(this, leftControllerTransform, SteamVR_Input_Sources.LeftHand);
		rightTracker = new GestureTrackingNode(this, rightControllerTransform, SteamVR_Input_Sources.RightHand);
    }

    public void finalizeGesture() {
		
		Gesture gestureLeft = leftTracker.getGesture();
		Gesture gestureRight = rightTracker.getGesture();
		Debug.Log("Gesture recorded: " + gestureLeft + ", " + gestureRight);
		
		int gestureHash = makeGestureHash(gestureLeft, gestureRight);
		
		GestureAbility ability;
		try {
			ability = abilityMap[gestureHash];
		}
		catch (KeyNotFoundException e) {
			ability = null;
		}
		
		ability?.ActivateAbility();
	}
	
	private int makeGestureHash(Gesture g1, Gesture g2) {
		return ((int) g1) << 16 | ((int) g2);
	}
}
