using UnityEngine;

namespace Action
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 100f;

        private Animator animator;
        private Rigidbody rigidbody;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void DoMovement(Vector3 movementVector)
        {
            rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.deltaTime * movementVector);

            // Animation
            animator.SetFloat("Speed", movementVector.sqrMagnitude);
            animator.SetFloat("Horizontal", movementVector.x);
            animator.SetFloat("Vertical", movementVector.y);
        }

        public void DoRotation(Vector3 rotationVector)
        {
            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles + Time.deltaTime * rotationSpeed * rotationVector));
        }
    }
}
