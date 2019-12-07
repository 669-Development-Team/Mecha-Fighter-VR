using System.Collections;
using System.Collections.Generic;
using Action;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardController : MonoBehaviour
{
    private MovementHandler playerController;
    [SerializeField]
    private float movementSpeed;
    private bool leftShieldActive = false;
    private bool rightShieldActive = false;

    [SerializeField] private UnityEvent projectileButtonDown = null;
    [SerializeField] private UnityEvent groundPoundButtonDown = null;
    [SerializeField] private UnityEvent uppercutButtonDown = null;

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
        Vector3 displacement = new Vector3(0, 0, 0);

        if (Input.GetKey("w"))
            displacement.z += 1;
        if (Input.GetKey("s"))
            displacement.z -= 1;
        if (Input.GetKey("a"))
            displacement.x -= 1;
        if (Input.GetKey("d"))
            displacement.x += 1;

        displacement = Time.deltaTime * movementSpeed * displacement;

        playerController.DoMovement(displacement);
    }

    private void checkAbilities()
    {
//        if (Input.GetKeyDown("l"))
//        {
//            if (!leftShieldActive)
//                playerController.enableLeftShield();
//            else
//                playerController.disableLeftShield();
//
//            leftShieldActive = !leftShieldActive;
//        }
//
//        if (Input.GetKeyDown("r"))
//        {
//            if (!rightShieldActive)
//                playerController.enableRightShield();
//            else
//                playerController.disableRightShield();
//
//            rightShieldActive = !rightShieldActive;
//        }
//
        if (Input.GetKeyDown("p"))
            projectileButtonDown.Invoke();

        if (Input.GetKeyDown("g"))
            groundPoundButtonDown.Invoke();

        if (Input.GetKeyDown("u"))
            uppercutButtonDown.Invoke();
    }
}
