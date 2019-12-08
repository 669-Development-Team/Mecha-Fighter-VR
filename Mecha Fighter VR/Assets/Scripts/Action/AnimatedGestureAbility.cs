using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedGestureAbility : GestureAbility
{
	[Tooltip("Mandatory field: How much energy this ability uses")]
	[SerializeField] private int energyCost;
	[Tooltip("Mandatory field: The name of the animation state & trigger associated with this ability. Name of animation trigger and of the state it transitions to are assumed to be the same")]
	[SerializeField] private string animStateName;
	[Tooltip("Optional field: Prefab that is instantiated when the InstantiateFX(state) animation callback is triggered")]
	[SerializeField] private GameObject abilityPrefab;
	[Tooltip("Optional field: Transform that ability prefab is instantiated at")]
	[SerializeField] private Transform spawnPoint;
	[Tooltip("Optional field: Sound effect that is played on ability activation")]
	[SerializeField] private AudioClip activationSFX;
	[Tooltip("Optional field: Sound effect that is played upon ability prefab instantiation")]
	[SerializeField] private AudioClip instantiationSFX;

	private PlayerStats stats;
	private AudioSource audioSource;
	private Animator animator;
	private AnimationListener animListener;
	private bool isAnimating;

	void Start() {
		stats = GetComponent<PlayerStats>();
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		animListener = new AnimationListener(this, animStateName, null, OnAnimationExit);
		isAnimating = false;
	}

    public string getAnimName()
    {
        return animStateName;
    }

	public override void ActivateAbility() { if (isAnimating) return;

		Debug.Log(animStateName + " ability was activated!");

        //Send the trigger to the opponent over the net
        Constants.animParam trigger = (Constants.animParam)System.Enum.Parse(typeof(Constants.animParam), animStateName);
        RequestPushUpdate.setTrigger(trigger);

		if (stats.DepleteEnergy(energyCost)) {
			if (activationSFX != null) 	audioSource.PlayOneShot(activationSFX);
			animator.SetTrigger(animStateName);
			isAnimating = true;
		}
	}

	void InstantiateFX(string stateName) { if (stateName != animStateName) return;

		if (abilityPrefab != null && spawnPoint != null) 	{
			var prefab = Instantiate(abilityPrefab, spawnPoint.position, spawnPoint.rotation);
			// set the prefab layer according to which player this is
//			prefab.layer = gameObject.layer == 12 ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("OpponentProjectile");
			prefab.layer = gameObject.layer;

			if (instantiationSFX != null) audioSource.PlayOneShot(instantiationSFX);
		}
	}

	private void OnAnimationExit() {
		isAnimating = false;
	}
}
