using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Logic for the maze being tilted and rotated to affect the ball
// Matthew Cormack @johnjoemcbob
// 11/03/16 23:46

public class MazeOrientScript : MonoBehaviour
{
	public float MoveMultiplier = 10;
	public float AndroidMultiplier = 10;
	public float MaxRotation = 10;
	public float ReturnMultiplier = 20;
	public float LerpSpeed = 5;
	public Transform CameraBuff;
	public bool DEBUG_AndroidTesting = false;

	private Vector3 PressedPos = Vector3.zero;
	private Vector3 TargetRotation = Vector3.zero;

	void Start()
	{
		if ( ( Application.platform == RuntimePlatform.Android ) || DEBUG_AndroidTesting )
		{
			Input.gyro.enabled = true;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}

	void Update()
	{
		if ( ( Application.platform == RuntimePlatform.Android ) || DEBUG_AndroidTesting )
		{
			// Convert gyro to new angles
			Vector3 gyro = Input.gyro.gravity;
			{
				gyro.y *= -1;
			}

			TargetRotation.z = ( PressedPos.x - gyro.x ) * MoveMultiplier * AndroidMultiplier * MaxRotation;
			TargetRotation.x = ( PressedPos.y - gyro.y ) * MoveMultiplier * AndroidMultiplier * MaxRotation;

			TargetRotation = CameraBuff.rotation * TargetRotation;
		}
		else
		{
			if ( Input.GetMouseButtonDown( 0 ) )
			{
				PressedPos.x = Input.mousePosition.x;
				PressedPos.y = Input.mousePosition.y;
			}
			else if ( Input.GetMouseButton( 0 ) )
			{
				// Affect differently depending on camera rotation
				// Mouse x and y to rotation x and z (or something)
				TargetRotation.z = ( ( PressedPos.x - Input.mousePosition.x ) / Screen.width * MoveMultiplier ) * MaxRotation;
				TargetRotation.x = -( ( PressedPos.y - Input.mousePosition.y ) / Screen.height * MoveMultiplier ) * MaxRotation;

				TargetRotation = CameraBuff.rotation * TargetRotation;
			}
			else
			{
				// Lerp the target slowly back when not holding mouse down
				TargetRotation += ( Vector3.zero - TargetRotation ) * Time.deltaTime * ReturnMultiplier;
			}
		}

		// Lerp the rotation of the maze
		Quaternion target = Quaternion.Euler( TargetRotation );
		transform.localRotation = Quaternion.Lerp( transform.localRotation, target, Time.deltaTime * LerpSpeed );
	}

	public void Reset()
	{
		TargetRotation = Vector3.zero;
		transform.localRotation = Quaternion.Euler( TargetRotation );
	}

	float ShortDistance( float current, float target )
	{
		return 180.0f - Mathf.Abs( ( Mathf.Abs( target - current ) % 360.0f ) - 180.0f );
	}
}
