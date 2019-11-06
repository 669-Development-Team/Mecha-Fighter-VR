using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandlerV2 : MonoBehaviour
{
	public Transform headTransform;
    public GestureTrackingNode leftTracker;
    public GestureTrackingNode rightTracker;

	private delegate void GestureAction();
	private Dictionary<int, GestureAction> gestureActions;
	private GestureAction currentAction = null;

    // Start is called before the first frame update
    void Start()
    {
        gestureActions = new Dictionary<int, GestureAction>();
		
		gestureActions.Add(
			makeGestureHash(Gesture.NONE, Gesture.UP), 
			delegate() { 
				Debug.Log("Registered Right Uppercut Gesture!"); 
			}
		);
		
		gestureActions.Add(
			makeGestureHash(Gesture.FORWARD, Gesture.FORWARD), 
			delegate() { 
				Debug.Log("Registered Projectile Gesture!"); 
				GetComponent<Action.ProjectileAbility>().ActivateAbility();
			}
		);
		
		gestureActions.Add(
			makeGestureHash(Gesture.DOWN, Gesture.DOWN), 
			delegate() { 
				Debug.Log("Registered Ground Pound Gesture!"); 
			}
		);
		
		leftTracker.initialize(this, headTransform);
		rightTracker.initialize(this, headTransform);
    }
	
	void Update() {
		
		if (currentAction != null) {
			currentAction();
			currentAction = null;
		}
	}

    public void finalizeGesture() {
		
		Gesture gestureLeft = leftTracker.getGesture();
		Gesture gestureRight = rightTracker.getGesture();
		Debug.Log("Gesture recorded: " + gestureLeft + ", " + gestureRight);
		
		int gestureHash = makeGestureHash(gestureLeft, gestureRight);
		
		try {
			currentAction = gestureActions[gestureHash];
		}
		catch (KeyNotFoundException e) {
			currentAction = null;
		}
		
		leftTracker.terminateRecording();
		rightTracker.terminateRecording();
	}
	
	private int makeGestureHash(Gesture g1, Gesture g2) {
		return ((int) g1) << 16 | ((int) g2);
	}
}
