using UnityEngine;
using System.Collections;

// Draw glow objects ontop of all others in the scene
// Matthew Cormack @johnjoemcbob
// 12/03/16 19:33

public class GlowLayerScript : MonoBehaviour
{
	public Texture Texture_Glow;

	void OnGUI()
	{
		GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), Texture_Glow, ScaleMode.ScaleToFit );
	}
}
