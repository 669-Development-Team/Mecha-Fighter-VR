using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class ArmStretcher : MonoBehaviour
{
	public Transform leftTarget, rightTarget;
	public float Wingspan;
	
	public GameObject nodePrefab;
	
	public List<ElectricityVFXNode> vfxNodesLeft, vfxNodesRight;
	
	IKSolverVR.Arm leftArm, rightArm;
	IKSolverArm leftSolver, rightSolver;
	Transform leftShoulder, rightShoulder;
	
	[ContextMenu("Add Electricity FX Nodes to the bones of the model")]
	void BuildVFXNodes() {
		
		Animator anim = GetComponent<Animator>();
		
		var vfxTransformsLeft = new Transform[4];
		vfxTransformsLeft[0] = anim.GetBoneTransform(HumanBodyBones.LeftShoulder);
		vfxTransformsLeft[1] = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
		vfxTransformsLeft[2] = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
		vfxTransformsLeft[3] = anim.GetBoneTransform(HumanBodyBones.LeftHand);
		
		var vfxTransformsRight = new Transform[4];
		vfxTransformsRight[0] = anim.GetBoneTransform(HumanBodyBones.RightShoulder);
		vfxTransformsRight[1] = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
		vfxTransformsRight[2] = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);
		vfxTransformsRight[3] = anim.GetBoneTransform(HumanBodyBones.RightHand);
		
		_BuildNodesFromList(vfxTransformsLeft, ref vfxNodesLeft);
		_BuildNodesFromList(vfxTransformsRight, ref vfxNodesRight);
	}
	void _BuildNodesFromList(Transform[] nodeTransforms, ref List<ElectricityVFXNode> nodes) {
		
		nodes = new List<ElectricityVFXNode>();
		
		for (int i = 0; i < nodeTransforms.Length - 1; i++) {
				
			var curr = nodeTransforms[i];
			var next = nodeTransforms[i+1];
			
			string nodeName = "VFXNode";
			
			if (!curr.Find(nodeName)) {
			
				GameObject curr_end = Instantiate(nodePrefab, curr);
				curr_end.name = nodeName;
				
				curr_end.transform.position = next.position;
				
				ElectricityVFXNode node = curr_end.GetComponent<ElectricityVFXNode>();
				node.endPoint = next;
				
				nodes.Add(node);
			}
			else {
				
				Transform existingNode = curr.Find(nodeName);
				
				nodes.Add(existingNode.GetComponent<ElectricityVFXNode>());
			}
		}
	}
	
	void Start()
	{
		ArmIK[] IKComponents = GetComponents<ArmIK>();
		leftShoulder = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftShoulder);
		rightShoulder = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightShoulder);
		
		foreach (ArmIK IK in IKComponents) {
			
			var solver = IK.GetIKSolver() as IKSolverArm;
			
			if (solver.isLeft) {
				
				leftSolver = solver;
				leftArm = solver.arm;
			}
			else {
				
				rightSolver = solver;
				rightArm = solver.arm;
			}
		}
	}
	
	void Update()
	{
		UpdateArm(leftArm, leftSolver, leftShoulder, leftTarget, vfxNodesLeft);
		UpdateArm(rightArm, rightSolver, rightShoulder, rightTarget, vfxNodesRight);
	}
	
	void UpdateArm(IKSolverVR.Arm arm, IKSolverArm solver, Transform shoulder, Transform target, List<ElectricityVFXNode> VFXnodes) {
		
		if (arm == null || target == null) return;
		
		float distance = Vector3.Distance(shoulder.position, target.position) * solver.GetIKPositionWeight();
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
