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
    //The keyboard key that the controller is hovering over
    private GameObject highlightedKey = null;
    //Button that the controller is currently in
    private GameObject selectedButton;
    //The tip of the glove's finger for casting rays
    private Transform fingertip;

    //The amount of time the controller will vibrate after clicking a button
    private const float clickButtonDuration = 0.1f;
    //Weather or not the controller is inside of the hologram canvas
    private bool inCanvas = false;
    //Weather or not the controller is inside a button
    private bool inButton = false;

    //Object that controls haptic feedback
    public SteamVR_Action_Vibration hapticController;


    void Start()
    {
        //Get the input controller and animation and ui scripts
        controller = GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource;
        animController = GetComponentInChildren<Animator>();
        uiManager = hologramMenu.GetComponent<UIManager>();
        //Find the transform for the fingertip by finding the collider and getting the parent's transform
        fingertip = gameObject.GetComponentInChildren<SphereCollider>().transform.parent.transform;
    }

    private void Update()
    {
        updateAnimation();
        hoverOverKey();
    }

    private void updateAnimation()
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

    //Check if the controller is hovering over a keyboard key
    private void hoverOverKey()
    {
        //Get the origin for the raycast from the sphere collider attatched to the glove
        Vector3 raycastOrigin = fingertip.transform.position;

        //Find any collisions with a key object
        RaycastHit[] hitList = Physics.RaycastAll(raycastOrigin, Vector3.forward, Mathf.Infinity);

        foreach(RaycastHit hit in hitList)
        {
            if (hit.transform.parent.tag == "Row")
            {
                //If the collided object is different from the previous frame, update the materials
                if (highlightedKey != hit.transform.gameObject)
                {
                    //Unselect the previously highlighted key if there is one
                    if(highlightedKey)
                        highlightedKey.GetComponent<KeyboardButton>().unHighlight();

                    //Highlight the new one
                    hit.transform.GetComponent<KeyboardButton>().highlight();
                    highlightedKey = hit.transform.gameObject;
                }

                return;
            }
        }

        //If no keys were hovered over, unhighlight the old key
        if(highlightedKey)
        {
            highlightedKey.GetComponent<KeyboardButton>().unHighlight();
            highlightedKey = null;
        }
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
        else if(otherObject.tag == "Hologram Menu Canvas")
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
        if (otherObject.tag == "Hologram Menu Canvas")
            inCanvas = false;
    }

    //If the controller isn't still vibrating from clicking a button, play a short vibration
    private void pulse(float amplitude, SteamVR_Input_Sources source)
    {
        hapticController.Execute(0, 0.1f, 0.1f, amplitude, source);
    }
}