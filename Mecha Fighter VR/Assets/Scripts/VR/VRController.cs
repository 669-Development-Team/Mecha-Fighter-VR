using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float m_Gravity = 30.0f;
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;
    public float m_RotateIncrement = 45f;


    public SteamVR_Action_Boolean m_RotatePress = null;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_Speed = 0.0f;


    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
        
    }

    // Update is called once per frame
    private void Update()
    { 

        
        HandleHeight();
        CalculateMovement();
        SnapRotation();
        Debug.Log(m_MoveValue.axis);
    }

    void HandleHeight()
    {

        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;




        m_CharacterController.center = newCenter;

    }

    void CalculateMovement()
    {
        
        
        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;
        
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
        {
            m_Speed = 0.0f;
        }
        
        if (m_MoveValue.axis.y != 0.0f && m_MoveValue.axis.x != 0.0f)
        {
            m_Speed += m_MoveValue.axis.y;

            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

            //forward movement
            movement += orientation * (m_Speed * Vector3.forward *  m_Sensitivity);


        }
        // Gravity
        movement.y -= m_Gravity * Time.deltaTime;

        m_CharacterController.Move(movement);

    }

    private void SnapRotation()
    {
        float snapValue = 0.0f;

        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
        //if (m_MoveValue.axis.x < -0.3f && m_MoveValue.axis.x > -0.6f)
        //if(m_MoveValue.axis.x == -1.0)
        {
            snapValue = -Mathf.Abs(m_RotateIncrement);
        }

        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
        //if (m_MoveValue.axis.x > 0.3f && m_MoveValue.axis.x < 0.6f)
        //if (m_MoveValue.axis.x == 1.0)
        {
            snapValue = Mathf.Abs(m_RotateIncrement);
        }

        transform.RotateAround(m_Head.position, Vector3.up, snapValue);

        
    }
   
}
