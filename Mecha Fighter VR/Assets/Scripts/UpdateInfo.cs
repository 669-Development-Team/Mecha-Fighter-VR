using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInfo : MonoBehaviour
{
	public RobotAnimation robot;
	Text text;
	public bool displayOverride;
	
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!displayOverride)
			text.text = "IK Weight: " + string.Format("{0:0.00}", robot.IKWeight);
		//else
			//text.text = "Override: " + robot.overrideIK + (robot.overrideIK ? ", Value: " + robot.overrideValue : "");
    }
}
