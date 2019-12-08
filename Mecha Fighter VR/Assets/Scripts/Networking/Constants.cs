using System.Collections.Generic;
using UnityEngine;

public class Constants
{

    //Constants
    public static readonly string CLIENT_VERSION = "1.00";
    public static readonly string REMOTE_HOST = "54.185.56.184";
    //public static readonly string REMOTE_HOST = "localhost";
    public static readonly int REMOTE_PORT = 9252;
    public static readonly int updatesPerSecond = 60;
    public static readonly int maxUpdateNumber = 10000;
    public static readonly int maxUpdateDistance = 1000;

    //Net code
    //Request: 1xx
    //Response: 2xx

    //General APIs:    x0x
    public static readonly short CMSG_HEARTBEAT = 101;
    public static readonly short SMSG_HEARTBEAT = 201;
    public static readonly short CMSG_PUSHUPDATE = 102;
    public static readonly short CMSG_KEEPALIVE = 103;

    //Authentication:  x1x
    public static readonly short CMSG_REGISTER = 111;
    public static readonly short SMSG_REGISTER = 211;
    public static readonly short CMSG_LOGIN = 112;
    public static readonly short SMSG_LOGIN = 212;

    //Lobby APIs:      x2x
    public static readonly short CMSG_STARTGAME = 121;
    public static readonly short SMSG_STARTGAME = 221;
    public static readonly short CMSG_JOINGAME = 122;
    public static readonly short SMSG_JOINGAME = 222;
    public static readonly short CMSG_ENDGAME = 223;
    public static readonly short SMSG_ENDGAME = 123;


    //Actions
    //Pickups:  x4x
    public static readonly short CMSG_PICKUP = 140;
    public static readonly short SMSG_PICKUP = 240;

    //Hit: x5x
    public static readonly short CMSG_HIT = 150;
    public static readonly short SMSG_HIT = 250;

    //GUI Window IDs
    public enum GUI_ID
    {
        Login
    };

    public static int USER_ID = -1;

    //Animation Parameters
    public static readonly string[] animParams = { "isJumping", "isWalking", "isShooting", "isRunning", "isForward", "isBackward", "isLeft", "isRight" };
}