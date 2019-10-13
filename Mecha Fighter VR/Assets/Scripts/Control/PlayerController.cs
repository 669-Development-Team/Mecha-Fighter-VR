using Action;
using UnityEngine;
using Valve.VR;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        public SteamVR_Action_Vector2 leftJoystick;
        public SteamVR_Action_Vector2 rightJoystick;
        public SteamVR_Action_Boolean leftTrigger;
        public SteamVR_Action_Boolean rightTrigger;

        private Vector2 leftAxis2D;
        private Vector2 rightAxis2D;
        private bool leftShieldTriggered = false;
        private bool rightShieldTriggered = false;

        private MovementHandler movementHandler;
        private ShieldAbility shieldAbility;

        private void Start()
        {
            movementHandler = GetComponent<MovementHandler>();
            shieldAbility = GetComponent<ShieldAbility>();
        }

        private void Update()
        {
            ReadInputs();

            ControlMovement();
            ControlShield();
        }

        private void ReadInputs()
        {
            leftAxis2D = leftJoystick.axis;
            rightAxis2D = rightJoystick.axis;

            leftShieldTriggered = leftTrigger.state;
            rightShieldTriggered = rightTrigger.state;
        }

        private void ControlMovement()
        {
            // Movement with left stick
            Vector3 movementDirection = leftAxis2D.x * transform.right + leftAxis2D.y * transform.forward;
            movementHandler.DoMovement(movementDirection);

            // Rotation with right stick
            Vector3 rotationVector = new Vector3(0, rightAxis2D.x, 0); // Rotate x on the y-axis
            movementHandler.DoRotation(rotationVector);
        }

        private void ControlShield()
        {
            shieldAbility.ToggleLeftShield(leftShieldTriggered);
            shieldAbility.ToggleRightShield(rightShieldTriggered);
        }
    }
}
