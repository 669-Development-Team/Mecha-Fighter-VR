using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    private bool displayConsole = false;
    private GameObject console;
    private string input = "";
    private bool initialized = false;
    private RegisterUserManager registerUserManager;
    private LoginUserManager loginUserManager;
    private JoinGameManager joinGameManager;

    void Update()
    {
        if (Input.GetKeyDown("`"))
            displayConsole = !displayConsole;
    }

    void OnGUI()
    {
        if(!initialized)
        {
            GameObject core = GameObject.Find("Core");
            registerUserManager = core.GetComponent<RegisterUserManager>();
            loginUserManager = core.GetComponent<LoginUserManager>();
            joinGameManager = core.GetComponent<JoinGameManager>();
        }

        //Get any key presses
        Event e = Event.current;

        if (displayConsole)
        {
            //Display the console
            input = GUI.TextField(new Rect(0, Screen.height - 25, Screen.width, 25), input);

            //Run the command if enter is pressed
            if(e.keyCode == KeyCode.Return && e.type == EventType.KeyUp)
            {
                bool reset = false;

                //Split the input by spaces
                string[] splitInput = input.Split();

                switch (splitInput[0])
                {
                    case "register":
                        registerUserManager.Register(splitInput[1], splitInput[2], splitInput[3]);
                        reset = true;
                        break;

                    case "login":
                        loginUserManager.Login(splitInput[1], splitInput[2]);
                        reset = true;
                        break;

                    case "join":
                        joinGameManager.JoinGame();
                        reset = true;
                        break;

                    default:
                        break;
                }

                if(reset)
                {
                    input = "";
                    displayConsole = false;
                }
            }
        }
    }
}