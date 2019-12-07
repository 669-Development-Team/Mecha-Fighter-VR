using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButton : MonoBehaviour
{
    //The materials for when the button is and isn't highlighted
    [SerializeField]
    private Material highlightedMat;
    [SerializeField]
    private Material unhighlightedMat;
    //The UI manager script attatched to the root object of the UI
    private UIManager uiManager;
    //The character that the key butotn is associated with
    private string key;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        key = gameObject.name;
    }

    public void enterKey()
    {
        if (key == "<-")
            uiManager.backspace();
        else if (key == "Space")
            uiManager.enterKey(" ");
        else if (key == "Tab")
            uiManager.enterKey("\t");
        else if (key == "Caps")
            uiManager.enterKey("");
        else if (key == "Shift")
            uiManager.enterKey("");
        else if (key == "Enter")
            uiManager.enterKey("");
        else
            uiManager.enterKey(key);
    }

    public void highlight()
    {
        transform.GetComponent<MeshRenderer>().material = highlightedMat;
    }

    public void unHighlight()
    {
        transform.GetComponent<MeshRenderer>().material = unhighlightedMat;
    }
}