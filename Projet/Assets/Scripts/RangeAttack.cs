using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]
	private Rigidbody m_RigidBody;
    [SerializeField]
    private float m_Speed;

    private void Start ()
    {
        m_RigidBody.velocity = transform.forward * m_Speed;
    }

	private void OnTriggerEnter(Collider aOther)
	{
        //détruit ma balle si elle entre en collision avec les frontières de mon terrain(box collider)
        if(aOther.gameObject.layer == LayerMask.NameToLayer("Boundary"))
	    {
	    	Destroy(gameObject);
    	}
    }
}
