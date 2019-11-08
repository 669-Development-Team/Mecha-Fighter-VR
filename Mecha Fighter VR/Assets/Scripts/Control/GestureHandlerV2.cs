using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandlerV2 : MonoBehaviour
{
    [SerializeField] private Stats.Health opponent;

	public Transform headTransform;
    public GestureTrackingNode leftTracker;
    public GestureTrackingNode rightTracker;

	private delegate void GestureAction();
	private Dictionary<int, GestureAction> gestureActions;
	private GestureAction currentAction = null;

    // Start is called before the first frame update
    void Start()
    {
        var uppercut    = GetComponent<Action.UppercutAbility>();
        var projectile  = GetComponent<Action.ProjectileAbility>();
        var groundPound = GetComponent<Action.GroundPoundAbility>();

        gestureActions = new Dictionary<int, GestureAction>();
		
		gestureActions.Add(
			makeGestureHash(Gesture.NONE, Gesture.UP), 
			delegate() { 
				Debug.Log("Registered Right Uppercut Gesture!");
                uppercut.ActivateAbility(opponent);
			}
		);
		
		gestureActions.Add(
            // Since I only have the right controller at the lab, I've set all gestures to only use the right controller.
            makeGestureHash(Gesture.NONE, Gesture.FORWARD), 
			delegate() { 
				Debug.Log("Registered Projectile Gesture!");
                projectile.ActivateAbility();
			}
		);
		
		gestureActions.Add(
			makeGestureHash(Gesture.NONE, Gesture.DOWN), 
			delegate() { 
				Debug.Log("Registered Ground Pound Gesture!");
                groundPound.ActivateAbility(opponent);
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
