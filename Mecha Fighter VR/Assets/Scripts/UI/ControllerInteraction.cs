using UnityEngine;
using Valve.VR;

public class ControllerInteraction : MonoBehaviour
{
    //Device that the script is attatched to
    private SteamVR_Input_Sources source;
    //Object that controls haptic feedback
    public SteamVR_Action_Vibration hapticController;

    void Start()
    {
        //Get the input source from the parent
        source = GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource;
    }

    void OnTriggerEnter(Collider otherObject)
    {
        //If the controller enters a UI button, pulse
        if(otherObject.tag == "UI")
            pulse(1.0f, source);
    }

    private void OnTriggerExit(Collider otherObject)
    {
        //If the controller exits a UI button, pulse
        if (otherObject.tag == "UI")
            pulse(0.1f, source);
    }

    private void pulse(float amplitude, SteamVR_Input_Sources source)
    {
        hapticController.Execute(0, 0.1f, 0.1f, amplitude, source);
    }
}
