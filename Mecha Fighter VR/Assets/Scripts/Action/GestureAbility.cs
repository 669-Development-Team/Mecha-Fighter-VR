using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GestureAbility : TriggeredAbility
{
    [SerializeField] private Gesture _leftGesture;
	[SerializeField] private Gesture _rightGesture;
	
	public Gesture leftGesture { get { return _leftGesture; } }
	public Gesture rightGesture { get { return _rightGesture; } }
}
