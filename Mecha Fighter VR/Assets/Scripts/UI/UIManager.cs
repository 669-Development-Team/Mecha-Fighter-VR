using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Default, previous, and current screens
    [SerializeField]
    private GameObject defaultScreen;
    private GameObject previousScreen;
    private GameObject currentScreen;
    //A list storing all of the screens in the scene
    private List<GameObject> menuScreens;
    //Text field being edited if the onscreen keyboard is active
    private TextMesh textField;
    //Text field to display the name of the field being edited
    [SerializeField]
    private TextMesh fieldDisplay;
    //Text field to display what the onscreen keyboard types
    [SerializeField]
    private TextMesh keyboardInput;

    //Speed at which menus open and close in seconds
    [SerializeField]
    private float menuLoadSpeed;
    //The percentage the menu needs to scale down every frame in order to dissapear in the specified time
    private float scalePerFrame;
    //A menu can be opening, closing, or neither
    private enum loadingState
    {
        None,
        Opening,
        Closing
    }
    //The loading state the UI is currently in
    private loadingState UILoadingState;
    //True if this is the first frame after the new screen loaded
    private bool firstFrameOfScreen = false;

    void Start()
    {
        //A containing all of the screens for faster screen lookup
        menuScreens = new List<GameObject>();
        //Compute percentage the closing/opening menu scales every frame
        scalePerFrame = 1f / (menuLoadSpeed * 60f);
        //Ensure that time is enabled
        Time.timeScale = 1;

        //All all of the menu screens to a list
        foreach (Transform screen in gameObject.GetComponentsInChildren<Transform>(true))
            if (screen.gameObject.tag == "Menu Screen")
                menuScreens.Add(screen.gameObject);

        //Activate the default screen and set it as the current screen
        defaultScreen.SetActive(true);
        currentScreen = defaultScreen;
    }

    private void FixedUpdate()
    {
        //Constantly reset the first frame boolean to false
        firstFrameOfScreen = false;

        //Play the closing and opening animations
        if (UILoadingState == loadingState.Closing)
            closeScreen();
        else if (UILoadingState == loadingState.Opening)
            openScreen();
    }

    //Getter function for firstFrameOfScreen
    public bool isFirstFrameOfScreen() { return firstFrameOfScreen; }

    //Switch to the screen given the name of the GameObject
    private void loadScreen(string screenName)
    {
        //If the menu object from the saved list and switch to it
        foreach (GameObject screen in menuScreens)
        {
            if (screen.name == screenName)
            {
                loadScreen(screen);
                break;
            }
        }
    }

    //Switch to the screen given a reference to the GameObject
    private void loadScreen(GameObject screen)
    {
        //Update the previous and current screens
        previousScreen = currentScreen;
        currentScreen = screen;

        //Set the scale of the new menu to 0 for when it is loaded
        currentScreen.transform.localScale = new Vector3(0, 0, 0);
        //Disable collisions in the screen to be closed
        disableCollisions(previousScreen);
        //Update the loading state to close the old screen
        UILoadingState = loadingState.Closing;
    }

    //Play the animation to close a screen
    private void closeScreen()
    {
        //If the old menu is more than one frame away from having a negative scale, decrese the scale
        if (previousScreen.transform.localScale.x - scalePerFrame > 0)
        {
            float newScale = previousScreen.transform.localScale.x - scalePerFrame;
            previousScreen.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        //If the old menu is completely closed, update the loading state and visibility of the screens
        else
        {
            UILoadingState = loadingState.Opening;
            previousScreen.SetActive(false);
            currentScreen.SetActive(true);
            //Disable collisions in the screeen to be opened
            disableCollisions(currentScreen);
        }
    }

    //Play the animation to open a screen
    private void openScreen()
    {
        //If the new menu is more than one frame away from full size, increase the scale
        if (currentScreen.transform.localScale.x + scalePerFrame < 1)
        {
            float newScale = currentScreen.transform.localScale.x + scalePerFrame;
            currentScreen.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        //Set the scale of the new menu to full and update the loading state
        else
        {
            currentScreen.transform.localScale = new Vector3(1, 1, 1);
            UILoadingState = loadingState.None;
            enableCollisions(currentScreen);
            firstFrameOfScreen = true;
        }
    }

    //Turn off all collision triggers in the game object if it is active
    private void disableCollisions(GameObject screen)
    {
        foreach (BoxCollider collider in screen.GetComponentsInChildren<BoxCollider>())
            collider.enabled = false;
    }

    //Turn on all collision triggers in the game object if it is active
    private void enableCollisions(GameObject screen)
    {
        foreach (BoxCollider collider in screen.GetComponentsInChildren<BoxCollider>())
            collider.enabled = true;
    }

    //Clear all of the input fields on the current screen
    private void clearInputs()
    {
        //Search through all buttons
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Button"))
            //Search through the children of the button for inputs
            foreach (Transform child in button.transform)
                if (child.name == "Input")
                    //Clear the text
                    child.GetComponentInChildren<TextMesh>().text = "";
    }

    //Search through the scene for active text fields with the given name
    private TextMesh findInputField(string fieldName)
    {
        //Search through all buttons with the corresponsding name
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Button"))
            if (button.name == fieldName)
                //Search through the children of the button for inputs
                foreach (Transform child in button.transform)
                    if (child.name == "Input")
                        return child.GetComponentInChildren<TextMesh>();

        return null;
    }

    //Switch to the onscreen keyboard and manage the input fields
    private void loadOnscreenKeyboard(string fieldName)
    {
        //Save the text field being edited
        textField = findInputField(fieldName);
        //Load the onscreen keyboard
        loadScreen("Onscreen Keyboard");
        //Set the field name and input boxes on the onscreen keyboard
        fieldDisplay.text = fieldName;
        keyboardInput.text = textField.text;
    }

    public void enterKey(string key)
    {
        keyboardInput.text += key;
    }

    //Remove the last character in the string
    public void backspace()
    {
        if(keyboardInput.text.Length > 0)
            keyboardInput.text = keyboardInput.text.Substring(0, keyboardInput.text.Length - 1);
    }

    //Control functions for the menu buttons
    public void back() { loadScreen(previousScreen); }
    public void exit() { Application.Quit(); Debug.Break(); }
    public void applySettings() { back(); }
    //Functions for loading the onscreen keyboard
    public void loadInputUser() { loadOnscreenKeyboard("Username"); }
    public void loadInputPass() { loadOnscreenKeyboard("Password"); }
    public void loadInputPassConfirm() { loadOnscreenKeyboard("Confirm"); }
    public void confirmInput() { textField.text = keyboardInput.text; back(); }
    //Make calls to the database for login and account creation
    public void submitLogin() { clearInputs(); loadMainMenuSignedIn(); }
    public void submitCreateAccount() { clearInputs(); loadMainMenuSignedIn(); }
    //Functions for only changing the menu screen
    public void loadMainMenuSignedOut() { loadScreen("Main Menu Signed Out"); }
    public void loadMainMenuSignedIn() { loadScreen("Main Menu Signed In"); }
    public void loadSettings() { loadScreen("Settings"); }
    public void loadExitConfirmation() { loadScreen("Exit Confirmation"); }
    public void loadAuthorization() { loadScreen("Authorization"); }
    public void loadLogin() { loadScreen("Login"); }
    public void loadCreateAccount() { loadScreen("Create Account"); }
    public void loadFindMatch() { loadScreen("Find Match"); }
}