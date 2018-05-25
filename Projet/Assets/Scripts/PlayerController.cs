using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody m_Player;
    public Rigidbody Ennemy;
    public float m_PlayerSpeed = 10f;
    public float m_PlayerDashSpeed = 20f;

    private float m_DashTimer = 0.2f;
    private float m_DashDuration = 0.3f;
    private float m_DashRecuperation = 0.5f;
    private bool m_CanDashUp = false;
    private bool m_UpDashActive = false;
    private bool m_CanDashDown = false;
    private bool m_DownDashActive = false;
    private bool m_CanDashLeft = false;
    private bool m_LeftDashActive = false;
    private bool m_CanDashRight = false;
    private bool m_RightDashActive = false;

    private Quaternion m_PlayerRot;
    private Vector3 m_TargetPosition = new Vector3();
    private Vector3 m_MoveDir = new Vector3();

    public float m_Life = 100f;
    public float m_DashCost = 10f;

    private void Start()
    {

    }

    private void Update()
    {
        m_TargetPosition = Ennemy.transform.position;
        this.transform.LookAt(m_TargetPosition);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(CanDashLeftTimer());
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_MoveDir = transform.forward;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && m_CanDashLeft)
        {
            StartCoroutine(CanDashLeftTimer());
            m_Life -= m_DashCost;
            StartCoroutine(DashRecovery());
        }
        else if (m_LeftDashActive)
        {
            m_PlayerSpeed = m_PlayerDashSpeed;
            m_MoveDir = transform.forward;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(CanDashRightTimer());
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_MoveDir = -transform.forward;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && m_CanDashRight)
        {
            StartCoroutine(CanDashRightTimer());
            m_Life -= m_DashCost;
            StartCoroutine(DashRecovery());
        }
        else if (m_RightDashActive)
        {
            m_PlayerSpeed = m_PlayerDashSpeed;
            m_MoveDir = -transform.forward;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(CanDashUpTimer());

        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            m_MoveDir = transform.right;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && m_CanDashUp)
        {
            StartCoroutine(CanDashUpTimer());
            m_Life -= m_DashCost;
            StartCoroutine(DashRecovery());
        }
        else if (m_UpDashActive)
        {
            m_PlayerSpeed = m_PlayerDashSpeed;
            m_MoveDir = transform.right;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(CanDashDownTimer());
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_MoveDir = -transform.right;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && m_CanDashDown)
        {
            StartCoroutine(CanDashDownTimer());
            m_Life -= m_DashCost;
            StartCoroutine(DashRecovery());
        }
        else if (m_DownDashActive)
        {
            m_PlayerSpeed = m_PlayerDashSpeed;
            m_MoveDir = -transform.right;
        }

        else
        {
            if (m_PlayerSpeed > 10f)
            {
                m_PlayerSpeed = 10f;
            }
            m_MoveDir = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_PlayerSpeed;
        m_MoveDir.y = m_Player.velocity.y;
        m_Player.velocity = m_MoveDir;
    }

    private IEnumerator CanDashUpTimer()
    {
        m_CanDashUp = true;
        yield return new WaitForSeconds(m_DashTimer);
        m_CanDashUp = false;
    }
    private IEnumerator UpDashIsActiveTimer()
    {
        m_UpDashActive = true;
        yield return new WaitForSeconds(m_DashDuration);
        m_UpDashActive = false;
    }

    private IEnumerator CanDashDownTimer()
    {
        m_CanDashDown = true;
        yield return new WaitForSeconds(m_DashTimer);
        m_CanDashDown = false;
    }
    private IEnumerator DownDashIsActiveTimer()
    {
        m_DownDashActive = true;
        yield return new WaitForSeconds(m_DashDuration);
        m_DownDashActive = false;
    }

    private IEnumerator CanDashLeftTimer()
    {
        m_CanDashLeft = true;
        yield return new WaitForSeconds(m_DashTimer);
        m_CanDashLeft = false;
    }
    private IEnumerator LeftDashIsActiveTimer()
    {
        m_LeftDashActive = true;
        yield return new WaitForSeconds(m_DashDuration);
        m_LeftDashActive = false;
    }

    private IEnumerator CanDashRightTimer()
    { 
        m_CanDashRight = true;
        yield return new WaitForSeconds(m_DashTimer);
        m_CanDashRight = false;
    }
    private IEnumerator RightDashIsActiveTimer()
    {
        m_RightDashActive = true;
        yield return new WaitForSeconds(m_DashDuration);
        m_RightDashActive = false;
    }

     private IEnumerator DashRecovery()
     {
         int i = 0;
         Debug.Log("DashRecovery");

         while  (i != m_DashCost)
         {
             yield return new WaitForSeconds(m_DashRecuperation);
             m_Life++;
             i++;
         }
     }
}
