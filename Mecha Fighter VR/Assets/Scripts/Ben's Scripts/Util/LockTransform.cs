using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour
{
	public bool enablePositionOverride;
	
	[Header("Position value")]
	public Vector3 overridePosition;
	public bool useTransformOverride;
	public Transform overridePositionTransform = null;
	
	[Header("Position axes to lock")]
	public bool x;
	public bool y;
	public bool z;
	
	[Space(20)]
	public bool enableRotationOverride;
	
	[Header("Rotation value")]
	public Vector3 overrideRotation;
	public bool _useTransformOverride;
	public Transform overrideRotationTransform = null;
	
	[Header("Rotation axes to lock")]
	public bool _x;
	public bool _y;
	public bool _z;

    void LateUpdate()
    {
		if (useTransformOverride) overridePosition = overridePositionTransform.position;
		if (_useTransformOverride) overrideRotation = overrideRotationTransform.rotation.eulerAngles;
		
		if (enablePositionOverride) {
			var pos = transform.position;
			
			pos.x = x ? overridePosition.x : pos.x;
			pos.y = y ? overridePosition.y : pos.y;
			pos.z = z ? overridePosition.z : pos.z;
			
			transform.position = pos;
		}
		
		if (enableRotationOverride) {
			
			var rot = transform.rotation.eulerAngles;
			
			rot.x = _x ? overrideRotation.x : rot.x;
			rot.y = _y ? overrideRotation.y : rot.y;
			rot.z = _z ? overrideRotation.z : rot.z;
			
			transform.rotation = Quaternion.Euler(rot);
		}
    }
}
