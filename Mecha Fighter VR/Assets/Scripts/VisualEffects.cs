using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

public class VisualEffects : MonoBehaviour
{
    public GameObject Hit;


    public GameObject AttachBurning;
    private float velocity = 0f;
    Vector3 previousframe;
    public GameObject Trail;
    public float TrailAppearsWhenGreaterThan;
    

    private void Start()
    {
        previousframe = transform.position;
    }

    private void Update()
    {
        //Keeps track of how fast you're swinging with the hand controllers
        float movementPerFrame = Vector3.Distance(previousframe, transform.position);
        velocity = movementPerFrame / Time.deltaTime;
        previousframe = transform.position;



        //Make trails appear whenever you swing fast with the controller && not moving the player with joystick
        if (velocity > TrailAppearsWhenGreaterThan && PlayerController.getMovementValue == new Vector3(0,0,0))
        {
            Trail.SetActive(true);
        }
        if(velocity < TrailAppearsWhenGreaterThan && PlayerController.getMovementValue != new Vector3(0, 0, 0))
        {
            Trail.SetActive(false);
        }

        Debug.Log("velocity = " + velocity);
        Debug.Log("movement value " + PlayerController.getMovementValue);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Opponent")
        {
            GameObject temp;
            Hit.transform.localScale = new Vector3(velocity / 10f, velocity / 10f, velocity / 10f);
            temp = Instantiate(Hit, transform.position, transform.rotation);
            Destroy(temp, 2f);
        }
        

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
