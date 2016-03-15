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
	// Cameras in the scene controlled by this object
	public Transform[] Cameras;

	// Target yaw angle
	private float HorizontalRotateTarget = 0;
	// Direction to rotate yaw angle
	private float HorizontalRotateDirection = 1;
	// The flags to rotate constantly (when going into 'overdrive' and continuously spinning)
	public bool HorizontalRotateOverdrive = false;
	private bool HorizontalRotateOverdriveCancel = false;
	// The time for which to continue in overdrive
	private float HorizontalRotateOverdriveTime = 0;
	// The number of swipes counting for overdrive
	public int Swipes = 0;

	public override void Update()
	{
		base.Update();

		// Lerp rotation
		float dir = HorizontalRotateDirection;
		float dist = 0;
		float speed = 0;
		Vector3 ang = transform.localEulerAngles;
        {
			dist = ShortDistance( transform.localEulerAngles.y, HorizontalRotateTarget );
			//dist = Mathf.Abs( HorizontalRotateTarget - transform.localEulerAngles.y );
			{
				// While in 'overdrive' don't stop spinning until clicked/touched
				if ( HorizontalRotateOverdrive )
				{
					dist = 300 + ( 50 * Mathf.Min( 2, Swipes ) );
				}
			}
			speed = dist * dir * LerpSpeed;
            ang.y += speed * Time.deltaTime;
		}
		transform.localEulerAngles = ang;

		// Lerp camera distance from maze by rotation speed
		foreach ( Transform cam in Cameras )
		{
			cam.localPosition = Vector3.Lerp( cam.localPosition, new Vector3( 0, 0, -Mathf.Abs( speed ) * 0.016f ), Time.deltaTime * 10 );
		}

		// Update audio while in overdrive
		if ( HorizontalRotateOverdrive )
		{
			float pitch = transform.GetChild( 1 ).GetComponent<AudioSource>().pitch;
			float target = 0.5f + ( 0.1f * Mathf.Min( 2, Swipes ) );
			//transform.GetChild( 1 ).GetComponent<AudioSource>().pitch = pitch + ( ( target - pitch ) * Time.deltaTime * 10 );

			HorizontalRotateOverdriveTime = Mathf.Max( 0, HorizontalRotateOverdriveTime - Time.deltaTime );
			if ( HorizontalRotateOverdriveTime == 0 )
			{
				CheckCancelOverdrive( 0 );
			}
		}
	}

	override protected void OnSwipe( SwipeType type )
	{
		switch ( type )
		{
			case SwipeType.Left:
				HorizontalRotateTarget -= HorizontalRotateAngle;
				CheckCancelOverdrive( -1 );
				HorizontalRotateDirection = -1;
				CheckOverdrive( -1 );
				GetComponents<AudioSource>()[0].pitch = 0.9f;
				break;
			case SwipeType.Right:
				HorizontalRotateTarget += HorizontalRotateAngle;
				CheckCancelOverdrive( 1 );
				HorizontalRotateDirection = 1;
				CheckOverdrive( 1 );
				GetComponents<AudioSource>()[0].pitch = 1.1f;
                break;
			default:
				break;
		}

		// Play spin swoosh
		foreach ( AudioSource audio in GetComponents<AudioSource>() )
		{
			audio.Play();
			//if ( HorizontalRotateOverdrive )
			{
				break;
			}
		}
    }

	public void CheckCancelOverdrive( int dir )
	{
		if ( dir != HorizontalRotateDirection )
		{
			if ( HorizontalRotateOverdrive )
			{
				// dir = 0 is the flag to always cancel
				if ( dir == 0 )
				{
					dir = (int) HorizontalRotateDirection;
				}

				HorizontalRotateOverdrive = false;
				HorizontalRotateOverdriveCancel = true;

				// Return to closest angle
				int[] Possible = new int[] { 0, 90, 180, 270, 360 };
				float dist = 0;
				int closest = -1;
				for ( int poss = 0; poss < Possible.Length; poss++ )
				{
					float possdist = ShortDistance( transform.localEulerAngles.y, Possible[poss] );
					if (
						( ( closest == -1 ) || ( possdist < dist ) ) && // Shortest Distance
						(
							( ( dir < 0 ) && ( Possible[poss] <= transform.localEulerAngles.y ) ) ||
							( ( dir > 0 ) && ( Possible[poss] >= transform.localEulerAngles.y ) )
						) // Must also be in the right direction
					)
					{
						dist = possdist;
						closest = poss;
					}
				}
				closest = Mathf.Max( 0, closest );
				HorizontalRotateTarget = Possible[closest];

				// Stop audio
				transform.GetChild( 0 ).GetComponent<AudioLoopWithPauseScript>().enabled = false;
				transform.GetChild( 1 ).GetComponent<AudioSource>().Stop();
			}
        }
	}

	void CheckOverdrive( int dir )
	{
		// Add to swipes
		StartCoroutine( WaitAndRemoveSwipe( dir, 1 ) );
		HorizontalRotateOverdriveTime += 2;

		// Go into overdrive if overshot (player is spinning really fast)
		if ( HorizontalRotateOverdriveCancel )
		{
			HorizontalRotateOverdriveCancel = false;
		}
		else if ( Swipes > 3 )
		{
			HorizontalRotateOverdrive = true;
			Swipes = 0;

			// Begin looping turn audio
			transform.GetChild( 0 ).GetComponent<AudioLoopWithPauseScript>().enabled = true;
			//transform.GetChild( 1 ).GetComponent<AudioSource>().Play();
        }
	}

    bool TurnDirection( float current, float target )
	{
		return ( target - current + 360 ) % 360 > 180;
    }

	float ShortDistance( float current, float target )
	{
		return 180.0f - Mathf.Abs( ( Mathf.Abs( target - current ) % 360.0f ) - 180.0f );
	}

	IEnumerator WaitAndRemoveSwipe( int dir, float wait )
	{
		Swipes++;

		yield return new WaitForSeconds( wait );

		if ( dir == HorizontalRotateDirection )
		{
			Swipes--;
			Swipes = Mathf.Max( 0, Swipes );
		}
	}
}
