using UnityEngine;
using System.Collections;

// Maze win trigger
// Matthew Cormack @johnjoemcbob
// 13/03/16 13:08

public class MazeHoleScript : MonoBehaviour
{
	// Reference to the round logic script
	public GameObject ScriptTemp;

	void OnTriggerEnter( Collider other )
	{
		if ( other.gameObject.name == "Ball" )
		{
			// Send message to round logic about winning


			// Play ball fall audio
			GetComponent<AudioSource>().Play();
		}
	}
}
