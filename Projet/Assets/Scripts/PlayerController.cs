using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    //Pour les Inputs
    [SerializeField]
    [Range(1,2)]
    private int m_PlayerID;

    //Combien de temps dure un dash?
    [SerializeField]
    private float m_IsDashingTimer = 0.15f;

    //Vitesse maximum du déplacement du joueur
    [SerializeField]
    private float m_ZMoveSpeed = 50f;
    [SerializeField]
    private float m_ZDashSpeed = 800f;
    private float m_RelativeZDashSpeed;
    [SerializeField]
    private float m_ZBloqueSpeed = 25f;

    //Vitesse maximum du déplacement du joueur
    [SerializeField]
    private float m_XMoveSpeed = 5f;
    [SerializeField]
    private float m_XDashSpeed = 15f;
    [SerializeField]
    private float m_XBloqueSpeed = 2.5f;

    //Vitesse qu'on récupère du stamina/vie
    [SerializeField]
    private float m_RecoveryTime = 4f;
    //Stamina/Vie maximum
    
    public float m_Life = 100f;

    //Coût en stamina/vie d'un Dash
    [SerializeField]
    private float m_DashCost = 10f;
    //Coût en stamina/vie d'une attack
    [SerializeField]
    private float m_NormalAttackCost = 10f;
    [SerializeField]
    private float m_NormalAttackDamage = 20f;
    [SerializeField]
    private float m_RangeAttackCost = 10f;
    [SerializeField]
    private float m_RangeAttackDamage = 10f;

    //Temps entre le double tap
    [SerializeField]
    private float m_DoubleTapDelay = 0.5f;

    [SerializeField]
    private GameObject m_AttackRangeBullet;
    [SerializeField]
    private GameObject m_AttackRangeSpawn;
    //Cylindre pour illustrer le blocage
    [SerializeField]
    private GameObject m_BloqueObject;

    //Cube pour illustrer l'attaque
    [SerializeField]
    private GameObject m_AttackObject;
    //Animation de l'attaque
    [SerializeField]
    private Animator m_AttackAnimation;

    //Mon Player
    [SerializeField]
    private Rigidbody m_Player;
    //Mon Ennemie
    [SerializeField]
    private Rigidbody m_Ennemy;
    [SerializeField]
    private PlayerController m_EnnemyController;


    private int m_TapCountX = 0;
	private int m_TapCountZ = 0;
    //Timer du double tap
    private float m_CurrentDoubleTapTime = 0f;
    private float m_Distance;
    //Vitesse de déplacement actuel du joueur en X
    private float m_ActualSpeedX  = 0f;
	//Vitesse de déplacement actuel du joueur en Z
	private float m_ActualSpeedZ  = 0f;
    //Quantité de stamina qu'il reste à récupérer
	private float m_Recovery = 0;
	//Valeur de l'Input en X
	private float m_InputX = 0f;
	//Valeur de l'Input en Z
	private float m_InputZ = 0f;
    
    //Bool pour bloquer les mouvements durant qu'on bloque
    private bool m_CanBloque;
    //Est-ce que je suis en train de dasher?
    private bool m_IsDashingHorizontal = false;
	//Est-ce que je suis en train de dasher?
	private bool m_IsDashingVertical = false;
	
    //vecteur pour direction de mon personnage
	private Vector3 m_MoveDir = new Vector3();
	//vecteur pour position de mon ennemie
	private Vector3 m_EnnemyPosition = new Vector3();


	private void Start()
	{
		m_ActualSpeedX = m_XMoveSpeed;
		m_ActualSpeedZ = m_ZMoveSpeed;
		m_BloqueObject.SetActive(false);
	}

	void Update () 
	{
		//vecteur pour position de mon ennemie = position du transform de mon ennemie
		m_EnnemyPosition = m_Ennemy.transform.position;
		//Mon Player regarde toujours en direction de l'ennemi
		m_EnnemyPosition.y = transform.position.y;
		transform.LookAt(m_EnnemyPosition);
		m_Distance = Vector3.Distance(m_EnnemyPosition, m_Player.transform.position);
		//Debug.Log("Distance: " + m_Distance);
		m_RelativeZDashSpeed = m_ZDashSpeed / m_Distance;

		if(Input.GetButton("Bloque_p" + m_PlayerID))
		{
        	m_CanBloque = true;
			m_BloqueObject.SetActive(true);
			m_ActualSpeedX = m_XBloqueSpeed;
			m_ActualSpeedZ = m_ZBloqueSpeed;
			
		}
		else if(Input.GetButtonDown("Fire_p" + m_PlayerID) && m_Life >= m_NormalAttackCost+1)
		{
			m_Life -= m_NormalAttackCost;
        	m_AttackAnimation.Play("Attack");
            m_Recovery += m_NormalAttackCost;
		}
		else if(Input.GetButtonDown("Fire2_p" + m_PlayerID) && m_Life >= m_RangeAttackCost+1)
		{    
        	Instantiate(m_AttackRangeBullet, m_AttackRangeSpawn.transform.position, m_AttackRangeSpawn.transform.rotation);
			m_Life -= m_NormalAttackCost;
			m_Recovery += m_RangeAttackCost;				
		}
		else
		{
			m_CanBloque = false;
			m_BloqueObject.SetActive(false);
		}
		
		if(m_Recovery > 0)
		{
			m_Recovery -= 1 * (Time.deltaTime * m_RecoveryTime);
			m_Life += 1 * (Time.deltaTime * m_RecoveryTime);
		}

		if(m_IsDashingHorizontal || m_CanBloque)
		{
			return;
		}
		else if (m_IsDashingVertical || m_CanBloque)
        {
            return;
        }

		if(m_Life <= 0)
		{
			Destroy(m_Player.gameObject);
		}


		CheckDoubletapX();
		
		CheckDoubletapZ();

		CheckInputAxis();	
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
		m_InputX = Input.GetAxisRaw("Horizontal_p" + m_PlayerID);
		m_InputZ = Input.GetAxisRaw("Vertical_p" + m_PlayerID);
	}

	private void CheckDoubletapX()
	{
		if(m_TapCountX >= 2 && m_Life >= m_DashCost+1)
		{
            StartCoroutine(DashingHorizontal());
			//Debug.Log("Double Tap X");
			m_TapCountX = 0;
			m_CurrentDoubleTapTime = 0f;
		}

		if(Input.GetButtonDown("Horizontal_p" + m_PlayerID))
		{
			if(m_TapCountX == 0)
			{
				m_CurrentDoubleTapTime = 0f;
			}
			m_TapCountX++;
		}

		m_CurrentDoubleTapTime += Time.deltaTime;
		if(m_CurrentDoubleTapTime >= m_DoubleTapDelay)
		{
			m_TapCountX = 0;
			m_CurrentDoubleTapTime = 0f;
		}
	}

	private void CheckDoubletapZ()
	{
		//Debug.Log("CheckDoubleTapZ");
		if(m_TapCountZ >= 2 && m_Life >= m_DashCost+1)
		{
            StartCoroutine(DashingVertical());
			Debug.Log("Double Tap Z");
			m_TapCountZ = 0;
			m_CurrentDoubleTapTime = 0f;
		}

		if(Input.GetButtonDown("Vertical_p" + m_PlayerID))
		{
			if(m_TapCountZ == 0)
			{
				m_CurrentDoubleTapTime = 0f;
			}
			m_TapCountZ++;
		}

		m_CurrentDoubleTapTime += Time.deltaTime;
		if(m_CurrentDoubleTapTime >= m_DoubleTapDelay)
		{
			m_TapCountZ = 0;
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
	}	

	private IEnumerator DashingVertical()
	{
		m_Life -= m_DashCost;
		m_Recovery += m_DashCost;
		m_ActualSpeedZ = m_RelativeZDashSpeed;
		m_IsDashingHorizontal = true;
		yield return new WaitForSeconds(m_IsDashingTimer);
		m_IsDashingHorizontal = false;
		m_ActualSpeedZ = m_ZMoveSpeed;
	}

	private void OnTriggerEnter(Collider aOther)
	{
		if (aOther.gameObject.layer == LayerMask.NameToLayer("Weapon"))
		{			
			if(m_CanBloque)
			{
				m_Life -= m_NormalAttackCost;
				m_Recovery += m_NormalAttackCost;
				m_EnnemyController.m_Recovery += m_NormalAttackCost;
			}
			else
			{
				m_Life -= m_NormalAttackDamage;
                m_EnnemyController.m_Life += m_EnnemyController.m_Recovery;
                m_EnnemyController.m_Recovery = 0;

			}
		}
		
		if (aOther.gameObject.layer == LayerMask.NameToLayer("Bullet"))
		{
			Debug.Log("Got Shot!");
			
			if(m_CanBloque)
			{
				m_Life -= m_RangeAttackDamage/2;
				Destroy(aOther.gameObject);
			}
			else
			{
				m_Life -= m_RangeAttackDamage;
				Destroy(aOther.gameObject);
			}
		}
	}
}