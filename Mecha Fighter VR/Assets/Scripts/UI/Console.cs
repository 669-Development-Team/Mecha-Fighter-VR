using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    private bool displayConsole = false;
    private GameObject console;
    private string input = "";
    private JoinGameManager joinGameManager;

    void Update()
    {
        if (Input.GetKeyDown("`"))
            displayConsole = !displayConsole;
    }

    void OnGUI()
    {
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

                if (input == "join")
                {
                    GameObject core = GameObject.Find("Core");
                    joinGameManager = core.GetComponent<JoinGameManager>();
                    joinGameManager.JoinGame();
                    reset = true;
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