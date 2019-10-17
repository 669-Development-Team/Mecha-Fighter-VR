using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    //The materials for when the button is being hovered over or not
    public Material unselected;
    public Material selected;

    void OnTriggerEnter(Collider otherObject)
    {
        transform.GetComponent<MeshRenderer>().material = selected;
    }

    private void OnTriggerExit(Collider otherObject)
    {
        transform.GetComponent<MeshRenderer>().material = unselected;
    }
}
