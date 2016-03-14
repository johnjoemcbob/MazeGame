using UnityEngine;
using System.Collections;

// Swiping Input
// Matthew Cormack @johnjoemcbob
// 11/03/16 23:05

public enum SwipeType
{
	Left,
	Right,
	Up,
	Down
};

public class SwipeScript : MonoBehaviour
{
	[Header( "Swiper" )]
	// Distance of input drag before considered a swipe
	public float DistanceThreshold = 1;

    private Vector2 PressedPos = Vector3.zero;
	private float PressedTime = 0;

	public virtual void Update()
	{
		// Pressed
		if ( Input.GetMouseButtonDown( 1 ) )
		{
			LogPress( Input.mousePosition );
		}
		// Released
		if ( Input.GetMouseButtonUp( 1 ) )
		{
			CheckForSwipe( Input.mousePosition );
        }
    }

	void LogPress( Vector2 pos )
	{
		PressedPos = pos;
		PressedTime = Time.time;
    }

	void CheckForSwipe( Vector2 pos )
	{
		Vector2 dif = pos - PressedPos;
		float dist = Vector3.Distance( pos, PressedPos ) / Mathf.Max( 1, Time.time - PressedTime );
		if ( dist > DistanceThreshold )
		{
			// Horizontal right
			if ( dif.x > 0 )
			{
				OnSwipe( SwipeType.Right );
			}
			// Horizontal left
			if ( dif.x < 0 )
			{
				OnSwipe( SwipeType.Left );
			}
			// Vertical up
			if ( dif.y > 0 )
			{
				OnSwipe( SwipeType.Up );
			}
			// Vertical down
			if ( dif.y < 0 )
			{
				OnSwipe( SwipeType.Down );
			}
		}
	}

	virtual protected void OnSwipe( SwipeType type )
	{
		print( type );
	}
}
