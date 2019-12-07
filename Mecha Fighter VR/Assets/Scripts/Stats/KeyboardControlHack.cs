using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControlHack : MonoBehaviour
{
    public AnimatedGestureAbility projectile;
    public AnimatedGestureAbility groundPound;

    public ToggleMove leftArm;
    public ToggleMove rightArm;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i")) {
            projectile.ActivateAbility();
        }
        if (Input.GetKeyDown("k")) {
            groundPound.ActivateAbility();
        }
        if (Input.GetKeyDown("o")) {
            leftArm.setToggle(true);
        }
        if (Input.GetKeyUp("o")) {
            leftArm.setToggle(false);
        }
        if (Input.GetKeyDown("p")) {
            rightArm.setToggle(true);
        }
        if (Input.GetKeyUp("p")) {
            rightArm.setToggle(false);
        }

        
    }
}
