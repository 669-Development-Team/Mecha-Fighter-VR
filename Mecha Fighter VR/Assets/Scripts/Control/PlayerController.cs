using Action;
using UnityEngine;
using Valve.VR;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// SteamVR input actions
        /// </summary>
        [SerializeField] private SteamVR_Action_Vector2 joystick = null;
        [SerializeField] private SteamVR_Action_Boolean trigger = null;
        [SerializeField] private SteamVR_Action_Boolean gripAction = null;

        [Tooltip("Left controller Game Object transform")]
        [SerializeField] private Transform leftHandTransform = null;
        [Tooltip("Right controller Game Object transform")]
        [SerializeField] private Transform rightHandTransform = null;

        [Tooltip("GameObject that the player will always be facing")]
        [SerializeField] private GameObject opponent = null;

        /// <summary>
        /// Values read from SteamVR input actions
        /// </summary>
        private Vector3 leftGripDownPosition;       // Input from left grip pressed and left hand position
        private Vector3 rightGripDownPosition;      // Input from right grip pressed and right hand position
        private Vector3 leftGripUpPosition;         // Input from left grip released and right hand position
        private Vector3 rightGripUpPosition;        // Input from right grip released and right hand position

        /// <summary>
        /// GameObject components
        /// </summary>
        private Animator animator = null;
        private MovementHandler movementHandler = null;
        private ProjectileAbility projectileAbility = null;
        private ShieldAbility shieldAbility = null;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            movementHandler = GetComponent<MovementHandler>();
            projectileAbility = GetComponent<ProjectileAbility>();
            shieldAbility = GetComponent<ShieldAbility>();
        }

        private void Start()
        {
            // Subscribe SteamVR inputs listener events
            joystick.AddOnAxisListener(ControlMovement, SteamVR_Input_Sources.LeftHand);
            trigger.AddOnChangeListener(TriggerChange, SteamVR_Input_Sources.LeftHand);
            trigger.AddOnChangeListener(TriggerChange, SteamVR_Input_Sources.RightHand);
            gripAction.AddOnStateDownListener(GripDown, SteamVR_Input_Sources.LeftHand);
            gripAction.AddOnStateUpListener(GripUp, SteamVR_Input_Sources.LeftHand);
            gripAction.AddOnStateDownListener(GripDown, SteamVR_Input_Sources.RightHand);
            gripAction.AddOnStateUpListener(GripUp, SteamVR_Input_Sources.RightHand);
        }

        private void ControlMovement(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
        {
            // Movement with left stick
            if (fromSource == SteamVR_Input_Sources.LeftHand)
            {
                Vector3 movementDirection = axis.x * transform.right + axis.y * transform.forward;
                movementHandler.DoMovement(movementDirection);
            }

            // No longer used as the player now uses IK
//            // Animation
//            animator.SetFloat("Speed", leftAxis2D.sqrMagnitude);
//            animator.SetFloat("Horizontal", leftAxis2D.x);
//            animator.SetFloat("Vertical", leftAxis2D.y);

            // No longer used as the player should always be facing the opponent.
//            // Rotation with right stick
//            Vector3 rotationVector = new Vector3(0, rightAxis2D.x, 0); // Rotate x on the y-axis
//            movementHandler.DoRotation(rotationVector);

            // Always look at opponent
            if (opponent != null)
            {
                // Only rotate y-axis
                Vector3 lookDirection = new Vector3(opponent.transform.position.x, transform.position.y, opponent.transform.position.z);
                transform.LookAt(lookDirection);
            }
        }

        private void TriggerChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
        {
            if (fromSource == SteamVR_Input_Sources.LeftHand) shieldAbility.ToggleLeftShield(newState);
            if (fromSource == SteamVR_Input_Sources.RightHand) shieldAbility.ToggleRightShield(newState);
        }

        private void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            if (fromSource == SteamVR_Input_Sources.LeftHand)
            {
                leftGripDownPosition = leftHandTransform.position;
                print("Left grip down: " + leftGripDownPosition + ", " + leftGripDownPosition.magnitude);
            }

            if (fromSource == SteamVR_Input_Sources.RightHand)
            {
                rightGripDownPosition = rightHandTransform.position;
                print("Right grip down: " + rightGripDownPosition + ", " + rightGripDownPosition.magnitude);
            }

            // Begin motion timer
            projectileAbility.StartMotionTime();
        }

        private void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            if (fromSource == SteamVR_Input_Sources.LeftHand)
            {
                leftGripUpPosition = leftHandTransform.position;
                print("Left grip up: " + leftGripUpPosition + ", " + leftGripUpPosition.magnitude);
            }

            if (fromSource == SteamVR_Input_Sources.RightHand)
            {
                rightGripUpPosition = rightHandTransform.position;
                print("Right grip up: " + rightGripUpPosition + ", " + rightGripUpPosition.magnitude);
            }

            // Shoot projectile if the user does a hadouken motion within the time threshold
            if (projectileAbility.IsForwardMotion(
                    leftGripDownPosition,
                    leftGripUpPosition,
                    rightGripDownPosition,
                    rightGripUpPosition,
                    opponent.transform.position) &&
                projectileAbility.IsWithinMotionTimeThreshold())
            {
                projectileAbility.ShootProjectile(opponent);
            }

            // Reset vectors
            leftGripDownPosition = Vector3.zero;
            leftGripUpPosition = Vector3.zero;
            rightGripDownPosition = Vector3.zero;
            rightGripUpPosition = Vector3.zero;
        }
    }
}
