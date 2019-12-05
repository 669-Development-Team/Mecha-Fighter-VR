using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathbyTouch : MonoBehaviour
{
    [SerializeField]
    private RectTransform OpponentHealthBar;
    [SerializeField]
    private RectTransform OpponentHealthPercent;
    [SerializeField]
    private RectTransform PlayerHealthBar;
    [SerializeField]
    private RectTransform PlayerHealthPercent;


    // Only Opponent has a collider
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Opponent")
        {
            OpponentHealthBar.sizeDelta = new Vector2(0f, 100f);
            OpponentHealthPercent.gameObject.GetComponent<TextMesh>().text = "0%";
        }
    }

    // Only Player has a trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealthBar.sizeDelta = new Vector2(0f, 100f);
            PlayerHealthPercent.gameObject.GetComponent<TextMesh>().text = "0%";
        }
    }
}
