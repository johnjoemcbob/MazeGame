using UnityEngine;
using System.Collections;

// Activated by triggers surrounding the maze to rotate the size the ball is on to the top
// Matthew Cormack @johnjoemcbob
// 13/03/16 12:50

public class MazeGravityScript : MonoBehaviour
{
	public Transform Default;

	private Transform MazeUpward;
	private GameObject Ball;

	void Update()
	{
		if ( !MazeUpward ) return;

		Quaternion target = MazeUpward.rotation;

		// Resume when rotated
		float dist = Quaternion.Angle( transform.rotation, target );
		if ( dist < 1 )
		{
		}
		else if ( dist < 10 )
		{
			//Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			//Ball.GetComponent<Rigidbody>().isKinematic = false;
			Ball.GetComponent<Rigidbody>().velocity /= 5000 * Time.deltaTime;
		}
		else
		{
			// Pause while reorientating
			//Ball.GetComponent<Rigidbody>().isKinematic = true;
			Ball.GetComponent<Rigidbody>().velocity /= 10000 * Time.deltaTime;

			// Reset tilting while reorientating
			transform.parent.GetComponent<MazeOrientScript>().Reset();
		}

		transform.rotation = Quaternion.Lerp( transform.rotation, target, Time.deltaTime * 5 );
	}

	public void Change( Transform up, GameObject ball )
	{
		if ( up == MazeUpward ) return;

		MazeUpward = up;
		Ball = ball;

		GetComponent<AudioSource>().Play();
	}
}
