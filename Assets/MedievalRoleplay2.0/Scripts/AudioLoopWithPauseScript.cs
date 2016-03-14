using UnityEngine;
using System.Collections;

// Allows an AudioSource to be looped with a pause in between each play
// Matthew Cormack @johnjoemcbob
// 14/03/16 11:49

public class AudioLoopWithPauseScript : MonoBehaviour
{
	// The time to pause for
	public float PauseTime = 0.5f;

	// The current time at which the audio will be started again
	private float NextLoop = 0;

	void Start()
	{
		AudioSource audio = GetComponent<AudioSource>();
		if ( !audio )
		{
			Destroy( this );
		}
	}

	void Update()
	{
		AudioSource audio = GetComponent<AudioSource>();
		if ( audio && ( !audio.isPlaying ) )
		{
			if ( NextLoop == -1 )
			{
				NextLoop = Time.time + PauseTime;
			}
			else if ( NextLoop <= Time.time )
			{
				audio.Play();
				NextLoop = -1;
			}
		}
	}
}
