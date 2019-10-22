using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    public GameObject Hit;


    public GameObject AttachBurning;
    private float velocity = 0f;
    Vector3 previousframe;

    private void Start()
    {
        previousframe = transform.position;
    }

    private void Update()
    {
        float movementPerFrame = Vector3.Distance(previousframe, transform.position);
        velocity = movementPerFrame / Time.deltaTime;
        previousframe = transform.position;
        Debug.Log("velocity = " + velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject temp;
       

        //Since I can't modify components within the particle component I decided to modify the local scale of each individually

        Hit.transform.localScale = new Vector3(velocity/10f, velocity / 10f, velocity / 10f);


        temp = Instantiate(Hit, transform.position, transform.rotation);


        Destroy(temp,2f);

        /*
        if(collision.gameObject.tag == "Burnable")
        {
            GameObject burnable;
            burnable = Instantiate(AttachBurning, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            burnable.transform.SetParent(collision.transform);
            Destroy(burnable, 3f);
        }
        */
    }
}
