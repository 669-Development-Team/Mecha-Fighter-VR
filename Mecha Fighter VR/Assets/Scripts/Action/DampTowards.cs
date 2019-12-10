using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampTowards : MonoBehaviour
{
    public Transform target;
    public float speed;
    Vector3 currentVelocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, 0.2f);   
    }
}
