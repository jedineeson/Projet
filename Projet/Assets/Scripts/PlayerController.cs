using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    #region Paramétrable Dans L'Inspector
    [SerializeField]
	private PlayerData m_Data;
    //Pour les Inputs
    [SerializeField]
    [Range(1,2)]
    private int m_PlayerID;
    [SerializeField]
    private GameObject m_AttackRangeBullet;
    [SerializeField]
    private GameObject m_AttackRangeSpawn;
    //Cylindre pour illustrer le blocage
    [SerializeField]
    private GameObject m_BloqueObject;
    [SerializeField]
    private GameObject m_BloqueObject2;
    //Cube pour illustrer l'attaque
    [SerializeField]
    private GameObject m_AttackObject;
	[SerializeField]
    private GameObject m_AttackObject2;
    //Animation de l'attaque
    [SerializeField]
    private Animator m_AttackAnimation;
    //Animation de l'attaque
    [SerializeField]
    private Animator m_AttackAnimation2;
    //Mon Player
    [SerializeField]
    private Rigidbody m_Player;
    //Mon Ennemie
    [SerializeField]
    private Rigidbody m_Ennemy;
    [SerializeField]
    private PlayerController m_EnnemyController;
    #endregion
    #region Data
    //Stamina/Vie maximum
    private float m_Life;
    //Combien de temps dure un dash?
    private float m_IsDashingTimer;
    //Vitesse maximum du déplacement du joueur
    private float m_ZMoveSpeed;
    private float m_ZDashSpeed;
    private float m_ZBloqueSpeed;
	//Vitesse maximum du déplacement du joueur
    private float m_XMoveSpeed;
    private float m_XDashSpeed;
    private float m_XBloqueSpeed;
    //Vitesse qu'on récupère du stamina/vie
    private float m_RecoveryTime;
    //Coût en stamina/vie d'un Dash
    private float m_DashCost;
    //Coût en stamina/vie d'une attack
    private float m_NormalAttackCost;
    private float m_NormalAttackDamage;
    private float m_RangeAttackCost;
    private float m_RangeAttackDamage;
    //Temps entre le double tap
    private float m_DoubleTapDelay;
    #endregion
    #region Variables Privés
    private int m_TapCountX = 0;
	private int m_TapCountZ = 0;

    //Timer du double tap
    private float m_CurrentDoubleTapTime = 0f;
    private float m_Distance;
	private float m_RelativeZDashSpeed;
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

    //Est-ce que je suis en train d'effectuer un bloc?
    private bool m_CanBloqueHigh = false;
	private bool m_CanBloqueLow = false;
    //Est-ce que je suis en train d'attaquer Verticalement?
    private bool m_IsVerticalAttack = false;
    //Est-ce que je suis en train de dasher?
    private bool m_IsDashingHorizontal = false;
	//Est-ce que je suis en train de dasher?
	private bool m_IsDashingVertical = false;

    //vecteur pour direction de mon personnage
	private Vector3 m_MoveDir = new Vector3();
	//vecteur pour position de mon ennemie
	private Vector3 m_EnnemyPosition = new Vector3();
    #endregion
    #region Getter
    //getter de Life pour le UI
    public float Life
    {
        get { return m_Life; }
    }
    #endregion

    private void Start()
	{
        //Data
		m_IsDashingTimer = m_Data.IsDashingTimer;
    	m_ZMoveSpeed = m_Data.ZMoveSpeed;
    	m_ZDashSpeed = m_Data.ZDashSpeed;
    	m_ZBloqueSpeed = m_Data.ZBloqueSpeed;
    	m_XMoveSpeed = m_Data.XMoveSpeed;
    	m_XDashSpeed = m_Data.XDashSpeed;
    	m_XBloqueSpeed = m_Data.XBloqueSpeed;
    	m_RecoveryTime = m_Data.RecoveryTime;
    	m_DashCost = m_Data.DashCost;
    	m_NormalAttackCost = m_Data.NormalAttackCost;
    	m_NormalAttackDamage = m_Data.NormalAttackDamage;
    	m_RangeAttackCost = m_Data.RangeAttackCost;
    	m_RangeAttackDamage = m_Data.RangeAttackDamage;
    	m_DoubleTapDelay = m_Data.DoubleTapDelay;
		m_Life = m_Data.LifeData;

		m_ActualSpeedX = m_XMoveSpeed;
		m_ActualSpeedZ = m_ZMoveSpeed;

        //Active seulement quand on bloque
		m_BloqueObject.SetActive(false);
		m_BloqueObject2.SetActive(false);
	}


	private void Update () 
	{
		//vecteur pour position de mon ennemie = position du transform de mon ennemie
		m_EnnemyPosition = m_Ennemy.transform.position;
		//Mon Player regarde toujours en direction de l'ennemi
		m_EnnemyPosition.y = transform.position.y;
        //Si je frappe Vertical je ne suis plus mon adversaire des yeux, il peut donc dasher 
        if (!m_IsVerticalAttack)
		{
			transform.LookAt(m_EnnemyPosition);
		}
        //Distance entre les 2 joueurs
		m_Distance = Vector3.Distance(m_EnnemyPosition, m_Player.transform.position);
        //Réduit la distance du dash relativement à la distance entre les 2 joueurs
		m_RelativeZDashSpeed = m_ZDashSpeed / m_Distance;
        //Si je n'ai plus de vie je meurt
        if (m_Life <= 0)
        {
            Destroy(gameObject);
        }
        //Bloque en haut
        if (Input.GetButton("Bloque_p" + m_PlayerID) && !m_CanBloqueHigh)
		{
        	m_CanBloqueLow = true;
			m_BloqueObject.SetActive(true);
			m_ActualSpeedX = m_XBloqueSpeed;
			m_ActualSpeedZ = m_ZBloqueSpeed;
            m_InputX = 0f;
            m_InputZ = 0f;


        }
        //Bloque en bas
		else if(Input.GetButton("Bloque2_p" + m_PlayerID) && !m_CanBloqueLow)
		{
        	m_CanBloqueHigh = true;
			m_BloqueObject2.SetActive(true);
			m_ActualSpeedX = m_XBloqueSpeed;
			m_ActualSpeedZ = m_ZBloqueSpeed;
            m_InputX = 0f;
            m_InputZ = 0f;
        }
        //attaque horizontal
		else if(Input.GetButtonDown("Fire_p" + m_PlayerID) && m_Life >= m_NormalAttackCost+1)
		{
			m_Life -= m_NormalAttackCost;
        	m_AttackAnimation.Play("Attack");
			m_Recovery += m_NormalAttackCost;
		}
        ///attaque vertical
		else if(Input.GetButtonDown("Fire3_p" + m_PlayerID) && m_Life >= m_NormalAttackCost+1)
		{
			m_IsVerticalAttack = true;
			m_Life -= m_NormalAttackCost;
        	m_AttackAnimation2.Play("Attack2");
			m_Recovery += m_NormalAttackCost;
		}
        //attaque à distance
		else if(Input.GetButtonDown("Fire2_p" + m_PlayerID) && m_Life >= m_RangeAttackCost+1)
		{    
        	Instantiate(m_AttackRangeBullet, m_AttackRangeSpawn.transform.position, m_AttackRangeSpawn.transform.rotation);
			m_Life -= m_NormalAttackCost;
			m_Recovery += m_RangeAttackCost;				
		}
        //Si je ne bloque pas
		else
		{
			m_CanBloqueLow = false;
			m_CanBloqueHigh = false;
			m_BloqueObject.SetActive(false);
			m_BloqueObject2.SetActive(false);
		}
		//Récupération de stamina/vie
		if(m_Recovery > 0)
		{
			m_Recovery -= 1 * (Time.deltaTime * m_RecoveryTime);
			m_Life += 1 * (Time.deltaTime * m_RecoveryTime);
		}
        //si je dash ou je bloque ne vérifie pas les input
		if(m_IsDashingHorizontal || m_IsDashingVertical || m_CanBloqueLow || m_CanBloqueHigh)
		{
			return;
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
        //tourner en un cercle parfait autours de l'adversaire
		transform.RotateAround(m_EnnemyPosition, (m_InputZ * Vector3.up), m_ActualSpeedZ * Time.deltaTime); 
    }

	//On vérifie l'Input en X, on donne sa valeur à m_InputX et on retourne vrai si il y a un input;
	private void CheckInputAxis()
	{
        //GetAxisRaw retourne toujours un chiffre précis : -1, 0 ou 1
		m_InputX = Input.GetAxisRaw("Horizontal_p" + m_PlayerID);
		m_InputZ = Input.GetAxisRaw("Vertical_p" + m_PlayerID);
	}

	private void CheckDoubletapX()
	{
        //Si j'ai double-tap et que j'ai l'énergie nécessaire je dash
		if(m_TapCountX >= 2 && m_Life >= m_DashCost+1)
		{
            StartCoroutine(DashingHorizontal());
			m_TapCountX = 0;
			m_CurrentDoubleTapTime = 0f;
		}
        //Si c'est mon premier tap ajoute 1 à TapCount
		if(Input.GetButtonDown("Horizontal_p" + m_PlayerID))
		{
			if(m_TapCountX == 0)
			{
				m_CurrentDoubleTapTime = 0f;
			}
			m_TapCountX++;
		}
        //Vérifie le délais en 2 tap, remet TapCount à 0 si le délais est dépassé.
		m_CurrentDoubleTapTime += Time.deltaTime;
		if(m_CurrentDoubleTapTime >= m_DoubleTapDelay)
		{
			m_TapCountX = 0;
			m_CurrentDoubleTapTime = 0f;
		}
	}

	private void CheckDoubletapZ()
	{
        //Si j'ai double-tap et que j'ai l'énergie nécessaire je dash
        if (m_TapCountZ >= 2 && m_Life >= m_DashCost+1)
		{
            StartCoroutine(DashingVertical());
			m_TapCountZ = 0;
			m_CurrentDoubleTapTime = 0f;
		}
        //Si c'est mon premier tap ajoute 1 à TapCount
        if (Input.GetButtonDown("Vertical_p" + m_PlayerID))
		{
			if(m_TapCountZ == 0)
			{
				m_CurrentDoubleTapTime = 0f;
			}
			m_TapCountZ++;
		}
        //Vérifie le délais en 2 tap, remet TapCount à 0 si le délais est dépassé.
        m_CurrentDoubleTapTime += Time.deltaTime;
		if(m_CurrentDoubleTapTime >= m_DoubleTapDelay)
		{
			m_TapCountZ = 0;
			m_CurrentDoubleTapTime = 0f;
		}
	}

	private void OnTriggerEnter(Collider aOther)
	{
		if (aOther.gameObject.layer == LayerMask.NameToLayer("Weapon"))
		{	
            //Si je bloque, j'ajoute le Damage que j'ai pris à mon recovery
			float Recovery = m_CanBloqueLow ? m_NormalAttackDamage : 0f;
            m_Life -= m_NormalAttackDamage;
			m_Recovery += Recovery;

            //Si je ne bloque pas, l'ennemie récupère d'un coup la totalité de son recovery accumulé
			if(!m_CanBloqueLow)
			{
				m_EnnemyController.m_Life += m_EnnemyController.m_Recovery;
				m_EnnemyController.m_Recovery = 0; 
			}
		}
        if (aOther.gameObject.layer == LayerMask.NameToLayer("WeaponVert"))
        {
            //Si je bloque, j'ajoute le Damage que j'ai pris à mon recovery 
            float Recovery = m_CanBloqueHigh ? m_NormalAttackDamage : 0f;
            m_Life -= m_NormalAttackDamage;
            m_Recovery += Recovery;
            
            //Si je ne bloque pas, l'ennemie récupère d'un coup la totalité de son recovery accumulé
            if (!m_CanBloqueHigh)
            {
                m_EnnemyController.m_Life += m_EnnemyController.m_Recovery;
                m_EnnemyController.m_Recovery = 0;
            }
        }

        if (aOther.gameObject.layer == LayerMask.NameToLayer("Bullet"))
		{	
            //Si je suis en position de bloque je reçoit la moitié des damages
			if (m_CanBloqueLow || m_CanBloqueHigh)
			{
				m_Life -= m_RangeAttackDamage/2;
			}
            //Sinon je reçoit tout les damages de l'attaque range
			else 
			{
				m_Life -= m_RangeAttackDamage;
			}
            //Détruit le projectile qui touche un joueur
			Destroy(aOther.gameObject);
		}

		if(aOther.gameObject.layer == LayerMask.NameToLayer("Boundary"))
		{
            //Tue le personnage si il tombe en dehors de l'arène
			Destroy(gameObject);
		}
	}
	
    //Pour parler à VertAttack:MonoBehaviour
	public void SetIsVerticalAttackFalse()
	{
        m_IsVerticalAttack = false;
	}

    //Déroulement du dash horizontal
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
    //Déroulement du dash vertical
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
}