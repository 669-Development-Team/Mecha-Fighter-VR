using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, Stats.IDamageable
{
    [SerializeField] private int maxHealth;
	[SerializeField] private float healthRegen;
	[SerializeField] private int maxEnergy;
	[SerializeField] private float energyRegen;
	[SerializeField] private int baseDamage;

	[SerializeField] private AudioSource audioSource = null;
	[SerializeField] private AudioClip[] hitImpactSfx;
	[SerializeField] private GameObject replenishEffect = null;
	[Tooltip("Transform of the mech so that the effect spawns at the mech's feet")]
	[SerializeField] private Transform mechRoot = null;

	[SerializeField] private UnityEvent onTakeDamage = null;
	[SerializeField] private UnityEvent onReplenish = null;

	private float currentHealth;
	private float currentEnergy;
	private float hitStunTimer;

	void Start() {
		currentHealth = maxHealth;
		currentEnergy = maxEnergy;
	}

	void Update() {
		if (currentHealth <= 0 && !GameStateManager.instance.gameOver) {
			GameStateManager.instance.GameOver(gameObject.tag);
		}

		currentHealth = Mathf.Clamp(currentHealth + healthRegen, 0, maxHealth);
		currentEnergy = Mathf.Clamp(currentEnergy + energyRegen, 0, maxEnergy);
		hitStunTimer  = Mathf.Clamp(hitStunTimer - Time.deltaTime, 0, 10);
	}

	public void TakeDamage(float damageDealt) {
		currentHealth -= Mathf.Max(1, damageDealt);
		PlaySfx();
		if(damageDealt >= 10f){
			gameObject.GetComponent<FightReactions>().ProjectileHit();
		}
		if (currentHealth <= 0f)
		{
			FindObjectOfType<GameStateManager>().GameOver(transform.root.gameObject.name);
		}

		onTakeDamage.Invoke();
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

	public void Heal(float healthToRestore)
	{
		if (currentHealth < maxHealth)
		{
			currentHealth = Mathf.Min(currentHealth + healthToRestore, maxHealth);
		}
	}

	public void Replenish(float energyToRestore)
	{
		if (currentEnergy < maxEnergy)
		{
			currentEnergy = Mathf.Min(currentEnergy + energyToRestore, maxEnergy);
		}

		GameObject effect = Instantiate(replenishEffect, mechRoot);
		Destroy(effect, 1f);
		onReplenish.Invoke();
	}

	public void PlaySfx()
	{
		audioSource.PlayOneShot(hitImpactSfx[Random.Range(0, hitImpactSfx.Length - 1)]);
		audioSource.Play();
	}

}
