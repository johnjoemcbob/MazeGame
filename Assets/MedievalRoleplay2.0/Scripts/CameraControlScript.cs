using UnityEngine;
using System.Collections;

// Control of the camera with mouse & touch
// Matthew Cormack @johnjoemcbob
// 11/03/16 23:14

public class CameraControlScript : SwipeScript
{
	[Header( "Camera Control" )]
	// Speed of the camera rotation lerp
	public float LerpSpeed = 1;
	// Angle to rotate by on the yaw
	public float HorizontalRotateAngle = 90;

	// Target yaw angle
	private float HorizontalRotateTarget = 0;
	// Direction to rotate yaw angle
	private float HorizontalRotateDirection = 1;

	public override void Update()
	{
		base.Update();

		// Lerp rotation
		float dir = HorizontalRotateDirection;
		Vector3 ang = transform.localEulerAngles;
		{
			float dist = ShortDistance( transform.localEulerAngles.y, HorizontalRotateTarget );
            ang.y += Time.deltaTime * dist * dir * LerpSpeed;
		}
		transform.localEulerAngles = ang;
	}

	override protected void OnSwipe( SwipeType type )
	{
		switch ( type )
		{
			case SwipeType.Left:
				HorizontalRotateTarget -= HorizontalRotateAngle;
				HorizontalRotateDirection = -1;
				break;
			case SwipeType.Right:
				HorizontalRotateTarget += HorizontalRotateAngle;
				HorizontalRotateDirection = 1;
				break;
			default:
				break;
		}

		GetComponent<AudioSource>().Play();
    }

	bool TurnDirection( float current, float target )
	{
		return ( target - current + 360 ) % 360 > 180;
    }

	float ShortDistance( float current, float target )
	{
		return 180.0f - Mathf.Abs( ( Mathf.Abs( target - current ) % 360.0f ) - 180.0f );
    }
}
