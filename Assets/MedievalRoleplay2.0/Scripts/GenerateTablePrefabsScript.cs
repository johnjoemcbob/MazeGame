using UnityEngine;
using System.Collections;

// Runs in the editor to create many objects quickly
// Matthew Cormack @johnjoemcbob
// 11/03/16 17:21

[ExecuteInEditMode]
public class GenerateTablePrefabsScript : MonoBehaviour
{
	public Vector3 DistanceRow = new Vector3( 1, 0, 0 );
	public Vector3 DistanceCol = new Vector3( 0, 0, 1 );
	public Vector2 Rows = new Vector2( 0, 2 );
	public Vector2 Columns = new Vector2( 0, 2 );
	public GameObject Prefab;

	// Flag for activation
	public bool Generate = false;

	void Update()
	{
		if ( Prefab && Generate )
		{
			for ( float row = Rows.x; row < Rows.y; row++ )
			{
				for ( float col = Columns.x; col < Columns.y; col++ )
				{
					Vector3 position = ( row * DistanceRow ) + ( col * DistanceCol );
					Quaternion rotation = transform.rotation;
					GameObject gobj = (GameObject) Instantiate( Prefab, position, rotation );
					gobj.transform.SetParent( transform );

					Generate = false;
				}
			}
		}
	}
}
