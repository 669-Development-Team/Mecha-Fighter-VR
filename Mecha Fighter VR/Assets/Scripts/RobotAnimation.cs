using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class RobotAnimation : MonoBehaviour
{
	delegate void Action();
	
	public Animator animator;
	public ArmIK leftArm, rightArm;
	public float IKWeight { get; set; }
	public int hitType;
	
	IKSolverArm solverLeft, solverRight;
	Dictionary<string, Action> KeyInputMap;
	const float defaultFadeTime = 0.3f;
	
	void Start()
	{
		solverLeft = leftArm.GetIKSolver() as IKSolverArm;
		solverRight = rightArm.GetIKSolver() as IKSolverArm;
		IKWeight = solverLeft.GetIKPositionWeight();
		
		this.KeyInputMap = new Dictionary<string, Action> () 
		{
			{ "up w", 	new Action(StopWalk) },
			{ "down w", new Action(StartWalk) },
			{ "down f", new Action(Knockdown) },
			{ "down a", new Action(Attack) },
			{ "down s", new Action(ToggleActionIdle) },
			{ "down g", new Action(GetHit) }
		};
	}
	
	void LateUpdate()
	{
		float IKWeight = animator.GetFloat("IKWeight");
		solverLeft.SetIKPositionWeight(IKWeight);
		solverLeft.IKRotationWeight = IKWeight;
		solverRight.SetIKPositionWeight(IKWeight);
		solverRight.IKRotationWeight = IKWeight;
	}
	
	/******************/
	/* PUBLIC METHODS *
	/******************/
	
	public void HandleInput(string inputName)
	{
		Action animationAction = KeyInputMap[inputName];
		animationAction();
	}
	
	/*********************************/
	/* ANIMATION STATE CHANGE */
	/*********************************/
	
	void GetHit()
	{
		animator.SetInteger("HitType", hitType);
		animator.SetTrigger("ExitIdle");
		animator.SetTrigger("GetHit");
	}
	
	void StartWalk()
	{
		animator.SetBool("Walking", true);
		animator.SetTrigger("ExitIdle");
	}
	
	void StopWalk()
	{
		animator.SetBool("Walking", false);
	}
	
	void Knockdown()
	{
		animator.SetTrigger("ExitIdle");
		animator.SetTrigger("Knockdown");
	}
	
	void Attack()
	{
		animator.SetTrigger("ExitIdle");
		animator.SetTrigger("Attack");
	}
	
	void ToggleActionIdle()
	{
		animator.SetBool("Action", !animator.GetBool("Action"));
	}
}