using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class SetIKWeight : MonoBehaviour
{
    Animator animator;

    public VRIK vrik;

    private IKSolverVR solverVR;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        solverVR = (IKSolverVR)vrik.GetIKSolver();
    }

    // Update is called once per frame
    void Update()
    {
        solverVR.SetIKPositionWeight(animator.GetFloat("IKWeight"));

    }

    void ApplyRootMotion(int value)
    {
        animator.applyRootMotion = value > 0 ? true : false;
    }
}
