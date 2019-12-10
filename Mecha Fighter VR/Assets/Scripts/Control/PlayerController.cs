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

        [Tooltip("GameObject that the player will always be facing and attacking")]
        [SerializeField] private PlayerStats opponent = null;
        /// <summary>
        /// Get values
        /// </summary>
        public static Vector3 getMovementValue;
		
		private Animator animator = null;
		private ShieldAbility shieldAbility = null;
		private MovementHandler movementHandler = null;

        private void Awake()
        {
            animator = GetComponent<Animator>();
			
			shieldAbility = GetComponent<ShieldAbility>();
			movementHandler = GetComponent<MovementHandler>();

            //Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
        }

        private void Start()
        {
            // Subscribe SteamVR inputs listener events
            joystick.AddOnUpdateListener(ControlMovement, SteamVR_Input_Sources.LeftHand);

            trigger.AddOnChangeListener(TriggerChange, SteamVR_Input_Sources.LeftHand);
            trigger.AddOnChangeListener(TriggerChange, SteamVR_Input_Sources.RightHand);
        }

        private void Update() {
            if (opponent != null)
            {
                // Only rotate y-axis
                Vector3 lookDirection = new Vector3(opponent.transform.position.x, transform.position.y, opponent.transform.position.z);
                transform.LookAt(lookDirection);
           }    
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
    }
}
