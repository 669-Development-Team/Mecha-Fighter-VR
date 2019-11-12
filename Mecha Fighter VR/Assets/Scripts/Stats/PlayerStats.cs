using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, Stats.IDamageable
{
    [SerializeField] private int maxHealth;
	[SerializeField] private float healthRegen;
	[SerializeField] private int maxEnergy;
	[SerializeField] private float energyRegen;
	[SerializeField] private int baseDamage;
	
	private float currentHealth;
	private float currentEnergy;
	private float hitStunTimer;
	
	void Start() {
		currentHealth = maxHealth;
		currentEnergy = maxEnergy;
	}
	
	void Update() {
		currentHealth = Mathf.Clamp(currentHealth + healthRegen, 0, maxHealth);
		currentEnergy = Mathf.Clamp(currentEnergy + energyRegen, 0, maxEnergy);
		hitStunTimer  = Mathf.Clamp(hitStunTimer - Time.deltaTime, 0, 10);
	}
	
	public void TakeDamage(float damageDealt) {
		currentHealth -= damageDealt;
	}
	
	public bool DepleteEnergy(float energyCost) {
		currentEnergy -= energyCost;
		return currentEnergy >= 0;
	}
	
	public float GetHealthPercentage() {
		return currentHealth / maxHealth * 100;
	}
	
	public float GetEnergyPercentage() {
		return currentEnergy / maxEnergy * 100;
	}
	
	public void TriggerHitStun(float stunTime) {
		hitStunTimer = stunTime;
	}
	
	public bool InHitStun() {
		return hitStunTimer > 0;
	}
}
