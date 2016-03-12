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
			TargetRotation.z = ( ( PressedPos.x - Input.mousePosition.x ) / Screen.width * MoveMultiplier ) * MaxRotation;
			TargetRotation.x = ( ( PressedPos.y - Input.mousePosition.y ) / Screen.height * MoveMultiplier ) * MaxRotation;
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
}
