 using UnityEngine;
 using System.Collections;
 
 public class FaceFighter : MonoBehaviour {
 
     public GameObject fighter;          // Reference to the first GameObject

     // Use this for initialization
     void Start () {
 
     }
     
     // Update is called once per frame
     void Update () {
		transform.LookAt(fighter.transform.position);
	}
 }