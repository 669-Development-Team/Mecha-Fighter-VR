using UnityEngine;
using System;

public class RequestPushUpdate : NetworkRequest
{
    //A reference to the IK targets of the player
    private GameObject playerCamera;
    private GameObject playerLeftController;
    private GameObject playerRightController;

    public RequestPushUpdate()
    {
        request_id = Constants.CMSG_PUSHUPDATE;
    }

    //Get new references to the player's IK targets
    public void init()
    {
        playerCamera = GameObject.Find("Camera");
        playerLeftController = GameObject.Find("Controller (left)");
        playerRightController = GameObject.Find("Controller (right)");
    }

    //Add the position of the given object to the packet
    private void addPosition(GamePacket packet, GameObject gameObject)
    {
        packet.addFloat32(gameObject.transform.position.x);
        packet.addFloat32(gameObject.transform.position.y);
        packet.addFloat32(gameObject.transform.position.z);
    }

    //Add the rotation of the given object to the packet
    private void addRotation(GamePacket packet, GameObject gameObject)
    {
        packet.addFloat32(gameObject.transform.rotation.eulerAngles.x);
        packet.addFloat32(gameObject.transform.rotation.eulerAngles.y);
        packet.addFloat32(gameObject.transform.rotation.eulerAngles.z);
    }

    public void send()
    {
        packet = new GamePacket(request_id);

        //Add the positions and rotations of all the IK targets
        addPosition(packet, playerCamera);
        addRotation(packet, playerCamera);
        addPosition(packet, playerLeftController);
        addRotation(packet, playerLeftController);
        addPosition(packet, playerRightController);
        addRotation(packet, playerRightController);

        ////Get the animation controller
        //Animator animator = player.GetComponent<Animator>();

        ////Add the speed of the animation
        //packet.addFloat32(animator.GetFloat("Speed"));

        ////Add the parameters for the specific character
        //foreach (string param in Constants.animParams)
        //    packet.addBool(animator.GetBool(param));

        return;
    }
}