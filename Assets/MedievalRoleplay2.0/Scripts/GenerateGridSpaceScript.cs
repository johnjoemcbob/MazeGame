using UnityEngine;
using System.Collections;

// Runs in the editor to create many objects quickly
// Matthew Cormack @johnjoemcbob
// 11/03/16 17:54

[ExecuteInEditMode]
public class GenerateGridSpaceScript : MonoBehaviour
{
	public bool Left = false;
	public bool Right = false;
	public bool Top = false;
	public bool Bottom = false;
	public GameObject[] Prefabs;

	// Flag for activation
	public bool Generate = false;
	public bool Clean = false;

	void Update()
	{
		if ( Generate || Clean )
		{
			foreach ( Transform trans in GetComponentsInChildren<Transform>() )
			{
				if ( trans && ( trans.gameObject != gameObject ) )
				{
					DestroyImmediate( trans.gameObject );
				}
			}
			GetComponent<Renderer>().enabled = true;
			Clean = false;
		}
		if ( ( Prefabs.Length > 0 ) && Generate )
		{
			GetComponent<Renderer>().enabled = false;
			AddSide( Left, 0 );
			AddSide( Right, 1 );
			AddSide( Top, 2 );
			AddSide( Bottom, 3 );

			Generate = false;
		}
	}

	void AddSide( bool side, int prefab )
	{
		if ( side )
		{
			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			GameObject gobj = (GameObject) Instantiate( Prefabs[prefab], position, rotation );
			gobj.transform.SetParent( transform );
		}
	}
}
