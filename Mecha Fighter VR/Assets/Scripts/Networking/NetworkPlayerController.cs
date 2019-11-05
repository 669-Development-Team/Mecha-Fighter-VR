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

    //Ability scripts attatched to the opponent
    private ShieldAbility shieldScript;
    private ProjectileAbility projectileScript;
    private UppercutAbility uppercutScript;
    private GroundPoundAbility groundPoundScipt;

    private void Start()
    {
        //Get references to all of the ability scripts
        shieldScript = gameObject.GetComponent<ShieldAbility>();
        projectileScript = gameObject.GetComponent<ProjectileAbility>();
        uppercutScript = gameObject.GetComponent<UppercutAbility>();
        groundPoundScipt = gameObject.GetComponent<GroundPoundAbility>();
    }

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
        projectileScript.ActivateAbility();
    }

    public void activateUppercut()
    {
        uppercutScript.ActivateAbility(null);
    }

    public void activateGroundPound()
    {
        groundPoundScipt.ActivateAbility(null);
    }

    public void setPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}