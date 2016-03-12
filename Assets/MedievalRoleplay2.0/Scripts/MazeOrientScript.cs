using UnityEngine;
using System.Collections;

// Logic for the maze being tilted and rotated to affect the ball
// Matthew Cormack @johnjoemcbob
// 11/03/16 23:46

public class MazeOrientScript : MonoBehaviour
{
	public float MoveMultiplier = 10;
	public float MaxRotation = 10;
	public float ReturnMultiplier = 20;
	public Transform CameraBuff;

	private Vector2 PressedPos = Vector3.zero;
	private Vector3 TargetRotation = Vector3.zero;

	void Update()
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
			GetTargetFromCamera();
		}
		else
		{
			// Lerp the target slowly back when not holding mouse down
			TargetRotation += ( Vector3.zero - TargetRotation ) * Time.deltaTime * ReturnMultiplier;
        }

		// Lerp the rotation of the maze
		Quaternion target = Quaternion.Euler( TargetRotation );
		transform.localRotation = Quaternion.Lerp( transform.localRotation, target, Time.deltaTime );
	}

	public void Reset()
	{
		TargetRotation = Vector3.zero;
		transform.localRotation = Quaternion.Euler( TargetRotation );
	}

	void GetTargetFromCamera()
	{
		TargetRotation.z = ( ( PressedPos.x - Input.mousePosition.x ) / Screen.width * MoveMultiplier ) * MaxRotation;
		TargetRotation.x = -( ( PressedPos.y - Input.mousePosition.y ) / Screen.height * MoveMultiplier ) * MaxRotation;

		int angle = 0;
		{
			float yaw = CameraBuff.localEulerAngles.y;
			float[] possibleang = new float[] { 0, 90, 180, 270 };
			float dist = -1;
			for ( int ang = 0; ang < 4; ang++ )
			{
				float curdist = ShortDistance( yaw, possibleang[ang] );
				if ( ( dist == -1 ) || ( curdist < dist ) )
				{
					angle = ang;
					dist = curdist;
				}
			}
		}
		switch ( angle )
		{
			case 0:
				break;
			case 1:
				break;
			case 2:
				TargetRotation.x *= -1;
				TargetRotation.z *= -1;
				break;
			case 3:
				TargetRotation.x *= -1;
				TargetRotation.z *= -1;
				break;
			default:
				break;
		}
	}

	float ShortDistance( float current, float target )
	{
		return 180.0f - Mathf.Abs( ( Mathf.Abs( target - current ) % 360.0f ) - 180.0f );
	}
}
