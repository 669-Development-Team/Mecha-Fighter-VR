using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseHeartbeat : NetworkResponse
{
    private ConnectionManager connectionManager;
    //The opponent and player's animators
    private static Animator opponentAnim;
    //Opponent references
    private static GameObject opponentRoot;
    private static GameObject opponentCamera;
    private static GameObject opponentLeftController;
    private static GameObject opponentRightController;

    public ResponseHeartbeat()
    {
        connectionManager = GameObject.Find("Main Object").GetComponent<ConnectionManager>();
    }

    override
    public void parse()
    {
    }

    //Get new references to the opponent's IK targets
    public static void init()
    {
        opponentRoot = GameObject.FindGameObjectWithTag("Opponent");
        opponentAnim = opponentRoot.GetComponent<Animator>();
        opponentCamera = GameObject.Find("Head Target");
        opponentLeftController = GameObject.Find("Left Arm Target");
        opponentRightController = GameObject.Find("Right Arm Target");
    }

    private Vector3 readVector3()
    {
        return new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
    }

    override
    public ExtendedEventArgs process()
    {
        //Determines whether the entire packet should be processed or not
        bool dropPacket = false;

        //The number of the incomming update
        int updateNumber = DataReader.ReadShort(dataStream);
        int lastUpdateNumber = GameObject.Find("Main Object").GetComponent<ConnectionManager>().getUpdateNumber();

        if (updateNumber <= lastUpdateNumber && (lastUpdateNumber - updateNumber) <= Constants.maxUpdateDistance)
        {
            dropPacket = true;
            Debug.Log("Packed Dropped. Last Update Number: " + lastUpdateNumber + "\tNew Update Number" + updateNumber);
        }
        else
            connectionManager.setUpdateNumber(updateNumber);

        if (!dropPacket)
        {
            Vector3 headPosition = readVector3();

            //Set the position of the opponent's root
            opponentRoot.transform.position = new Vector3(headPosition.x, 0, headPosition.z);

            //Set the position and angles of the opponent's IK targets.
            opponentCamera.transform.position = headPosition;
            opponentCamera.transform.rotation = Quaternion.Euler(readVector3());
            opponentLeftController.transform.position = readVector3();
            opponentLeftController.transform.rotation = Quaternion.Euler(readVector3());
            opponentRightController.transform.position = readVector3();
            opponentRightController.transform.rotation = Quaternion.Euler(readVector3());

            //Get any triggers and apply them to the opponent
            Constants.animParam trigger = (Constants.animParam)DataReader.ReadInt(dataStream);

            if (trigger != Constants.animParam.None)
                opponentAnim.SetTrigger(trigger.ToString());
        }

        return null;
    }
}