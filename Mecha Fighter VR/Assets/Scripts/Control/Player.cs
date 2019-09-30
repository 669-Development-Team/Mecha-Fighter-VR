using UnityEngine;
using Valve.VR;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;

    public SteamVR_Action_Vector2 leftJoystick;
    public SteamVR_Action_Vector2 rightJoystick;

    private Animator animator;
    private Rigidbody rigidbody;

    private Vector2 leftJoystickInput;
    private Vector2 rightJoystickInput;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ReadInputs();

        if (leftJoystick.axis != Vector2.zero)
        {
            Debug.Log(leftJoystick.axis);
        }

        // Movement with left stick
        Vector3 movementDirection = leftJoystickInput.x * transform.right + leftJoystickInput.y * transform.forward;
        rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.deltaTime * movementDirection);

        // Rotate player to movement direction
//        rigidbody.rotation = Quaternion.AngleAxis(Mathf.Atan2(inputs.x, inputs.z) * Mathf.Rad2Deg, Vector3.up);

        // Rotation with right stick
        Vector3 rotationVector = new Vector3(0, rightJoystickInput.x, 0);    // Rotate x on the y-axis
        rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles + Time.deltaTime * rotationSpeed * rotationVector));

        // Animation
        animator.SetFloat("Speed", leftJoystickInput.sqrMagnitude);
        animator.SetFloat("Horizontal", leftJoystickInput.x);
        animator.SetFloat("Vertical", leftJoystickInput.y);
    }

    void ReadInputs()
    {
        ReadLeftJoystick();
        ReadRightJoystick();
    }

    void ReadLeftJoystick()
    {
        if (leftJoystick == null)
        {
            return;
        }
        leftJoystickInput.x = leftJoystick.axis.x;
        leftJoystickInput.y = leftJoystick.axis.y;
    }

    void ReadRightJoystick()
    {
        if (rightJoystick == null)
        {
            return;
        }
        rightJoystickInput.x = rightJoystick.axis.x;
        rightJoystickInput.y = rightJoystick.axis.y;
    }
}
