using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

public class NetworkPlayerController : MonoBehaviour
{
    //The opponent and player's animators
    private static Animator opponentAnim;

    //Target objects for IK
    [SerializeField] private GameObject headTarget;
    [SerializeField] private GameObject leftArmTarget;
    [SerializeField] private GameObject rightArmTarget;

    //Ability scripts attatched to the opponent
    private ShieldAbility shieldScript;

    //Arrays to store lerp targets and sources. The array contains the root position, IK positions, and IK rotations
    private Vector3[] lastPosition = new Vector3[4];
    private Vector3[] lastRotation = new Vector3[3];
    private Vector3[] newPosition = new Vector3[4];
    private Vector3[] newRotation = new Vector3[3];

    //Arrays to store the interpolation fractions for each variable
    private float[] positionFraction = new float[4];
    private float[] rotationFraction = new float[3];

    //The time at which the last update was received
    private float timeLastUpdate;

    private bool firstFrame = true;

    void Start()
    {
        //Get references to all of the ability scripts
        shieldScript = gameObject.GetComponent<ShieldAbility>();

        opponentAnim = GetComponent<Animator>();
        headTarget = GameObject.Find("Head Target");
        leftArmTarget = GameObject.Find("Left Arm Target");
        rightArmTarget = GameObject.Find("Right Arm Target");
    }

    void Update()
    {
        if(!firstFrame)
        {
            //Interpolate all of the positions
            transform.position = Vector3.Lerp(lastPosition[0], newPosition[0], positionFraction[0]);
            headTarget.transform.position = Vector3.Lerp(lastPosition[1], newPosition[1], positionFraction[1]);
            leftArmTarget.transform.position = Vector3.Lerp(lastPosition[2], newPosition[2], positionFraction[2]);
            rightArmTarget.transform.position = Vector3.Lerp(lastPosition[3], newPosition[3], positionFraction[3]);

            //Interpolate all of the rotations
            headTarget.transform.rotation = Quaternion.Euler(Vector3.Lerp(lastRotation[0], newRotation[0], rotationFraction[0]));
            leftArmTarget.transform.rotation = Quaternion.Euler(Vector3.Lerp(lastRotation[1], newRotation[1], rotationFraction[1]));
            rightArmTarget.transform.rotation = Quaternion.Euler(Vector3.Lerp(lastRotation[2], newRotation[2], rotationFraction[2]));
        }
    }

    //Caluclate what fraction of the journey should be lerped for each variable
    private void calculateFractions()
    {
        //How many frames it will take to complete the interpolation
        float interpolationFrames = (Time.time - timeLastUpdate) / 60;

        for (int i = 0; i < 4; i++)
            positionFraction[i] = (newPosition[i] - lastPosition[i]).magnitude / interpolationFrames;
        for (int i = 0; i < 3; i++)
            rotationFraction[i] = (newRotation[i] - lastRotation[i]).magnitude / interpolationFrames;
    }

    //Update the positions of the root and IK targets
    public void updatePositions(Vector3 cameraPos, Vector3 lHandPos, Vector3 rHandPos)
    {
        //Store the current variables
        lastPosition[0] = transform.position;
        lastPosition[1] = headTarget.transform.position;
        lastPosition[2] = leftArmTarget.transform.position;
        lastPosition[3] = rightArmTarget.transform.position;

        //Store the new update
        newPosition[0] = new Vector3(cameraPos.x, 0, cameraPos.z);
        newPosition[1] = cameraPos;
        newPosition[2] = lHandPos;
        newPosition[3] = rHandPos;
    }

    //Update the rotations of the IK targets
    public void updateRotations(Vector3 cameraRot, Vector3 lHandRot, Vector3 rHandRot)
    {
        //Store the current variables
        lastRotation[0] = headTarget.transform.rotation.eulerAngles;
        lastRotation[1] = leftArmTarget.transform.rotation.eulerAngles;
        lastRotation[2] = rightArmTarget.transform.rotation.eulerAngles;

        //Store the new update
        newRotation[0] = cameraRot;
        newRotation[1] = lHandRot;
        newRotation[2] = rHandRot;

        //Store the time of the update
        timeLastUpdate = Time.time;

        //Calculate the fractions for the lerp if it isn't the first frame
        if (!firstFrame)
            calculateFractions();
        else
            firstFrame = false;
    }

    //Set any animations
    public void setAnimation(Constants.animParam trigger)
    {
        opponentAnim.SetTrigger(trigger.ToString());
    }
}