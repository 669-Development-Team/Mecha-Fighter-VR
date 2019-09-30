using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Footstep : MonoBehaviour
{
    [SerializeField] private VisualEffect footstepVFX;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;

    private Animator animator;

    private float currentFrameFootstepLeft;
    private float currentFrameFootstepRight;
    private float lastFrameFootstepLeft;
    private float lastFrameFootstepRight;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check THIS FRAME to see if we need to play a sound for the left foot, RIGHT NOW...
        currentFrameFootstepLeft = animator.GetFloat("FootstepLeft");
        if (currentFrameFootstepLeft > 0 && lastFrameFootstepLeft < 0)
        {
            //is this frame's curve BIGGER than the last frames?
            RaycastHit surfaceHitLeft;
            Ray aboveLeftFoot = new Ray(leftFoot.transform.position + new Vector3(0, 1.5f, 0), Vector3.down);
            if (Physics.Raycast(aboveLeftFoot, out surfaceHitLeft, 2f))
            {
                Instantiate(footstepVFX, transform.position, Quaternion.identity); //Play LEFT FOOTSTEP
            }
        }
        lastFrameFootstepLeft = animator.GetFloat("FootstepLeft"); //get left foot's CURVE FLOAT from the Animator Controller, from the CURRENT FRAME.

        //Check THIS FRAME to see if we need to play a sound for the right foot, RIGHT NOW...
        currentFrameFootstepRight = animator.GetFloat("FootstepRight");
        if (currentFrameFootstepRight < 0 && lastFrameFootstepRight > 0)
        {
            //is this frame's curve SMALLER than last frames?
            RaycastHit surfaceHitRight;
            Ray aboveRightFoot = new Ray(rightFoot.transform.position + new Vector3(0, 1.5f, 0), Vector3.down);
            if (Physics.Raycast(aboveRightFoot, out surfaceHitRight, 2f))
            {
                Instantiate(footstepVFX, transform.position, Quaternion.identity); //Play RIGHT FOOTSTEP
            }
        }
        lastFrameFootstepRight = animator.GetFloat("FootstepRight"); //get right foot's CURVE FLOAT from the Animator Controller, from the CURRENT FRAME.
    }
}
