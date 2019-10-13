using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour

{
    public GameObject gameObject;
    public Material[] materials;
    Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = materials[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision cul)
    {
        RobotAnimation controller = gameObject.GetComponent<RobotAnimation>();
        controller.HandleInput("down a");
        renderer.sharedMaterial = materials[1];

    }
    void OnCollisionExit(Collision cul)
    {
        renderer.sharedMaterial = materials[0];

    }

}
