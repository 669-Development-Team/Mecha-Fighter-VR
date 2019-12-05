using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseHit : NetworkResponse
{
    private int damage;

    override
    public void parse()
    {
        damage = DataReader.ReadShort(dataStream);
    }

    override
    public ExtendedEventArgs process()
    {
        Debug.Log("Got hit for " + damage);

        return null;
    }
}
