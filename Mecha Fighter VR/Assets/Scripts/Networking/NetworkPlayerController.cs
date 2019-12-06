using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

public class NetworkPlayerController : MonoBehaviour
{
    //Target objects for IK
    [SerializeField]
    GameObject headTarget;
    [SerializeField]
    GameObject leftArmTarget;
    [SerializeField]
    GameObject rightArmTarget;

    //The player object (for the opponent to face)
    [SerializeField]
    GameObject player;

    //Ability scripts attatched to the opponent
    private ShieldAbility shieldScript;
    //private ProjectileAbility projectileScript;
    //private UppercutAbility uppercutScript;
    //private GroundPoundAbility groundPoundScipt;

    private void Start()
    {
        //Get references to all of the ability scripts
        shieldScript = gameObject.GetComponent<ShieldAbility>();
        //projectileScript = gameObject.GetComponent<ProjectileAbility>();
        //uppercutScript = gameObject.GetComponent<UppercutAbility>();
        //roundPoundScipt = gameObject.GetComponent<GroundPoundAbility>();
    }

    //Set the position of the opponnent
    public void setPosition(Vector3 position)
    {
        gameObject.transform.position = position;

        //Update the rotation to face the player
        transform.LookAt(player.transform.position);
    }

    //Functions to toggle abilities
    public void enableLeftShield()
    {
        shieldScript.ToggleLeftShield(true);
    }

    public void disableLeftShield()
    {
        shieldScript.ToggleLeftShield(false);
    }

    public void enableRightShield()
    {
        shieldScript.ToggleRightShield(true);
    }

    public void disableRightShield()
    {
        shieldScript.ToggleRightShield(false);
    }

    public void activateProjectile()
    {
        //projectileScript.ActivateAbility();
    }

    public void activateUppercut()
    {
        //uppercutScript.ActivateAbility(null);
    }

    public void activateGroundPound()
    {
        //groundPoundScipt.ActivateAbility(null);
    }
}