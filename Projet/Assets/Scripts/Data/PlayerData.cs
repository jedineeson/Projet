using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data", fileName = "New Data", order = 1)]
public class PlayerData : ScriptableObject
{
    
    [SerializeField]
    private float m_IsDashingTimer = 0.15f;
    //Vitesse maximum du déplacement du joueur
    [SerializeField]
    private float m_ZMoveSpeed = 50f;
	[SerializeField]
    private float m_ZDashSpeed = 800f;
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
    [SerializeField]
	private float m_DataLife = 100f;
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

	public float IsDashingTimer
	{
		get{return m_IsDashingTimer;}
	}	

	public float ZMoveSpeed
	{
		get{return m_ZMoveSpeed;}
	}	

	public float ZDashSpeed
	{
		get{return m_ZDashSpeed;}
	}	

	public float ZBloqueSpeed
	{
		get{return m_ZBloqueSpeed;}
	}

	public float XMoveSpeed
	{
		get{return m_XMoveSpeed;}
	}

    public float XDashSpeed
	{
		get{return m_XDashSpeed;}
	}	
	
	public float XBloqueSpeed
	{
		get{return m_XBloqueSpeed;}
	}	

	public float RecoveryTime
	{
		get{return m_RecoveryTime;}
	}

	public float LifeData
	{
		get{return m_DataLife;}
	}

	public float DashCost
	{
		get{return m_DashCost;}
	}

	public float NormalAttackCost
	{
		get{return m_NormalAttackCost;}
	}

	public float NormalAttackDamage
	{
		get{return m_NormalAttackDamage;}
	}

	public float RangeAttackCost
	{
		get{return m_RangeAttackCost;}
	}

	public float RangeAttackDamage
	{
		get{return m_RangeAttackDamage;}
	}

	public float DoubleTapDelay
	{
		get{return m_DoubleTapDelay;}
	}
}
