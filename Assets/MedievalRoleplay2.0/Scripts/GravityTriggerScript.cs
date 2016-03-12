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

	void Update()
	{
		if ( Lerp )
		{
			Quaternion target = MazeUpward.rotation;
			transform.parent.parent.rotation = Quaternion.Lerp( transform.parent.parent.rotation, target, Time.deltaTime * 5 );

			// Resume when rotated
			float dist = Quaternion.Angle( transform.parent.parent.rotation, target );
			if ( dist < 10 )
			{
				Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
				Ball.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}

	void OnTriggerEnter( Collider collider )
	{
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

		// Pause while reorientating
		Ball.GetComponent<Rigidbody>().isKinematic = true;
	}

	void OnTriggerExit( Collider collider )
	{
		BallScript ball = collider.GetComponent<BallScript>();
		if ( ball )
		{
			//ball.AddGravity( -Gravity );
		}
		Lerp = false;
	}
}
