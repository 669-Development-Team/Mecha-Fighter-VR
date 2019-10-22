using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class RobotAnimation : MonoBehaviour
{
	delegate void Action();
	
	Animator animator;
	
	public int hitType;
	public bool gotHit;
	
	IKSolverVR fullBodyIK;
	Dictionary<string, Action> KeyInputMap;
	const float defaultFadeTime = 0.3f;
	
	void Start()
	{
		animator = GetComponent<Animator>();
		fullBodyIK = GetComponent<VRIK>().GetIKSolver() as IKSolverVR;
		
		this.KeyInputMap = new Dictionary<string, Action> () 
		{
			{ "up w", 	new Action(StopWalk) },
			{ "down w", new Action(StartWalk) },
			{ "down f", new Action(Knockdown) },
			{ "down a", new Action(Attack) },
			{ "down s", new Action(ToggleActionIdle) },
			{ "down g", new Action(GetHit) }
		};
		
		gotHit = false;
	}
	
	void LateUpdate()
	{
		float IKWeight = animator.GetFloat("IKWeight");
		fullBodyIK.SetIKPositionWeight(IKWeight);
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
	
	public void GetHit()
	{
		if (!gotHit) {
			animator.SetInteger("HitType", hitType);
			animator.SetTrigger("ExitIdle");
			animator.SetTrigger("GetHit");
			gotHit = true;
			StartCoroutine(waitForEndOfGetHit());
		}
	}
	
	private IEnumerator waitForEndOfGetHit() {
		yield return new WaitForSeconds(0.5f);
		gotHit = false;
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