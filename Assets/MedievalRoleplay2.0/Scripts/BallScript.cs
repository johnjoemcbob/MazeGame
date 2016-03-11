using UnityEngine;
using System.Collections;

// Logic for the main rolling ball object
// Matthew Cormack @johnjoemcbob
// 11/03/16 19:33

public class BallScript : MonoBehaviour
{
	private Vector3 Gravity = Vector3.zero;

	void Update()
	{
		GetComponent<Rigidbody>().AddForce( Gravity );
	}

	public void SetGravity( Vector3 gravity )
	{
		Gravity = gravity;
	}
	public void AddGravity( Vector3 gravity )
	{
		Gravity += gravity;
	}
}
