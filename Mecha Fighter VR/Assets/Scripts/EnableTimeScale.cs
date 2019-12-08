using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTimeScale : MonoBehaviour
{
    void Start()
    {
        //Ensure that time is enabled
        Time.timeScale = 1;
    }
}
