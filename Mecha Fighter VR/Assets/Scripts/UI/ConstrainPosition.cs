using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainPosition : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Vector3 offset;

    private void Start()
    {
        offset = this.gameObject.transform.position - target.transform.position;
    }

    void Update()
    {
        this.gameObject.transform.position = target.transform.position + offset;
    }
}
