using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Quaternion m_PlayerRot;
    private Vector3 m_TargetPosition = new Vector3();

    public Rigidbody m_Player1;
    public Rigidbody m_Player2;
    public float m_Speed = 10f;
    public Vector3 m_MoveDir = new Vector3();

    private void Start()
    {
      
    }

    private void Update()
    {
        m_TargetPosition = m_Player2.transform.position;
        this.transform.LookAt(m_TargetPosition);

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            m_MoveDir = -transform.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            m_MoveDir = transform.right;
        }
        else
        {
            m_MoveDir = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_Speed;
        m_MoveDir.y = m_Player1.velocity.y;
        m_Player1.velocity = m_MoveDir;
    }

}
