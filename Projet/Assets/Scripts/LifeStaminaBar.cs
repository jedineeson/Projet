using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeStaminaBar : MonoBehaviour
{
	public Image m_LifeBar1;
	public Image m_LifeBar2;
	private PlayerController m_Player1;	
	private PlayerController m_Player2;	
	private float m_Life1;
	private float m_Life2;
	private float m_MaxLife = 100;

	private void Awake()
	{
		m_Player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
		m_Player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>();
	}
	void Start () 
	{

	}
	
	void Update () 
	{
		m_Life1 = m_Player1.Life;
		m_LifeBar1.fillAmount = m_Life1 / m_MaxLife;
		m_Life2 = m_Player2.Life;
		m_LifeBar2.fillAmount = m_Life2 / m_MaxLife;
	}
}
