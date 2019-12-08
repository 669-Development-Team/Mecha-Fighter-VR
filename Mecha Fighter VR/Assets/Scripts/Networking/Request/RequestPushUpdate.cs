using UnityEngine;
using System;

public class RequestPushUpdate : NetworkRequest
{
    //A reference to the IK targets of the player
    private static GameObject playerCamera;
    private static GameObject playerLeftController;
    private static GameObject playerRightController;
    //Temporarily store  trigger before it is sent
    private static Constants.animParam trigger = Constants.animParam.None;

    public RequestPushUpdate()
    {
        request_id = Constants.CMSG_PUSHUPDATE;
    }

    //Get new references to the player's IK targets
    public static void init()
    {
        playerCamera = GameObject.Find("Head Target for Mech IK");
        playerLeftController = GameObject.Find("Left Hand Target for Mech IK");
        playerRightController = GameObject.Find("Right Hand Target for Mech IK");
    }

    //Store a trigger to be sent
    public static void setTrigger(Constants.animParam newTrigger)
    {
        trigger = newTrigger;
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

        //Send the trigger if there is one
        packet.addInt32((int)trigger);
        //Reset the trigger
        trigger = Constants.animParam.None;

        return;
    }
}