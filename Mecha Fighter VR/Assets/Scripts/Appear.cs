using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Appear : MonoBehaviour { 
	public SteamVR_Action_Boolean SphereOnOff;
	public SteamVR_ActionSet steamVR_ActionSet;

public SteamVR_Input_Sources handType;

public GameObject Sphere;

    // Start is called before the first frame update

    void Start()
    {
		steamVR_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
	}


	void Update()
	{
		if (!SphereOnOff.GetState(SteamVR_Input_Sources.Any))
		{
			Sphere.transform.localScale = new Vector3(0, 0, 0);

		}
		else
		{
			Sphere.transform.localScale = new Vector3(.5f, .5f, .1f);
		}
	}
}

