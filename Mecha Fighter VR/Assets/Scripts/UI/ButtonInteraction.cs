using UnityEngine;
using UnityEngine.Events;

public class ButtonInteraction : MonoBehaviour
{
    //The materials for when the button is being hovered over or not
    public Material unselected;
    public Material selected;
    //The function that gets called when the button is activated
    public UnityEvent buttonAction;

    //If the button is selected, change the material
    void OnTriggerEnter(Collider otherObject)
    {
        transform.GetComponent<MeshRenderer>().material = selected;
    }

    //If the button is unselected, change the material
    private void OnTriggerExit(Collider otherObject)
    {
        transform.GetComponent<MeshRenderer>().material = unselected;
    }

    //If the button is activated, invoke the associated function
    public void action()
    {
        buttonAction.Invoke();
    }
}
