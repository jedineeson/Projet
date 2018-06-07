using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour 
{
	public float m_DoubleTapDelay = 0.5f;
	private float m_CurrentDoubleTapTime = 0f;
	private int m_TapCount = 0;

	public GameObject m_Bloque;
	private bool m_CanBloque;
	public GameObject m_Cylindre;
	public Animator m_AttackAnimation;

	//Mon Player
	public Rigidbody m_Player;
	//Mon Ennemie
    public Rigidbody m_Ennemy;
	//Combien de temps il me reste pour faire un dash?
	public float m_CanDashTimer = 0.2f;
	//Combien de temps dure un dash?
	public float m_IsDashingTimer = 0.2f;
	//Vitesse de déplacement actuel du joueur
	public float m_ActualSpeedX  = 0f;
	//Vitesse de déplacement actuel du joueur
	public float m_ActualSpeedZ  = 0f;
	//Vitesse maximum du déplacement du joueur
	public float m_ZMoveSpeed  = 50f;
	private float m_ZDashSpeed;	
	public float m_ZDashSpeedReference = 800f;
	public float m_ZBloqueSpeed = 20f;	
	
	//Vitesse maximum du déplacement du joueur
	public float m_XMoveSpeed  = 5f;
	public float m_XDashSpeed = 20f;
	public float m_XBloqueSpeed = 2f;	
	
	public float m_RecoveryTime = 0.5f;
	public int m_Life = 100;
    public int m_DashCost = 10;
	private int m_Recovery = 0;
	
	//Valeur de l'Input en X
	private float m_InputX = 0f;
	//Valeur de l'Input en Z
	private float m_InputZ = 0f;
	//Est-ce que je peux dash(déterminer par le m_CanDashTimer)?
	//private bool m_CanDash = false;
	//Est-ce que je suis en train de dasher?
	private bool m_IsDashingHorizontal = false;
	//Est-ce que je suis en train de dasher?
	private bool m_IsDashingVertical = false;
	//vecteur pour direction de mon personnage
	private Vector3 m_MoveDir = new Vector3();
	//vecteur pour position de mon ennemie
	private Vector3 m_EnnemyPosition = new Vector3();
	private float m_Distance; 

	private void Start()
	{
		m_ActualSpeedX = m_XMoveSpeed;
		m_ActualSpeedZ = m_ZMoveSpeed;
		m_Bloque.SetActive(false);
	}

	void Update () 
	{
		//vecteur pour position de mon ennemie = position du transform de mon ennemie
		m_EnnemyPosition = m_Ennemy.transform.position;
		//Mon Player regarde toujours en direction de l'ennemi
		m_EnnemyPosition.y = transform.position.y;
		transform.LookAt(m_EnnemyPosition);
		m_Distance = Vector3.Distance(m_EnnemyPosition, m_Player.transform.position);
		Debug.Log("Distance: " + m_Distance);
		m_ZDashSpeed = m_ZDashSpeedReference / m_Distance;
		
		if(Input.GetKeyDown(KeyCode.Q))
		{
        	m_AttackAnimation.Play("Attack");
		}
		
		if(Input.GetKey(KeyCode.E))
		{
        	m_CanBloque = true;
			m_Bloque.SetActive(true);
			m_ActualSpeedX = m_XBloqueSpeed;
			m_ActualSpeedZ = m_ZBloqueSpeed;
			
		}
		else
		{
			m_CanBloque = false;
			m_Bloque.SetActive(false);
		}

		if(m_IsDashingHorizontal || m_CanBloque)
		{
			return;
		}
		else if (m_IsDashingVertical)
        {
            return;
        }

		CheckInputAxis();
		
		if(m_InputX != 0)
		{
			CheckDoubletapX();
		}
		if(m_InputZ != 0)
		{
			CheckDoubletapZ();
		}
	}

    private void FixedUpdate()
    {
		m_MoveDir = (m_InputX * transform.forward);
		m_MoveDir *= m_ActualSpeedX;
        m_MoveDir.y = m_Player.velocity.y;
        m_Player.velocity = m_MoveDir;

		transform.RotateAround(m_EnnemyPosition, (m_InputZ * Vector3.up), m_ActualSpeedZ * Time.deltaTime); 
    }

	//On vérifie l'Input en X, on donne sa valeur à m_InputX et on retourne vrai si il y a un input;
	private void CheckInputAxis()
	{
		m_InputX = Input.GetAxisRaw("Horizontal");
		m_InputZ = Input.GetAxisRaw("Vertical");
	}

	private void CheckDoubletapX()
	{
		Debug.Log("CheckDoubleTapX");
		if(m_TapCount >= 2)
		{
            StartCoroutine(DashingHorizontal());
			Debug.Log("Double Tap X");
			m_TapCount = 0;
			m_CurrentDoubleTapTime = 0f;
		}

		if(Input.GetButtonDown("Horizontal"))
		{
			if(m_TapCount == 0)
			{
				m_CurrentDoubleTapTime = 0f;
			}
			m_TapCount++;
		}

		m_CurrentDoubleTapTime += Time.deltaTime;
		if(m_CurrentDoubleTapTime >= m_DoubleTapDelay)
		{
			m_TapCount = 0;
			m_CurrentDoubleTapTime = 0f;
		}
	}

	private void CheckDoubletapZ()
	{
		Debug.Log("CheckDoubleTapZ");
		if(m_TapCount >= 2)
		{
            StartCoroutine(DashingVertical());
			Debug.Log("Double Tap Z");
			m_TapCount = 0;
			m_CurrentDoubleTapTime = 0f;
		}

		if(Input.GetButtonDown("Vertical"))
		{
			if(m_TapCount == 0)
			{
				m_CurrentDoubleTapTime = 0f;
			}
			m_TapCount++;
		}

		m_CurrentDoubleTapTime += Time.deltaTime;
		if(m_CurrentDoubleTapTime >= m_DoubleTapDelay)
		{
			m_TapCount = 0;
			m_CurrentDoubleTapTime = 0f;
		}
	}

	private IEnumerator DashingHorizontal()
	{
		m_Life -= m_DashCost;
		m_Recovery += m_DashCost;
		m_ActualSpeedX = m_XDashSpeed;
		m_IsDashingHorizontal = true;
		yield return new WaitForSeconds(m_IsDashingTimer);
		m_IsDashingHorizontal = false;
		m_ActualSpeedX = m_XMoveSpeed;
		StartCoroutine(DashRecovery());
	}	

	private IEnumerator DashingVertical()
	{
		m_Life -= m_DashCost;
		m_Recovery += m_DashCost;
		m_ActualSpeedZ = m_ZDashSpeed;
		m_IsDashingHorizontal = true;
		yield return new WaitForSeconds(m_IsDashingTimer);
		m_IsDashingHorizontal = false;
		m_ActualSpeedZ = m_ZMoveSpeed;
		StartCoroutine(DashRecovery());
	}	

	//m_Recovery==EnergyRecoveryQuantity, je récupère le coût en énergie d'un dash
	private IEnumerator DashRecovery()
	{
		while(m_Recovery != 0)
		{
			m_Recovery -= 1;
			m_Life += 1;
			yield return new WaitForSeconds(m_RecoveryTime);
		}
	}
}