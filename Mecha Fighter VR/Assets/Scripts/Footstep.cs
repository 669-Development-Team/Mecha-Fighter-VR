using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Footstep : MonoBehaviour
{
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private VisualEffect footstepVfx;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepSfx;

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
        TriggerFootstep();
    }

    private void TriggerFootstep()
    {
        // Check this frame if foot hits the ground
        currentFrameFootstepLeft = animator.GetFloat("FootstepLeft");
        currentFrameFootstepRight = animator.GetFloat("FootstepRight");

        if (currentFrameFootstepLeft > 0 && lastFrameFootstepLeft < 0)
        {
            // Play LEFT FOOTSTEP
            PlayVfx(leftFoot);
            PlaySfx();
        }

        if (currentFrameFootstepRight < 0 && lastFrameFootstepRight > 0)
        {
            // Play RIGHT FOOTSTEP
            PlayVfx(rightFoot);
            PlaySfx();
        }

        // Record current frame into last frame for each foot from animation curve
        lastFrameFootstepLeft = animator.GetFloat("FootstepLeft");
        lastFrameFootstepRight = animator.GetFloat("FootstepRight");
    }

    public void PlayVfx(Transform footTransform)
    {
        Instantiate(footstepVfx, footTransform.position, Quaternion.identity);
    }

    public void PlaySfx()
    {
        audioSource.PlayOneShot(footstepSfx[Random.Range(0, footstepSfx.Length - 1)], 0.8f);
        audioSource.Play();
    }
}
