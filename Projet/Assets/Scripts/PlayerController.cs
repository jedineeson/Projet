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
    private bool m_CanDodgeUp = false;
    private bool m_UpDodgeActive = false;
    private bool m_CanDodgeDown = false;
    private bool m_DownDodgeActive = false;
    private bool m_CanDodgeLeft = false;
    private bool m_LeftDodgeActive = false;
    private bool m_CanDodgeRight = false;
    private bool m_RightDodgeActive = false;
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

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(CanDodgeLeftTimer());
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_MoveDir = -transform.forward;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && m_CanDodgeLeft)
        {
            StartCoroutine(CanDodgeLeftTimer());
        }
        else if (m_LeftDodgeActive)
        {
            m_PlayerSpeed = 20f;
            m_MoveDir = -transform.forward;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(CanDodgeRightTimer());
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_MoveDir = transform.forward;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && m_CanDodgeRight)
        {
            StartCoroutine(CanDodgeRightTimer());
        }
        else if (m_RightDodgeActive)
        {
            m_PlayerSpeed = 20f;
            m_MoveDir = transform.forward;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(CanDodgeUpTimer());
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            m_MoveDir = -transform.right;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && m_CanDodgeUp)
        {
            StartCoroutine(CanDodgeUpTimer());
        }
        else if (m_UpDodgeActive)
        {
            m_PlayerSpeed = 20f;
            m_MoveDir = -transform.right;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(CanDodgeDownTimer());
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_MoveDir = transform.right;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && m_CanDodgeDown)
        {
            StartCoroutine(CanDodgeDownTimer());
        }
        else if (m_DownDodgeActive)
        {
            m_PlayerSpeed = 20f;
            m_MoveDir = transform.right;
        }

        else
        {
            m_PlayerSpeed = 10f;
            m_MoveDir = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_PlayerSpeed;
        m_MoveDir.y = m_Player1.velocity.y;
        m_Player1.velocity = m_MoveDir;
    }

    private IEnumerator CanDodgeUpTimer()
    {
        m_CanDodgeUp = true;
        yield return new WaitForSeconds(0.3f);
        m_CanDodgeUp = false;
    }
    private IEnumerator UpDodgeIsActiveTimer()
    {
        m_UpDodgeActive = true;
        yield return new WaitForSeconds(0.5f);
        m_UpDodgeActive = false;
    }

    private IEnumerator CanDodgeDownTimer()
    {
        m_CanDodgeDown = true;
        yield return new WaitForSeconds(0.3f);
        m_CanDodgeDown = false;
    }
    private IEnumerator DownDodgeIsActiveTimer()
    {
        m_DownDodgeActive = true;
        yield return new WaitForSeconds(0.5f);
        m_DownDodgeActive = false;
    }

    private IEnumerator CanDodgeLeftTimer()
    {
        m_CanDodgeLeft = true;
        yield return new WaitForSeconds(0.3f);
        m_CanDodgeLeft = false;
    }
    private IEnumerator LeftDodgeIsActiveTimer()
    {
        m_LeftDodgeActive = true;
        yield return new WaitForSeconds(0.5f);
        m_LeftDodgeActive = false;
    }

    private IEnumerator CanDodgeRightTimer()
    {
        m_CanDodgeRight = true;
        yield return new WaitForSeconds(0.3f);
        m_CanDodgeRight = false;
    }
    private IEnumerator RightDodgeIsActiveTimer()
    {
        m_RightDodgeActive = true;
        yield return new WaitForSeconds(0.5f);
        m_RightDodgeActive = false;
    }
}
