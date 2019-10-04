using Action;
using UnityEngine;
using Valve.VR;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        public SteamVR_Action_Vector2 leftJoystick;
        public SteamVR_Action_Vector2 rightJoystick;

        private Vector2 leftAxis2D;
        private Vector2 rightAxis2D;

        private MovementHandler movementHandler;

        private void Start()
        {
            movementHandler = GetComponent<MovementHandler>();
        }

        private void Update()
        {
            ReadInputs();

            ControlMovement();
        }

        private void ReadInputs()
        {
            if (leftJoystick.axis != Vector2.zero)
            {
                Debug.Log(leftJoystick.axis);
            }

            leftAxis2D = leftJoystick.axis;
            rightAxis2D = rightJoystick.axis;
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
    }
}
