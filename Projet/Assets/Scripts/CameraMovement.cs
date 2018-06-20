using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public GameObject m_Player1;
	public GameObject m_Player2;
	public GameObject m_CenterPoint;
	private float m_DistanceBetweenPlayer;

	private Vector3 m_Direction;
	private Vector3 m_PointVector;
	
	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		m_PointVector = (m_Player1.transform.position + m_Player2.transform.position) / 2;
		m_PointVector.y = m_Player2.transform.position.y;
		
		m_DistanceBetweenPlayer = Vector3.Distance(m_Player1.transform.position, m_Player2.transform.position);

		m_CenterPoint.transform.position = m_PointVector;

		m_CenterPoint.transform.right = m_Player1.transform.forward;			
	}
}
