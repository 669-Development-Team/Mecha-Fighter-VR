using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

public class NetworkPlayerController : MonoBehaviour
{
    //The opponent and player's animators
    private static Animator opponentAnim;

    //Target objects for IK
    [SerializeField] GameObject headTarget;
    [SerializeField] GameObject leftArmTarget;
    [SerializeField] GameObject rightArmTarget;

    //Ability scripts attatched to the opponent
    private ShieldAbility shieldScript;

    private void Start()
    {
        //Get references to all of the ability scripts
        shieldScript = gameObject.GetComponent<ShieldAbility>();

        opponentAnim = GetComponent<Animator>();
        headTarget = GameObject.Find("Head Target");
        leftArmTarget = GameObject.Find("Left Arm Target");
        rightArmTarget = GameObject.Find("Right Arm Target");
    }

    //Set the positions of the root and IK targets
    public void updatePositions(Vector3 cameraPos, Vector3 lHandPos, Vector3 rHandPos)
    {
        //Set the position of the opponent's root
        transform.position = new Vector3(cameraPos.x, 0, cameraPos.z);
        
        headTarget.transform.position = cameraPos;
        leftArmTarget.transform.position = lHandPos;
        rightArmTarget.transform.position = rHandPos;
    }

    //Set the rotations of the IK targets
    public void updateRotations(Quaternion cameraRot, Quaternion lHandRot, Quaternion rHandRot)
    {
        headTarget.transform.rotation = cameraRot;
        leftArmTarget.transform.rotation = lHandRot;
        rightArmTarget.transform.rotation = rHandRot;
    }

    //Set any animations
    public void setAnimation(Constants.animParam trigger)
    {
        opponentAnim.SetTrigger(trigger.ToString());
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
}