using System.Collections;
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
        displacement.Normalize();
        // Multiplying by Time.deltaTime is already done in MovementHandler.cs
        // Movement speed is handled in MovementHandler.cs

//        if (Input.GetKey("w"))
//            displacement.z += 1;
//        if (Input.GetKey("s"))
//            displacement.z -= 1;
//        if (Input.GetKey("a"))
//            displacement.x -= 1;
//        if (Input.GetKey("d"))
//            displacement.x += 1;

//        displacement = displacement.x * transform.right + displacement.z * transform.forward;
//        displacement *= Time.deltaTime * movementSpeed;

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
