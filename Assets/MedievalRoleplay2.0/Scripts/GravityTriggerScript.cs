using UnityEngine;
using System.Collections;

// Triggers around the cube to change the gravity affecting the ball
// Matthew Cormack @johnjoemcbob
// 11/03/16 19:31

public class GravityTriggerScript : MonoBehaviour
{
	public Vector3 Gravity = Vector3.zero;

	void OnTriggerEnter( Collider collider )
	{
		BallScript ball = collider.GetComponent<BallScript>();
		if ( ball )
		{
			//ball.AddGravity( Gravity );
			ball.SetGravity( Gravity );
		}
	}

	void OnTriggerExit( Collider collider )
	{
		BallScript ball = collider.GetComponent<BallScript>();
		if ( ball )
		{
			//ball.AddGravity( -Gravity );
		}
	}
}
