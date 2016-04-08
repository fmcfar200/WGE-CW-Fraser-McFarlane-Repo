using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


	//public VoxelChunk voxelChunk;

	public delegate void EventSetBlockHandler(Vector3 v, int blockType);
	public static event EventSetBlockHandler OnEventSetBlock;
	public static event EventSetBlockHandler OnEventDropBlock;


	public Texture2D crossHairTexture;
	Rect crossHairPos;

	PlayerInventoryScript playerInv;

	public float overlapSphereRadius = 2.0f;



	// Use this for initialization
	void Start () {
		playerInv = this.gameObject.GetComponent<PlayerInventoryScript> ();


		crossHairPos =  new Rect((Screen.width - crossHairTexture.width) / 2, 
		                    (Screen.height - crossHairTexture.height) / 2,crossHairTexture.width,crossHairTexture.height);

		Screen.showCursor = false;
	}

	void Update()
	{	


		if (Input.GetButtonDown ("Fire1")) {
			Vector3 v;
			if (DigOrPlaceBlock (out v, 4,true)) 
			{
				OnEventSetBlock(v,0);

			}

		} 
		else if (Input.GetButtonDown ("Fire2")) 
		{
			Vector3 v;
			if (DigOrPlaceBlock(out v,4,false))
			{
				Debug.Log (v);
				if (playerInv.blockAmounts[playerInv.currentBlock-1] > 0)
				{
					OnEventSetBlock(v,playerInv.currentBlock);
					playerInv.blockAmounts[playerInv.currentBlock -1]--;
				}

			}

		}
	}

	void FixedUpdate()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, overlapSphereRadius,5);
		foreach (Collider c in colliders) {
			if (c.gameObject.tag == "DropCube") 
			{	
				c.transform.position = Vector3.MoveTowards (c.transform.position, this.transform.position, 2 * Time.deltaTime);
			}
		}
	}


	void OnGUI()
	{
		GUI.DrawTexture (crossHairPos, crossHairTexture);
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "DropCube") {
			Destroy(coll.gameObject);
		}
	}

	bool DigOrPlaceBlock(out Vector3 v, float dist, bool dig)
	{
		v = new Vector3 ();
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height/2, 0));
		
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, dist)) 
		{
			if (dig == true)
			{
				v = hit.point - hit.normal/2;
			

			}
			else
			{
				v = hit.point + hit.normal/2;
			}
			//offsets the hit point to the centre of the block hit

			//round down to the index of the block hit
			v.x = Mathf.Floor(v.x);
			v.y = Mathf.Floor(v.y);
			v.z = Mathf.Floor(v.z);

			return true;
		}
		return false;
	}
	
	bool PickThisBlock(out Vector3 v, float dist)
	{
		v = new Vector3 ();
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height/2, 0));

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, dist)) 
		{
			//offsets the hit point to the centre of the block hit
			v = hit.point - hit.normal/2;
			//round down to the index of the block hit
			v.x = Mathf.Floor(v.x);
			v.y = Mathf.Floor(v.y);
			v.z = Mathf.Floor(v.z);
			return true;
		}
		return false;
	}

	bool PickEmptyBlock(out Vector3 v, float dist)
	{
		v = new Vector3 ();
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height/2, 0));
		
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, dist)) 
		{
			//offsets the hit point to the neighbour
			v = hit.point + hit.normal/2;
			//round down to the index of the block hit
			v.x = Mathf.Floor(v.x);
			v.y = Mathf.Floor(v.y);
			v.z = Mathf.Floor(v.z);
			return true;
		}
		return false;
	}
}
