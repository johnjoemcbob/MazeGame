using UnityEngine;
using System.Collections;

public class LookAtTestScript : MonoBehaviour
{
	public GameObject Obj;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Quaternion target = Quaternion.LookRotation( ( Obj.transform.position - transform.position ).normalized );
		transform.rotation = target;
	}
}
