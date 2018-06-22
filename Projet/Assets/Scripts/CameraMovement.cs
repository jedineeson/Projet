using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
	private GameObject m_Player1;
    [SerializeField]
    private GameObject m_Player2;
    [SerializeField]
    private GameObject m_CenterPoint;

	private Vector3 m_Direction;
	private Vector3 m_PointVector;
	
	private void Update () 
	{
        //Calcul pour trouver le point milieu de 2 objets.
		m_PointVector = (m_Player1.transform.position + m_Player2.transform.position) / 2;
		m_PointVector.y = m_Player2.transform.position.y;
		
		m_CenterPoint.transform.position = m_PointVector;

		m_CenterPoint.transform.right = m_Player1.transform.forward;			
	}
}
