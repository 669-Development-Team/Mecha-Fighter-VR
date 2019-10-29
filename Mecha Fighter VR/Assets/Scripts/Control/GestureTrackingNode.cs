using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GestureTrackingNode : MonoBehaviour
{
    public enum Gestures
    {
        NONE = 0,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        FORWARD,
        BACK
    }
    
    [SerializeField] private SteamVR_Action_Boolean grip;
    [SerializeField] private Transform trackingObject;
    [SerializeField] private SteamVR_Input_Sources controller;

    private Vector3 startPosition;
    private List<Gestures> gesturesThisFrame;
    private Vector3[] axes;
    private bool isRecording;

    // Start is called before the first frame update
    void Start()
    {
        grip.AddOnStateDownListener(GripDown, controller);
        grip.AddOnStateUpListener(GripUp, controller);
        gesturesThisFrame = new List<Gestures>();
        axes = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        startPosition = Vector3.zero;
        isRecording = false;
    }

    private void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        startPosition = trackingObject.position;
        isRecording = true;
    }

    private void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Vector3 endPosition = trackingObject.position;
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
        
        if (maxGesture != 0) gesturesThisFrame.Add((Gestures) maxGesture);
        isRecording = false;
    }

    public bool GripHeldDown() {
        return isRecording;
    }

    private bool GetGesture(Gestures gesture) {
        return gesturesThisFrame.Contains(gesture);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gesturesThisFrame.Clear();
    }
}
