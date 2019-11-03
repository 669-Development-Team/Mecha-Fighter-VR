using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class RobotAnimation : MonoBehaviour
{
    delegate void Action();

    public Animator animator;
    public int hitType;

    public VRIK vrik;

    Dictionary<string, Action> KeyInputMap;
    const float defaultFadeTime = 0.3f;

    void Start()
    {

        this.KeyInputMap = new Dictionary<string, Action>()
        {
            { "Stop Walk", new Action(StopWalk) },
            { "Start Walk F", new Action(StartWalkF) },
            { "Start Walk B", new Action(StartWalkB)},
            { "Start Walk L", new Action(StartWalkL)},
            { "Start Walk R", new Action(StartWalkR)},
            { "Knockdown", new Action(Knockdown) },
            { "Attack", new Action(Attack) },
            { "Action", new Action(ToggleActionIdle) },
            { "GetHit", new Action(GetHit) }
        };
    }

    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            vrik.solver.IKPositionWeight = 0;
        }
        else
        {
            vrik.solver.IKPositionWeight = 1;
        }
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

    void StartWalkF()
    {
        animator.SetTrigger("ExitIdle");
        animator.SetBool("WalkForward", true);
    }

    void StartWalkB()
    {
        animator.SetTrigger("ExitIdle");
        animator.SetBool("WalkBack", true);
    }

    void StartWalkL()
    {
        animator.SetTrigger("ExitIdle");
        animator.SetBool("WalkLeft", true);
    }

    void StartWalkR()
    {
        animator.SetTrigger("ExitIdle");
        animator.SetBool("WalkRight", true);
    }

    void StopWalk()
    {
        animator.SetBool("WalkForward", false);
        animator.SetBool("WalkRight", false);
        animator.SetBool("WalkLeft", false);
        animator.SetBool("WalkBack", false);
    }

    void Knockdown()
    {
        animator.SetTrigger("ExitIdle");
        animator.SetTrigger("Knockdown");
    }

    void Attack()
    {
        vrik.solver.IKPositionWeight = 0;
        animator.SetTrigger("ExitIdle");
        animator.SetTrigger("Attack");
    }

    void ToggleActionIdle()
    {
        vrik.solver.IKPositionWeight = 0;
        animator.SetBool("Action", !animator.GetBool("Action"));
    }
}