using UnityEngine;
using System.Collections;

// Triggers around the cube to change the gravity affecting the ball
// Matthew Cormack @johnjoemcbob
// 11/03/16 19:31

public class GravityTriggerScript : MonoBehaviour
{
	public Vector3 Gravity = Vector3.zero;
	public Transform MazeUpward;

	private bool Lerp = false;
	private GameObject Ball;

	void OnTriggerEnter( Collider collider )
	{
		if ( Lerp ) return;

        BallScript ball = collider.GetComponent<BallScript>();
		if ( ball )
		{
			//ball.AddGravity( Gravity );
			//ball.SetGravity( Gravity );
			ball.SetGravity( -Vector3.up );
		}

		// Reorient maze
		Lerp = true;
		Ball = ball.gameObject;
		transform.parent.parent.GetComponent<MazeGravityScript>().Change( MazeUpward, Ball );
	}

	void OnTriggerExit( Collider collider )
	{
		BallScript ball = collider.GetComponent<BallScript>();
		if ( ball )
		{
			//ball.AddGravity( -Gravity );
		}
		Lerp = false;
		Ball = null;
	}
}
