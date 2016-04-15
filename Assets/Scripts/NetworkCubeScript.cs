using UnityEngine;
using System.Collections;

public class NetworkCubeScript : MonoBehaviour {


	float speed = 10f;
	float jumpForce = 20f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.W))
		{
			rigidbody.MovePosition(rigidbody.position + Vector3.forward * speed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			rigidbody.MovePosition(rigidbody.position - Vector3.forward * speed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			rigidbody.MovePosition(rigidbody.position + Vector3.right * speed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.A))
		{
			rigidbody.MovePosition(rigidbody.position - Vector3.right * speed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.Space))
		{
			rigidbody.AddForce(Vector3.up * jumpForce);
		}
	}
}
