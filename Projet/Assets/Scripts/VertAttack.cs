using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertAttack : MonoBehaviour 
{
	[SerializeField]
	private PlayerController m_PlayerController;

	public void SetIsVerticalAttackFalse()
	{
		m_PlayerController.m_IsVerticalAttack = false;
	}
}
