using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMove : MonoBehaviour
{
    public Vector3 moveAmount;

    bool toggled = false;
    Vector3 start = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.localPosition;   
    }

    // Update is called once per frame
    void Update()
    {
        if (toggled) {
            transform.localPosition = start + moveAmount;
        }
        else {
            transform.localPosition = start;
        }
    }

    public void setToggle(bool val) {
        toggled = val;
    }
}
