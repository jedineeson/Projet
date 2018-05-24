using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody m_Player1;
    public Rigidbody m_Player2;
    public float m_PlayerSpeed = 10f;

    public float m_DodgeTimer = 0f;
    public float m_DodgeDuration = 0f;

    private bool m_DodgeIsActive = false;
    private Quaternion m_PlayerRot;
    private Vector3 m_TargetPosition = new Vector3();
    private Vector3 m_MoveDir = new Vector3();

    private void Start()
    {

    }

    private void Update()
    {
        m_TargetPosition = m_Player2.transform.position;
        this.transform.LookAt(m_TargetPosition);

        //while (m_DodgeIsActive)
        //{
        //    m_DodgeDuration = 0.2f;
        //    m_PlayerSpeed = 20f;
        //    m_MoveDir = -transform.right;
        //}

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            m_MoveDir = -transform.forward;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            m_MoveDir = transform.forward;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            m_MoveDir = -transform.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            m_MoveDir = transform.right;
        }
        else
        {
            m_PlayerSpeed = 10f;
            m_MoveDir = Vector3.zero;
        }

        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        //{
        //    m_DodgeTimer = 0.5f;
        //}
        //if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && m_DodgeTimer > 0f)
        //{
        //    m_DodgeIsActive = true;
        //}

        //if (m_DodgeTimer > 0)
        //{
        //    m_DodgeTimer -= 0.1f;
        //}


        //if (m_DodgeDuration > 0)
        //{
        //    m_DodgeDuration -= 0.1f;
        //    if(m_DodgeDuration <= 0)
        //    {
        //        m_DodgeIsActive = false;
        //    }
        //}

    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_PlayerSpeed;
        m_MoveDir.y = m_Player1.velocity.y;
        m_Player1.velocity = m_MoveDir;
    }

}
