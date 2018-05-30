using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody m_Player;
    public Rigidbody m_Ennemy;
    public float m_PlayerSpeed = 10f;
    public float m_PlayerRotationSpeed = 20f;
    public float m_PlayerRotationDegree = 15f;
    public float m_PlayerDashSpeed = 20f;
    public float m_PlayerRotationDashSpeed = 300f;

    private float m_DashTimer = 0.2f;
    private float m_DashDuration = 0.2f;
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
    private Vector3 m_EnnemyPosition = new Vector3();
    private Vector3 m_PlayerPosition = new Vector3();
    private Vector3 m_MoveDir = new Vector3();

    public float m_Life = 100f;
    public float m_DashCost = 10f;

    private float m_DistanceBetweenPlayer;

    private void Start()
    {

    }

    private void Update()
    {

        m_DistanceBetweenPlayer = Vector3.Distance(m_Player.transform.position, m_Ennemy.transform.position); 

        m_EnnemyPosition = m_Ennemy.transform.position;
        //this.transform.LookAt(m_EnnemyPosition);
        
        //Left Arrow
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

        //Right Arrow
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

        //Up Arrow
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(CanDashUpTimer());

        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.RotateAround(m_EnnemyPosition, -Vector3.up, m_PlayerRotationSpeed);// * Time.deltaTime);
            //m_MoveDir = transform.right;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && m_CanDashUp)
        {
            StartCoroutine(UpDashIsActiveTimer());
            m_Life -= m_DashCost;
            StartCoroutine(DashRecovery());
        }
        else if (m_UpDashActive)
        {
            //m_PlayerSpeed = m_PlayerDashSpeed;
            m_PlayerRotationSpeed = m_PlayerRotationDashSpeed;
            //transform.RotateAround(m_EnnemyPosition, -Vector3.up, m_PlayerRotationDegree * Time.deltaTime);
            //m_MoveDir = transform.right;
        }

        //Down Arrow
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StopCoroutine("CanDashDownTimer");
            StartCoroutine(CanDashDownTimer());
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.RotateAround(m_EnnemyPosition, Vector3.up, m_PlayerRotationSpeed * Time.deltaTime);
            //m_MoveDir = -transform.right;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && m_CanDashDown)
        {
            StartCoroutine(DownDashIsActiveTimer());
            m_Life -= m_DashCost;
            StartCoroutine(DashRecovery());
        }
        else if (m_DownDashActive)
        {
            m_PlayerRotationSpeed = m_PlayerRotationDashSpeed;
            transform.RotateAround(m_EnnemyPosition, Vector3.up, m_PlayerRotationDegree * Time.deltaTime);
            //m_MoveDir = -transform.right;
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
        if(!m_DownDashActive || !m_UpDashActive)
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
        transform.RotateAround(m_EnnemyPosition, -Vector3.up, m_PlayerRotationSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        m_UpDashActive = false;
        m_PlayerRotationSpeed = 100f;
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
        //yield return new WaitForSeconds(m_DashDuration);
        yield return new WaitForSeconds(1f);
        m_DownDashActive = false;
        m_PlayerRotationSpeed = 100f;
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

         while  (i != m_DashCost)
         {
             yield return new WaitForSeconds(m_DashRecuperation);
             m_Life++;
             i++;
         }
     }
}
