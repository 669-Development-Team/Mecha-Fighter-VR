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
    [SerializeField] private float transitionSpeed;

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
    private bool transitioning = false;
    private Vector3 transitionTarget;

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
        //When both sticks are clicked in, set the location to lerp to
        if (currLeftStickState && currRightStickState && (!prevLeftStickState || !prevRightStickState))
        {
            if (thirdPerson)
            {
                transitionTarget = Vector3.zero;
                pilot.SetActive(false);
            }
            else
            {
                transitionTarget = thirdPersonCameraDisplacement;
            }

            transitioning = true;
        }

        //If transitioning, move the camera rig closer to the target
        if (transitioning)
        {
            cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, transitionTarget, transitionSpeed);
            Vector3 distance = transitionTarget - cameraRig.transform.position;

            //If the camera rig is within 0.1 units of the target, switch pov
            if (distance.magnitude < 0.1f)
            {
                //Switch to first person
                if (thirdPerson)
                {
                    setFirstPersonTargets();
                    thirdPerson = false;
                }
                //Switch to third person
                else
                {
                    setThirdPersonTargets();
                    thirdPerson = true;
                    pilot.SetActive(true);
                }

                transitioning = false;
            }
        }
    }

    private void setFirstPersonTargets()
    {
        cameraRig.transform.position = Vector3.zero;
        mechIKScript.solver.spine.headTarget = firstPersonHeadTarget.transform;
        mechIKScript.solver.leftArm.target = firstPersonLeftHandTarget.transform;
        mechIKScript.solver.rightArm.target = firstPersonRightHandTarget.transform;
    }

    private void setThirdPersonTargets()
    {
        cameraRig.transform.position = thirdPersonCameraDisplacement;
        mechIKScript.solver.spine.headTarget = thirdPersonHeadTarget.transform;
        mechIKScript.solver.leftArm.target = thirdPersonLeftHandTarget.transform;
        mechIKScript.solver.rightArm.target = thirdPersonRightHandTarget.transform;
    }
}
