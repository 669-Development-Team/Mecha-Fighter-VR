using RootMotion.FinalIK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR;

public class povHandler : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean joystickClick;
    [SerializeField] private Vector3 thirdPersonCameraDisplacement;
    [SerializeField] private float transitionSpeed;

    private GameObject cameraRig;
    private GameObject pilot;

    private GameObject headTarget;
    private GameObject leftHandTarget;
    private GameObject rightHandTarget;

    private PositionConstraint headTargetContraint;
    private PositionConstraint leftHandTargetContraint;
    private PositionConstraint rightHandTargetContraint;

    private POV pointOfView = POV.third;
    private bool transitioning = false;
    private Vector3 transitionTarget;

    private bool acceptInputs = true;
    private bool transitionAfterSpecial;

    private bool prevLeftStickState = false;
    private bool currLeftStickState = false;
    private bool prevRightStickState = false;
    private bool currRightStickState = false;

    public enum POV
    {
        first,
        third
    }

    void Start()
    {
        joystickClick.AddOnUpdateListener(joystickClicked, SteamVR_Input_Sources.LeftHand);
        joystickClick.AddOnUpdateListener(joystickClicked, SteamVR_Input_Sources.RightHand);

        cameraRig = GameObject.Find("[CameraRig]");
        pilot = GameObject.Find("Pilot");

        headTarget = GameObject.Find("Head Target for Mech IK");
        leftHandTarget = GameObject.Find("Left Hand Target for Mech IK");
        rightHandTarget = GameObject.Find("Right Hand Target for Mech IK");

        headTargetContraint = headTarget.GetComponent<PositionConstraint>();
        leftHandTargetContraint = leftHandTarget.GetComponent<PositionConstraint>();
        rightHandTargetContraint = rightHandTarget.GetComponent<PositionConstraint>();
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

    void FixedUpdate()
    {
        //When both sticks are clicked in, start the transition to the new pov
        if (currLeftStickState && currRightStickState
            && (!prevLeftStickState || !prevRightStickState)
            && acceptInputs && !transitioning)
        {
            if (pointOfView == POV.third)
                setPOV(POV.first);
            else
                setPOV(POV.third);
        }

        //If transitioning, move the camera rig closer to the target
        if (transitioning)
        {
            //Move the camera rig
            cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, transitionTarget, transitionSpeed);
            
            //Update the target offsets to maintain their local position
            if(pointOfView == POV.third)
                setTargetOffsets(transitionTarget - cameraRig.transform.position);
            else
                setTargetOffsets((transitionTarget - cameraRig.transform.position) - thirdPersonCameraDisplacement);

            //Find the offset between the camera rig and the target
            Vector3 distance = transitionTarget - cameraRig.transform.position;

            //If the camera rig is within 0.01 units of the target, switch pov
            if (distance.magnitude < 0.01f)
            {
                //Switch to first person
                if (pointOfView == POV.third)
                {
                    setTargetOffsets(Vector3.zero);
                    pointOfView = POV.first;
                }
                //Switch to third person
                else
                {
                    setTargetOffsets(-thirdPersonCameraDisplacement);
                    pilot.SetActive(true);
                    pointOfView = POV.third;
                }

                transitioning = false;
            }
        }
    }

    private void setTargetOffsets(Vector3 newOffset)
    {
        headTargetContraint.translationOffset = newOffset;
        leftHandTargetContraint.translationOffset = newOffset;
        rightHandTargetContraint.translationOffset = newOffset;
    }

    public POV getPOV()
    {
        return pointOfView;
    }

    public void setPOV(POV newPOV)
    {
        //When moving to first person, start by hiding the pilot
        if (newPOV == POV.first)
        {
            pilot.SetActive(false);
            transitionTarget = cameraRig.transform.position - thirdPersonCameraDisplacement;
        }
        else
            transitionTarget = cameraRig.transform.position + thirdPersonCameraDisplacement;

        //Indicate that the transition is taking place
        transitioning = true;
    }

    public void startSpecialAttack()
    {
        if (pointOfView == POV.first)
        {
            setPOV(POV.third);
            transitionAfterSpecial = true;
        }

        acceptInputs = false;
    }

    public void endSpecialAttack()
    {
        if (transitionAfterSpecial)
            setPOV(POV.first);

        acceptInputs = true;
    }
}
