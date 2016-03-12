using UnityEngine;
using System.Collections;

// Triggers around the cube to change the gravity affecting the ball
// Matthew Cormack @johnjoemcbob
// 11/03/16 19:31

public class GravityTriggerScript : MonoBehaviour
{
	public Vector3 Gravity = Vector3.zero;
	public Vector3 Angle = Vector3.zero;
	private bool Lerp = false;

	void Update()
	{
		if ( Lerp )
		{
			Quaternion target = Quaternion.LookRotation( -transform.right, -Gravity );
			transform.parent.parent.rotation = Quaternion.Lerp( transform.parent.parent.rotation, target, Time.deltaTime );
			//transform.parent.parent.localEulerAngles = Vector3.Lerp( transform.parent.parent.localEulerAngles, Angle, Time.deltaTime * 10 );
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
    }

	void OnTriggerExit( Collider collider )
	{
		BallScript ball = collider.GetComponent<BallScript>();
		if ( ball )
		{
			//ball.AddGravity( -Gravity );
		}
		Lerp = true;
	}
}
