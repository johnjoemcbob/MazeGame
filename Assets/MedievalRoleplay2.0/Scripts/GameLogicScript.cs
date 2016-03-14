using UnityEngine;
using System.Collections;

// Game main logic
// Matthew Cormack @johnjoemcbob
// 13/03/16 18:29

public enum MazeScaleState
{
	None,
	Out,
	In
};

public class GameLogicScript : MonoBehaviour
{
	// Collection of levels to cycle through
	public GameObject[] Mazes;
	// Reference to the ball object
	public GameObject Ball;
	// Reference to the maze rotators
	public MazeOrientScript MazeOrient;
	public MazeGravityScript MazeGravity;
	// Reference to the camera buff
	public Transform CameraBuff;

	// The current level being played
	private int Maze = 0;
	// The current scale state of the maze
	private MazeScaleState ScaleState = MazeScaleState.None;

	void Update()
	{
		if ( ScaleState == MazeScaleState.Out )
		{
			CameraBuff.localScale = Vector3.Lerp( CameraBuff.lossyScale, new Vector3( 10, 10, 10 ), Time.deltaTime );
		}
		else if ( ScaleState == MazeScaleState.In )
		{
			CameraBuff.localScale = Vector3.Lerp( CameraBuff.lossyScale, new Vector3( 1, 1, 1 ), Time.deltaTime );
		}
	}

	public void CompleteMaze()
	{
		StartCoroutine( Complete( 1 ) );
	}

	IEnumerator Complete( float wait )
	{
		CameraControlScript cam = CameraBuff.GetComponent<CameraControlScript>();

		// Activate scale lerp
		ScaleState = MazeScaleState.Out;

		// START SPINNING
		cam.HorizontalRotateOverdrive = true;
		cam.Swipes = 3;

		// Wait for a while then change
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

		// Activate scale lerp
		ScaleState = MazeScaleState.In;

		// Stop spinning
		cam.CheckCancelOverdrive( 0 );
		cam.Swipes = 0;
	}
}
