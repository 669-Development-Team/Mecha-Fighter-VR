using UnityEngine;
using Valve.VR;

public class ControllerInteraction : MonoBehaviour
{
    //A reference to the hologram menu game object
    [SerializeField]
    private GameObject hologramMenu = null;
    //A reference to the UI Manager attatched to the hologram menu
    private UIManager uiManager;
    //Controller that the script is attatched to
    private SteamVR_Input_Sources controller;
    //The animation controller attatched to the glove model
    private Animator animController;
    //Button that the controller is currently in
    private GameObject selectedButton;

    //The amount of time the controller will vibrate after clicking a button
    private const float clickButtonDuration = 0.1f;
    //Weather or not the controller is inside of the hologram canvas
    private bool inCanvas = false;
    //
    private bool inButton = false;

    //Object that controls haptic feedback
    public SteamVR_Action_Vibration hapticController;

    void Start()
    {
        //Get the input controller and animation and ui scripts
        controller = GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource;
        animController = GetComponentInChildren<Animator>();
        uiManager = hologramMenu.GetComponent<UIManager>();
    }

    private void Update()
    {
        //If the controller is in the canvas, always use the point animation
        if (inCanvas)
            animController.SetBool("Point", true);
        //If the controller is not in the canvas but the grip button is pressed, use the point animation
        else
            animController.SetBool("Point", SteamVR_Actions._default.GrabGrip.GetState(controller));

        //If neither, use a blend between the rest and point animations based on the trigger input
        animController.SetFloat("Blend", SteamVR_Actions._default.Squeeze.GetAxis(controller));
    }

    //If the controller `s a UI element
    void OnTriggerEnter(Collider otherObject)
    {
        //A button was touched
        if (otherObject.tag == "Button")
        {
            //If a new screen just loaded and the controller is in a button,
            //don't allow the controller to interact until it has left the button
            if (uiManager.isFirstFrameOfScreen())
                inButton = true;

            //Only activate the button if the controller was not already in the button
            if (inButton == false)
            {
                pulse(1.0f, controller);
                selectedButton = otherObject.gameObject.transform.gameObject;

                //If the controller is in a button, perform the action of the button
                if (selectedButton != null && selectedButton.activeInHierarchy)
                {
                    selectedButton.GetComponent<ButtonInteraction>().action();
                    hapticController.Execute(0, clickButtonDuration, 100f, 0.3f, controller);
                }
            }
        }
        //The controller entered the hologram menu canvas
        else if(otherObject.tag == "Menu Screen")
        {
            inCanvas = true;
        }
    }

    void OnTriggerExit(Collider otherObject)
    {
        //The controller exited a button
        if (otherObject.tag == "Button")
            inButton = false;
        //The controller exited the hologram menu canvas
        if (otherObject.tag == "Menu Screen")
            inCanvas = false;
    }

    //If the controller isn't still vibrating from clicking a button, play a short vibration
    private void pulse(float amplitude, SteamVR_Input_Sources source)
    {
        hapticController.Execute(0, 0.1f, 0.1f, amplitude, source);
    }
}