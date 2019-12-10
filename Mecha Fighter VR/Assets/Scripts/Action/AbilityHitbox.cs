using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an AbilityHitbox class should be on every ability prefab
public abstract class AbilityHitbox : MonoBehaviour
{
	[SerializeField] private float lifetime;
	
	void Start() {
		Destroy(gameObject, lifetime);
	}
	
    public abstract bool Apply(PlayerStats other);
    public abstract bool ApplyShield(Shield other);
}
