using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    private Action.MovementHandler playerController;
    [SerializeField]
    private float movementSpeed;
    private bool leftShieldActive = false;
    private bool rightShieldActive = false;

    void Start()
    {
        playerController = gameObject.GetComponent<Action.MovementHandler>();
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

        displacement = displacement * movementSpeed * Time.deltaTime;

        playerController.DoMovement(displacement);
    }

    private void checkAbilities()
    {
        /*if (Input.GetKeyDown("l"))
        {
            if (!leftShieldActive)
                playerController.enableLeftShield();
            else
                playerController.disableLeftShield();

            leftShieldActive = !leftShieldActive;
        }

        if (Input.GetKeyDown("r"))
        {
            if (!rightShieldActive)
                playerController.enableRightShield();
            else
                playerController.disableRightShield();

            rightShieldActive = !rightShieldActive;
        }

        if (Input.GetKeyDown("p"))
            playerController.activateProjectile();

        if (Input.GetKeyDown("u"))
            playerController.activateUppercut();

        if (Input.GetKeyDown("g"))
            playerController.activateGroundPound();*/
    }
}
