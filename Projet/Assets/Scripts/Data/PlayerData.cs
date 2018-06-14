using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data", fileName = "New Data", order = 1)]
public class PlayerData : ScriptableObject
{
    
    [SerializeField]
    private float m_IsDashingTimer = 0.15f;
	public float IsDashingTimer
	{
		get{return m_IsDashingTimer;}
	}	

    //Vitesse maximum du déplacement du joueur
    [SerializeField]
    private float m_ZMoveSpeed = 50f;
	public float ZMoveSpeed
	{
		get{return m_ZMoveSpeed;}
	}	

    [SerializeField]
    private float m_ZDashSpeed = 800f;
	public float ZDashSpeed
	{
		get{return m_ZDashSpeed;}
	}	

    [SerializeField]
    private float m_ZBloqueSpeed = 25f;
	public float ZBloqueSpeed
	{
		get{return m_ZBloqueSpeed;}
	}

    //Vitesse maximum du déplacement du joueur
    [SerializeField]
    private float m_XMoveSpeed = 5f;
	public float XMoveSpeed
	{
		get{return m_XMoveSpeed;}
	}

    [SerializeField]
	private float m_XDashSpeed = 15f;
    public float XDashSpeed
	{
		get{return m_XDashSpeed;}
	}
	
	[SerializeField]
    private float m_XBloqueSpeed = 2.5f;
	public float XBloqueSpeed
	{
		get{return m_ZDashSpeed;}
	}
	
    //Vitesse qu'on récupère du stamina/vie
    [SerializeField]
    private float m_RecoveryTime = 4f;
	public float RecoveryTime
	{
		get{return m_RecoveryTime;}
	}


    //Stamina/Vie maximum
    [SerializeField]
	private float m_Life = 100f;
	public float Life
	{
		get{return m_Life;}
	}

    //Coût en stamina/vie d'un Dash
    [SerializeField]
    private float m_DashCost = 10f;
	public float DashCost
	{
		get{return m_DashCost;}
	}

    //Coût en stamina/vie d'une attack
    [SerializeField]
    private float m_NormalAttackCost = 10f;
		public float NormalAttackCost
	{
		get{return m_NormalAttackCost;}
	}

    [SerializeField]
    private float m_NormalAttackDamage = 20f;
	public float NormalAttackDamage
	{
		get{return m_NormalAttackDamage;}
	}

    [SerializeField]
    private float m_RangeAttackCost = 10f;
	public float RangeAttackCost
	{
		get{return m_RangeAttackCost;}
	}

    [SerializeField]
    private float m_RangeAttackDamage = 10f;
	public float RangeAttackDamage
	{
		get{return m_RangeAttackDamage;}
	}

    //Temps entre le double tap
    [SerializeField]
    private float m_DoubleTapDelay = 0.5f;
	public float DoubleTapDelay
	{
		get{return m_DoubleTapDelay;}
	}
}
