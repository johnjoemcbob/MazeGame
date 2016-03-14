using UnityEngine;
using System.Collections;

// Game main logic
// Matthew Cormack @johnjoemcbob
// 13/03/16 18:29

public class GameLogicScript : MonoBehaviour
{
	// Collection of levels to cycle through
	public GameObject[] Mazes;
	// Reference to the ball object
	public GameObject Ball;
	// Reference to the maze rotators
	public MazeOrientScript MazeOrient;
	public MazeGravityScript MazeGravity;

	// The current level being played
	private int Maze = 0;

	void Update()
	{

	}

	public void CompleteMaze()
	{
		StartCoroutine( Complete( 1 ) );
	}

	IEnumerator Complete( float wait )
	{
		yield return new WaitForSeconds( wait );

		// Disable the old
		Mazes[Maze].SetActive( false );

		// Enable the new
		Maze = Mathf.Min( Maze + 1, Mazes.Length - 1 );
		Mazes[Maze].SetActive( true );

		// Reset maze rotation, tilt
		MazeOrient.Reset();
		MazeGravity.Change( MazeGravity.Default, Ball );

		// Reset ball
		Ball.transform.localPosition = Vector3.zero;
		Ball.transform.localRotation = Quaternion.identity;
		Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}
