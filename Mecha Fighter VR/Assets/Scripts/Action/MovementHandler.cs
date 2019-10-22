using UnityEngine;

namespace Action
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 100f;

        public Rigidbody rigidbody;

        /*private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }*/

        public void DoMovement(Vector3 movementVector)
        {
			if (rigidbody == null) return;
            rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.deltaTime * movementVector);
        }

        // Formerly used with the right joystick/trackpad. No longer used as the player should always be facing the opponent.
//        public void DoRotation(Vector3 rotationVector)
//        {
//            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles + Time.deltaTime * rotationSpeed * rotationVector));
//        }
    }
}
