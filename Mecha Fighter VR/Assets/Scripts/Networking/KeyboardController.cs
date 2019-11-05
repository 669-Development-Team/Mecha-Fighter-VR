using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    private NetworkPlayerController playerController;
    private bool leftShieldActive = false;
    private bool rightShieldActive = false;

    void Start()
    {
        playerController = gameObject.GetComponent<NetworkPlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
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
            playerController.activateGroundPound();
    }
}
