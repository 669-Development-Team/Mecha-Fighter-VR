using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    public GameObject Hit;
    public GameObject AttachBurning;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject temp;

        temp = Instantiate(Hit, transform.position, transform.rotation);

        Destroy(temp,2f);
        if(collision.gameObject.tag == "Burnable")
        {
            GameObject burnable;
            burnable = Instantiate(AttachBurning, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            burnable.transform.SetParent(collision.transform);
            Destroy(burnable, 3f);
        }
    }
}
