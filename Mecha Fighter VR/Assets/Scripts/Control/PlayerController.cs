using Action;
using Stats;
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

        [Tooltip("GameObject that the player will always be facing and attacking")]
        [SerializeField] private Health opponent = null;

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
        private GestureHandler gestureHandler = null;
        private GroundPoundAbility groundPoundAbility = null;
        private MovementHandler movementHandler = null;
        private ProjectileAbility projectileAbility = null;
        private ShieldAbility shieldAbility = null;
        private UppercutAbility uppercutAbility = null;

        /// <summary>
        /// Get values
        /// </summary>
        public static Vector3 getMovementValue;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            gestureHandler = GetComponent<GestureHandler>();
            groundPoundAbility = GetComponent<GroundPoundAbility>();
            movementHandler = GetComponent<MovementHandler>();
            projectileAbility = GetComponent<ProjectileAbility>();
            shieldAbility = GetComponent<ShieldAbility>();
            uppercutAbility = GetComponent<UppercutAbility>();
        }

        private void Start()
        {
            // Subscribe SteamVR inputs listener events
            joystick.AddOnUpdateListener(ControlMovement, SteamVR_Input_Sources.LeftHand);

            trigger.AddOnChangeListener(TriggerChange, SteamVR_Input_Sources.LeftHand);
            trigger.AddOnChangeListener(TriggerChange, SteamVR_Input_Sources.RightHand);

            gripAction.AddOnStateDownListener(GripDown, SteamVR_Input_Sources.LeftHand);
            gripAction.AddOnStateUpListener(GripUp, SteamVR_Input_Sources.LeftHand);
            gripAction.AddOnStateDownListener(GripDown, SteamVR_Input_Sources.RightHand);
            gripAction.AddOnStateUpListener(GripUp, SteamVR_Input_Sources.RightHand);
        }

        // Movement with left stick
        private void ControlMovement(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
        {
            if (animator.GetFloat("IKWeight") != 0)
            {
                Vector3 movementDirection = axis.x * transform.right + axis.y * transform.forward;
                movementHandler.DoMovement(movementDirection);

                //
                getMovementValue = movementDirection;

                // No longer used as the player now uses IK
                //            // Animation
                //            animator.SetFloat("Speed", axis.sqrMagnitude);
                //            animator.SetFloat("Horizontal", axis.x);
                //            animator.SetFloat("Vertical", axis.y);

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
        }

        // Get movement value
        Vector3 GetmovementValue()
        {
            return getMovementValue;
        }

        private void TriggerChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
        {
            if (fromSource == SteamVR_Input_Sources.LeftHand) shieldAbility.ToggleLeftShield(newState);
            if (fromSource == SteamVR_Input_Sources.RightHand) shieldAbility.ToggleRightShield(newState);
        }

        private void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // Record left hand start position
            if (fromSource == SteamVR_Input_Sources.LeftHand)
            {
                leftGripDownPosition = transform.InverseTransformDirection(leftHandTransform.position);
                Debug.Log("Left grip down: " + leftGripDownPosition + ", " + leftGripDownPosition.magnitude);
            }

            // Record right hand start position
            if (fromSource == SteamVR_Input_Sources.RightHand)
            {
                rightGripDownPosition = transform.InverseTransformDirection(rightHandTransform.position);
                Debug.Log("Right grip down: " + rightGripDownPosition + ", " + rightGripDownPosition.magnitude);
            }

            // Begin motion timer
            gestureHandler.StartMotionTime();
        }

        private void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // Record left hand end position
            if (fromSource == SteamVR_Input_Sources.LeftHand)
            {
                leftGripUpPosition = transform.InverseTransformDirection(leftHandTransform.position);
                Debug.Log("Left grip up: " + leftGripUpPosition + ", " + leftGripUpPosition.magnitude);
            }

            // Record right hand end position
            if (fromSource == SteamVR_Input_Sources.RightHand)
            {
                rightGripUpPosition = transform.InverseTransformDirection(rightHandTransform.position);
                Debug.Log("Right grip up: " + rightGripUpPosition + ", " + rightGripUpPosition.magnitude);
            }

            // Check if the motion was performed satisfies the maximum time threshold
            if (gestureHandler.IsWithinMotionTimeThreshold())
            {
                // Check which gesture was performed and trigger the correct ability accordingly
                // Certain abilities may be activated with only one hand
                // TODO: There definitely should be a better way of doing this than a switch block
                switch (gestureHandler.CheckGesture(leftGripDownPosition, leftGripUpPosition, rightGripDownPosition, rightGripUpPosition))
                {
                    // Shoot projectile if the user does a hadouken motion within the time threshold
                    case GestureHandler.Gestures.Projectile:
                        projectileAbility.ActivateAbility(opponent);
                        break;
                    // TODO: Uppercut ability
                    case GestureHandler.Gestures.Uppercut:
                        uppercutAbility.ActivateAbility(opponent);
                        break;
                    // TODO: Ground pound ability
                    case GestureHandler.Gestures.GroundPound:
                        groundPoundAbility.ActivateAbility(opponent);
                        break;
                    default:
                        break;
                }
            }

            // Reset vectors
            leftGripDownPosition = Vector3.zero;
            leftGripUpPosition = Vector3.zero;
            rightGripDownPosition = Vector3.zero;
            rightGripUpPosition = Vector3.zero;
        }
    }
}
