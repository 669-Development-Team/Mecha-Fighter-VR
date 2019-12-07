using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class IndexInput : MonoBehaviour
{
    public SteamVR_Action_Vector2 JoyStickAction = null;
    public SteamVR_Action_Vector2 TrackpadAction = null;

    public SteamVR_Action_Single SqueezeAction = null;
    public SteamVR_Action_Boolean GripAction = null;
    public SteamVR_Action_Boolean PinchAction = null;

    public SteamVR_Action_Skeleton SkeletonAction = null;


  

    // Update is called once per frame
    void Update()
    {
        Joystick();
        Trackpad();
        Squeeze();

        //Grip();
        //Pinch();
        //Skeleton();
    }

    private void Joystick()
    {
        if(JoyStickAction.axis == Vector2.zero)
        {
            return;
        }
        print("Joystick: " + JoyStickAction.axis);
    }

    private void Trackpad()
    {
        if (TrackpadAction.axis == Vector2.zero)
        {
            return;
        }
        print("Trackpad: " + TrackpadAction.axis);
    }

    private void Squeeze()
    {
        if (SqueezeAction.axis == 0.0f)
        {
            return;
        }
        print("Squeeze: " + SqueezeAction.axis);

    }

    private void Grip()
    {

    }

    private void Pinch()
    {

    }

    private void Skeleton()
    {

    }
}
