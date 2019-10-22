using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxNode : MonoBehaviour
{
	RobotAnimation animation;
	
    // Start is called before the first frame update
    void Start()
    {
        Transform curr = transform;
		while (curr.parent != null)
			curr = curr.parent;
		
		// get RobotAnimation component from the root gameObject
		animation = curr.GetComponent<RobotAnimation>();
    }

    void OnTriggerEnter() {
		
		animation.hitType = Random.Range(1, 4);
		animation.GetHit();
	}
}
