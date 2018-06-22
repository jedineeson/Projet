using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertAttack : MonoBehaviour 
{
	[SerializeField]
	private PlayerController m_PlayerController;

    //Est appellé par un Animation Event
    public void SetIsVerticalAttackFalse()
    {
        m_PlayerController.SetIsVerticalAttackFalse();
    }
}
