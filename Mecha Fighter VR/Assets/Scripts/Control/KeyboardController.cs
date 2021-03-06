﻿using System.Collections;
using System.Collections.Generic;
using Action;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardController : MonoBehaviour
{
    private MovementHandler playerController;
    [SerializeField] private PlayerStats opponent = null;
    private bool leftShieldActive = false;
    private bool rightShieldActive = false;

    private Vector3 displacement = Vector3.zero;

    void Start()
    {
        playerController = GetComponent<MovementHandler>();
    }

    void Update()
    {
        updatePosition();
        checkAbilities();
    }

    private void updatePosition()
    {
        displacement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        displacement = displacement.x * transform.right + displacement.z * transform.forward;
        displacement.Normalize();

        playerController.DoMovement(displacement);

        // Always look at opponent
        if (opponent != null)
        {
            // Only rotate y-axis
            Vector3 lookDirection = new Vector3(opponent.transform.position.x, transform.position.y, opponent.transform.position.z);
            transform.LookAt(lookDirection);
        }
    }

    private void checkAbilities()
    {
        string abilityToActivate = "";

        if (Input.GetKeyDown("p"))
            abilityToActivate = "Projectile";
        if (Input.GetKeyDown("g"))
            abilityToActivate = "GroundPound";
        if (Input.GetKeyDown("u"))
            abilityToActivate = "Uppercut";

        AnimatedGestureAbility[] guestureScripts = GetComponents<AnimatedGestureAbility>();

        foreach(AnimatedGestureAbility script in guestureScripts)
        {
            if (script.getAnimName() == abilityToActivate)
                script.ActivateAbility();
        }
    }
}
