using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : AbilityHitbox
{
	[SerializeField] private float damage;
	[SerializeField] private float hitStun;
	
    public override bool Apply(PlayerStats other) {
		if (other.InHitStun()) return false;
		other.TakeDamage(damage);
		other.TriggerHitStun(hitStun);
		return true;
	}
}
