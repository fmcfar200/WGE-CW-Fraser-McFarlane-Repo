using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


	//public VoxelChunk voxelChunk;

	public delegate void EventSetBlockHandler(Vector3 v, int blockType, int destroyBlockType);
	public static event EventSetBlockHandler OnEventSetBlock;
	public static event EventSetBlockHandler OnEventDropBlock;


	public Texture2D crossHairTexture;
	Rect crossHairPos;

	VoxelChunk vox;
	PlayerInventoryScript playerInv;
	public float overlapSphereRadius = 2.0f;

	public int destroyedBlockTerrain;



	// Use this for initialization
	void Start () {
		vox = GetComponent<VoxelChunk> ();
		playerInv = this.gameObject.GetComponent<PlayerInventoryScript> ();


		crossHairPos =  new Rect((Screen.width - crossHairTexture.width) / 2, 
		                    (Screen.height - crossHairTexture.height) / 2,crossHairTexture.width,crossHairTexture.height);

		Screen.showCursor = false;
	}

	void Update()
	{	


		if (Input.GetButtonDown ("Fire1")) {
			Vector3 v;
			if (DigOrPlaceBlock (out v, 4, true)) {
				OnEventSetBlock (v, 0,destroyedBlockTerrain);

			}

		} else if (Input.GetButtonDown ("Fire2")) {
			Vector3 v;
			if (DigOrPlaceBlock (out v, 4, false)) 
			{
			
				Debug.Log (v);
				if (playerInv.blockAmounts [playerInv.currentBlock - 1] > 0) 
				{
					OnEventSetBlock (v, playerInv.currentBlock,0);
					playerInv.blockAmounts [playerInv.currentBlock - 1]--;
					playerInv.UpdateInventory();
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
			string cubeType = coll.gameObject.GetComponent<Renderer>().material.mainTexture.name;
			if (cubeType == "GrassSprite")
			{
				playerInv.blockAmounts[0]++;
			}
			else if (cubeType == "DirtSprite")
			{
				playerInv.blockAmounts[1]++;
			}
			else if (cubeType == "StoneSprite")
			{
				playerInv.blockAmounts[2]++;
			}
			else
			{
				playerInv.blockAmounts[3]++;
			}
			Destroy(coll.gameObject);
			playerInv.UpdateInventory();
		}
	}

	bool DigOrPlaceBlock(out Vector3 v, float dist, bool dig)
	{
		v = new Vector3 ();
		if (playerInv.invOpen == false) 
		{
			if (GetComponent<PauseScript>().paused == false)
			{
			Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));

			RaycastHit hit;
		
			if (Physics.Raycast (ray, out hit, dist)) 
				{
				if (dig == true) {
					v = hit.point - hit.normal / 2;
					Debug.Log (hit.transform.GetComponent<VoxelChunk> ().terrainArray [(int)v.x, (int)v.y, (int)v.z]);
					destroyedBlockTerrain = hit.transform.GetComponent<VoxelChunk> ().terrainArray [(int)v.x, (int)v.y, (int)v.z];
				} else {
					v = hit.point + hit.normal / 2;
				}
				//offsets the hit point to the centre of the block hit

				//round down to the index of the block hit
				v.x = Mathf.Floor (v.x);
				v.y = Mathf.Floor (v.y);
				v.z = Mathf.Floor (v.z);


				return true;
				}
			}
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
