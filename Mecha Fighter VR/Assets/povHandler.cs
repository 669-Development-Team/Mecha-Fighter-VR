using RootMotion.FinalIK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class povHandler : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean joystickClick;
    [SerializeField] private Vector3 thirdPersonCameraDisplacement;

    private GameObject cameraRig;
    private GameObject pilot;
    private VRIK mechIKScript;
    private GameObject firstPersonHeadTarget;
    private GameObject firstPersonLeftHandTarget;
    private GameObject firstPersonRightHandTarget;
    private GameObject thirdPersonHeadTarget;
    private GameObject thirdPersonLeftHandTarget;
    private GameObject thirdPersonRightHandTarget;

    private bool thirdPerson = true;
    private bool prevLeftStickState = false;
    private bool currLeftStickState = false;
    private bool prevRightStickState = false;
    private bool currRightStickState = false;

    void Start()
    {
        joystickClick.AddOnUpdateListener(joystickClicked, SteamVR_Input_Sources.LeftHand);
        joystickClick.AddOnUpdateListener(joystickClicked, SteamVR_Input_Sources.RightHand);

        cameraRig = GameObject.Find("[CameraRig]");
        pilot = GameObject.Find("Pilot");
        mechIKScript = gameObject.GetComponentInChildren<VRIK>();

        firstPersonHeadTarget = GameObject.Find("Camera");
        firstPersonLeftHandTarget = GameObject.Find("Controller (left)");
        firstPersonRightHandTarget = GameObject.Find("Controller (right)");

        thirdPersonHeadTarget = GameObject.Find("Head Target for Mech IK");
        thirdPersonLeftHandTarget = GameObject.Find("Left Hand Target for Mech IK");
        thirdPersonRightHandTarget = GameObject.Find("Right Hand Target for Mech IK");
    }

    private void joystickClicked(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        if (fromSource == SteamVR_Input_Sources.LeftHand)
        {
            prevLeftStickState = currLeftStickState;
            currLeftStickState = newState;
        }
        else
        {
            prevRightStickState = currRightStickState;
            currRightStickState = newState;
        }
    }

    void Update()
    {
        if (currLeftStickState && currRightStickState && (!prevLeftStickState || !prevRightStickState))
        {
            if (thirdPerson)
            {
                firstPersonPOV();
                thirdPerson = false;
            }
            else
            {
                thirdPersonPOV();
                thirdPerson = true;
            }
        }
    }

    private void firstPersonPOV()
    {
        cameraRig.transform.position = Vector3.zero;
        mechIKScript.solver.spine.headTarget = firstPersonHeadTarget.transform;
        mechIKScript.solver.leftArm.target = firstPersonLeftHandTarget.transform;
        mechIKScript.solver.rightArm.target = firstPersonRightHandTarget.transform;

        pilot.SetActive(false);
    }

    private void thirdPersonPOV()
    {
        cameraRig.transform.position = thirdPersonCameraDisplacement;
        mechIKScript.solver.spine.headTarget = thirdPersonHeadTarget.transform;
        mechIKScript.solver.leftArm.target = thirdPersonLeftHandTarget.transform;
        mechIKScript.solver.rightArm.target = thirdPersonRightHandTarget.transform;

        pilot.SetActive(true);
    }
}
