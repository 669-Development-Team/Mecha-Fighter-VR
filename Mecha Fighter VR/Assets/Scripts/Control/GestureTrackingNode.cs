﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum Gesture
{
    NONE = 0,
    UP,
    DOWN,
    LEFT,
    RIGHT,
    FORWARD,
    BACK
}

public class GestureTrackingNode
{
	static private Vector3[] axes = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
	
    private SteamVR_Input_Sources controller;
	private GestureHandlerV2 handler;
	private Transform head;
	private Transform trackingObject;
	
    private Vector3 startPosition;
	private Matrix4x4 startWorldToLocal;
    private bool isRecording;
	
	public GestureTrackingNode(GestureHandlerV2 handler, Transform trackingObject, SteamVR_Input_Sources controller) {
		
		this.handler = handler;
		this.head = handler.headTransform;
		this.trackingObject = trackingObject;
		
		handler.grip.AddOnStateDownListener(GripDown, controller);
        handler.grip.AddOnStateUpListener(GripUp, controller);
		
        startPosition = Vector3.zero;
        isRecording = false;
	}

	// controller grip down starts gesture recording for this tracker
    private void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
		var startWorldToLocal = head.worldToLocalMatrix;
        startPosition = startWorldToLocal.MultiplyPoint(trackingObject.position);
        isRecording = true;
    }

    private void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
		if (!isRecording) return; // if recording has been externally terminated, exit this function without doing anything
		
		// finalizeGesture will terminate the recording of all trackers
		handler.finalizeGesture();
    }
	
	public bool currentlyRecording() {
		return isRecording;
	}
	
	private void terminateRecording() {
		isRecording = false;
	}

	// returns the current gesture being recorded, and terminates the recording session
	// if there is no current gesture being recorded, this function will return Gesture.NONE
    public Gesture getGesture() {
		
		if (!isRecording) return Gesture.NONE;
		
		var endWorldToLocal = head.worldToLocalMatrix;
		Vector3 endPosition = endWorldToLocal.MultiplyPoint(trackingObject.position);
        Vector3 diffPosition = (endPosition - startPosition).normalized;

        int maxGesture = 0;
        float maxDot = 0;
        for (int i = 0; i < 6; i++) {
            float dotProduct = Vector3.Dot(diffPosition, axes[i]);
            if (dotProduct > maxDot) {
                maxDot = dotProduct;
                maxGesture = i + 1;
            }
        }
		
		terminateRecording();
        
        return (Gesture) maxGesture;
    }
	
	
}
