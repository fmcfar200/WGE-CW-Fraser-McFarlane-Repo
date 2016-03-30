using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class DroppedCubeScript : MonoBehaviour {

	Vector3 popForce = new Vector3(Random.Range(50.0f,120f),200.0f,50.0f);


	// Use this for initialization
	void Start () 
	{
		this.gameObject.GetComponent<Rigidbody> ().AddForce (popForce);
		this.gameObject.tag = "DropCube";
		this.gameObject.layer = 9;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

	}

}
