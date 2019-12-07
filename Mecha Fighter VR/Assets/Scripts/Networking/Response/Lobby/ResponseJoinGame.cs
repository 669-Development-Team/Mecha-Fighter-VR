using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseJoinGameEventArgs : ExtendedEventArgs
{
    public string character { get; set; }

    public ResponseJoinGameEventArgs()
    {
        event_id = Constants.SMSG_JOINGAME;
    }
}

public class ResponseJoinGame : NetworkResponse
{
    override
    public void parse()
    {  
    }

    override
    public ExtendedEventArgs process()
    {
        ResponseJoinGameEventArgs args = new ResponseJoinGameEventArgs();
        return args;
    }
}