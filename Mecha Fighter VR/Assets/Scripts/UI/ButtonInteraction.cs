using UnityEngine;
using UnityEngine.Events;

public class ButtonInteraction : MonoBehaviour
{
    //The material for when the button
    public Material unselected;
    //The function that gets called when the button is activated
    public UnityEvent buttonAction;

    //If the button is activated, invoke the associated function
    public void action()
    {
        transform.GetComponent<MeshRenderer>().material = unselected;
        buttonAction.Invoke();
    }
}
