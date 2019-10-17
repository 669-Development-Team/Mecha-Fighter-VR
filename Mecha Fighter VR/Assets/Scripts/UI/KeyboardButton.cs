using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButton : MonoBehaviour
{
    //The UI manager script attatched to the root object of the UI
    UIManager uiManager;
    //The character that the key butotn is associated with
    string key;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        key = gameObject.name;
    }

    public void enterKey()
    {
        if (key == "<-")
            uiManager.backspace();
        else
            uiManager.enterKey(key);
    }
}