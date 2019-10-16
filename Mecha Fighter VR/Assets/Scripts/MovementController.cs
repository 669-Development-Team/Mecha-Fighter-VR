using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MovementController : MonoBehaviour
{
	public Transform camera;
	public float vertical;
	public float _speed = 0.04f;
	public float multiplier = 1f;
	public bool controllable = true;

	public SteamVR_Action_Vector2 Joystick;
	public SteamVR_Action_Boolean Trigger;
	public SteamVR_ActionSet steamVR_ActionSet;

	private RobotAnimation animationController;


	float speed { get { return _speed * multiplier; } }

	Animator animator;
	Transform root;
	
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
		root = transform.Find("Root");
		
		steamVR_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
		animationController = GetComponent<RobotAnimation>();
    }
	
	Vector3 currentSlope = Vector3.up;

    // Update is called once per frame
    void Update()
    {
		
		RaycastHit hit;
		int ignoreLayers = ~(1 << 9);
		
		if (Physics.Raycast(transform.position + 0.3f * Vector3.up, Vector3.down, out hit, 100, ignoreLayers)) {
			transform.position = hit.point;
			currentSlope = hit.normal;
		}
		
		var moveRotation = Quaternion.FromToRotation(Vector3.up, currentSlope) * onlyYRotation(camera.rotation);

		if (controllable && !Trigger.GetState(SteamVR_Input_Sources.Any) ) {
			animator.SetBool("Action", false);
			float horizontal = Joystick.GetAxis(SteamVR_Input_Sources.Any)[0];
			float vertical = Joystick.GetAxis(SteamVR_Input_Sources.Any)[1];
			this.vertical = vertical;
			
			var forward = moveRotation * Vector3.forward;
			var right = moveRotation * Vector3.right;
			
			transform.position += (vertical * speed * forward);
			transform.position += (horizontal * speed * right);
			
			
				if (vertical < 0f) {
					animationController.HandleInput("Start Walk");
					//DisableWalkCycles("WalkBack");
				}
				else if (vertical > 0f) {
					animationController.HandleInput("Start Walk");
					//animator.SetBool("WalkForward", true);
					//DisableWalkCycles("WalkForward");
				}
				else if (horizontal < 0f) {
					animationController.HandleInput("Start Walk");
					//animator.SetBool("WalkLeft", true);
					//DisableWalkCycles("WalkLeft");
				}
				else if (horizontal > 0f) {
					animationController.HandleInput("Start Walk");
					//animator.SetBool("WalkRight", true);
					//DisableWalkCycles("WalkRight");
				}
				else {
					animationController.HandleInput("Stop Walk");
					DisableWalkCycles();
				}
		}
		else{
			animator.SetBool("Action", true);
		}
    }
	
	void DisableWalkCycles(string exception = "")
	{
		// if (exception != "WalkForward") animator.SetBool("WalkForward", false);
		// if (exception != "WalkBack") animator.SetBool("WalkBack", false);
		// if (exception != "WalkRight") animator.SetBool("WalkRight", false);
		// if (exception != "WalkLeft") animator.SetBool("WalkLeft", false);
	}
	
	Quaternion onlyYRotation(Quaternion rot)
	{
		var euler = rot.eulerAngles;
		euler.x = euler.z = 0;
		return Quaternion.Euler(euler);
	}
}
