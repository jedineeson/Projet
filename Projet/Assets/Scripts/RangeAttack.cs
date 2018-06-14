using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour 
{
	public Rigidbody m_RigidBody;
    public float m_Speed;

    private void Start ()
    {
        m_RigidBody.velocity = transform.forward * m_Speed;
    }


	private void OnTriggerEnter(Collider aOther)
	{
        if(aOther.gameObject.layer == LayerMask.NameToLayer("Boundary"))
	    {
	    	Destroy(gameObject);
    	}
    }

}
