using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnCollision : MonoBehaviour
{

    public GameObject replacement;
    public GameObject Hpbar;
    float objectHealth;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            //Store current visual HP bar into "Playerhealth"
            objectHealth = Hpbar.GetComponent<RectTransform>().rect.width;

            //Set the new visual HP bar's stat the same width minus 200
            //Hpbar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerHealth - 200);

            //GameObject.Instantiate(replacement, transform.position, transform.rotation);
            // Destroy(gameObject);
        }

    }
}
