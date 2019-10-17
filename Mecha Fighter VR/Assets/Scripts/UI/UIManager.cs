using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //The UI screens that are shown when the scene is loaded and being shown currently
    public GameObject defaultScreen;
    private GameObject currentScreen;
    //A list storing all of the screens in the scene
    private List<GameObject> menuScreens;

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

    //Find the menu screen with the given name and switch to it
    private void loadScreen(string screenName)
    {
        foreach (GameObject screen in menuScreens)
        {
            if (screen.name == screenName)
            {
                currentScreen.SetActive(false);
                screen.SetActive(true);
                currentScreen = screen;
                break;
            }
        }
    }

    //Simple funcitons for switching menu screens
    public void loadFindMatch()
    {
        loadScreen("Find Match");
    }

    public void loadSettings()
    {
        loadScreen("Settings");
    }

    public void loadExitConfirmation()
    {
        loadScreen("Exit Confirmation");
    }
}
