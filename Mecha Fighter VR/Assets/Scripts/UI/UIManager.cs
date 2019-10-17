using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //The default, previous, and current screens
    public GameObject defaultScreen;
    private GameObject previousScreen;
    private GameObject currentScreen;
    //A list storing all of the screens in the scene
    private List<GameObject> menuScreens;
    //The text field being edited if the onscreen keyboard is active
    private TextMesh textField;
    //The text inputted by the user
    private TextMesh input;

    void Start()
    {
        menuScreens = new List<GameObject>();

        //All all of the menu screens to a list
        foreach (Transform screen in gameObject.GetComponentsInChildren<Transform>(true))
            if (screen.gameObject.tag == "Menu Screen")
                menuScreens.Add(screen.gameObject);

        //Activate the default screen and set it as the current screen
        defaultScreen.SetActive(true);
        currentScreen = defaultScreen;
    }

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
        currentScreen.SetActive(false);
        screen.SetActive(true);
        previousScreen = currentScreen;
        currentScreen = screen;
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
        //Save the text mesh for the keyboard input
        input = findInputField("Input");
        //Set the field name and input boxes on the onscreen keyboard
        findInputField("Field Name").text = fieldName;
        input.text = textField.text;
    }

    //
    public void enterKey(string key)
    {
        input.text += key;
    }

    public void backspace()
    {
        input.text.Substring(0, input.text.Length - 1);
    }

    //Control functions for the menu buttons
    public void back() { loadScreen(previousScreen); }
    public void exit() { Application.Quit(); Debug.Break(); }
    public void applySettings() { back(); }
    //Functions for loading the onscreen keyboard
    public void loadInputUser() { loadOnscreenKeyboard("Username"); }
    public void loadInputPass() { loadOnscreenKeyboard("Password"); }
    public void loadInputPassConfirm() { loadOnscreenKeyboard("Confirm"); }
    public void confirmInput() { textField.text = input.text; back(); }
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