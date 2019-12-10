using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseHeartbeat : NetworkResponse
{
    private ConnectionManager connectionManager;
    [SerializeField] GameObject opponent;
    private static NetworkPlayerController networkController;

    public ResponseHeartbeat()
    {
        connectionManager = GameObject.Find("Main Object").GetComponent<ConnectionManager>();
    }

    public static void init()
    {
        networkController = GameObject.FindGameObjectWithTag("Opponent").GetComponent<NetworkPlayerController>();
    }

    override
    public void parse()
    {
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
            //Read all of the positions and rotations
            Vector3 cameraPos = readVector3();
            Vector3 cameraRot = readVector3();
            Vector3 lHandPos = readVector3();
            Vector3 lHandRot = readVector3();
            Vector3 rHandPos = readVector3();
            Vector3 rHandRot = readVector3();

            //Update the root and IK targets
            networkController.updatePositions(cameraPos, lHandPos, rHandPos);
            networkController.updateRotations(cameraRot, lHandRot, rHandRot);

            //Get any triggers and apply them to the opponent
            Constants.animParam trigger = (Constants.animParam)DataReader.ReadInt(dataStream);

            if (trigger != Constants.animParam.None)
                networkController.setAnimation(trigger);
        }

        return null;
    }
}