using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class ArmStretcher : MonoBehaviour
{
	public Transform target, root;
	public float Wingspan;
	public bool isLeft;
	public ElectricityVFXNode[] VFXnodes;
	
	IKSolverVR.Arm arm;
	IKSolverArm solver;
	
	void Start()
	{
		ArmIK[] IKComponents = root.GetComponents<ArmIK>();
		foreach (ArmIK IK in IKComponents) {
			solver = IK.GetIKSolver() as IKSolverArm;
			if (solver.isLeft == isLeft) {
				arm = solver.arm;
			}
		}
	}
	
	void Update()
	{
		if (arm == null || target == null) return;
		
		float distance = Vector3.Distance(transform.position, target.position) * solver.GetIKPositionWeight();
		if (distance > Wingspan) {
			arm.armLengthMlp = distance / Wingspan;
			foreach (ElectricityVFXNode VFXNode in VFXnodes) {
				VFXNode.enabled = true;
			}
		}
		else {
			arm.armLengthMlp = 1;
			foreach (ElectricityVFXNode VFXNode in VFXnodes) {
				VFXNode.enabled = false;
			}
		}
	}
}
