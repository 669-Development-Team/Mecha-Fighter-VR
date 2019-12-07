using UnityEngine;
using UnityEngine.Experimental.VFX;

/// <summary>
/// This script is used with Animator. The public methods may be called from VRIK Unity Events.
/// </summary>
public class Footstep : MonoBehaviour
{
//    [SerializeField] private Transform leftFoot = null;
//    [SerializeField] private Transform rightFoot = null;
    [SerializeField] private VisualEffect footstepVfx = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip[] footstepSfx = null;

//    private Animator animator = null;
//
//    private float currentFrameFootstepLeft = 0f;
//    private float currentFrameFootstepRight = 0f;
//    private float lastFrameFootstepLeft = 0f;
//    private float lastFrameFootstepRight = 0f;

//    private void Awake()
//    {
//        animator = GetComponent<Animator>();
//    }
//
//    private void Update()
//    {
//        TriggerFootstep();
//    }
//
//    private void TriggerFootstep()
//    {
//        // Check this frame if foot hits the ground
//        currentFrameFootstepLeft = animator.GetFloat("FootstepLeft");
//        currentFrameFootstepRight = animator.GetFloat("FootstepRight");
//
//        if (currentFrameFootstepLeft > 0 && lastFrameFootstepLeft < 0)
//        {
//            // Play LEFT FOOTSTEP
//            PlayVfx(leftFoot);
//            PlaySfx();
//        }
//
//        if (currentFrameFootstepRight < 0 && lastFrameFootstepRight > 0)
//        {
//            // Play RIGHT FOOTSTEP
//            PlayVfx(rightFoot);
//            PlaySfx();
//        }
//
//        // Record current frame into last frame for each foot from animation curve
//        lastFrameFootstepLeft = animator.GetFloat("FootstepLeft");
//        lastFrameFootstepRight = animator.GetFloat("FootstepRight");
//    }

    public void PlayVfx(Transform footTransform)
    {
        Instantiate(footstepVfx, footTransform.position, Quaternion.identity);
    }

    public void PlaySfx()
    {
        audioSource.PlayOneShot(footstepSfx[Random.Range(0, footstepSfx.Length - 1)], 1f);
        audioSource.Play();
    }
}
