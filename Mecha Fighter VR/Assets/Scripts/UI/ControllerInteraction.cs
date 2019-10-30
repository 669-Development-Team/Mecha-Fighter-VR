using UnityEngine;
using Valve.VR;

public class ControllerInteraction : MonoBehaviour
{
    //Device that the script is attatched to
    private SteamVR_Input_Sources source;
    //Button that the controller is currently in
    private GameObject selectedButton;

    //The amount of time the controller will vibrate after clicking a button
    private const float clickButtonDuration = 0.1f;
    //The time at which the screen changed last
    private float timeLastScreenChange = 0;

    //The corresponding controller for the script
    public SteamVR_Input_Sources controller;
    //Object that controls haptic feedback
    public SteamVR_Action_Vibration hapticController;

    //Get the input source from the parent
    void Start()
    {
        source = GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource;
    }

    //If the controller enters a UI button
    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Button")
        {
            pulse(1.0f, source);
            selectedButton = otherObject.gameObject.transform.gameObject;

            //If the controller is in a button, perform the action of the button
            if (selectedButton != null && selectedButton.activeInHierarchy)
            {
                selectedButton.GetComponent<ButtonInteraction>().action();
                hapticController.Execute(0, clickButtonDuration, 100f, 0.3f, source);
                timeLastScreenChange = Time.time;
            }
        }
    }

    //If the controller isn't still vibrating from clicking a button, play a short vibration
    private void pulse(float amplitude, SteamVR_Input_Sources source)
    {
        if(Time.time - timeLastScreenChange > clickButtonDuration)
            hapticController.Execute(0, 0.1f, 0.1f, amplitude, source);
    }
}
