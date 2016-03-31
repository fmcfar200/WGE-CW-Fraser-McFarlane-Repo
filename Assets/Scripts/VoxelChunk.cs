using UnityEngine;
using System.Collections;

public class VoxelChunk : MonoBehaviour {


	VoxelGenerator voxelGenerator;
	public int [, ,] terrainArray;
	int chunkSize = 16;

	//delegate signature
	public delegate void EventBlockChangedWithType(int blockType);
	//event instances for EventBlockChanged
	public static event EventBlockChangedWithType OnEventBlockChanged;
	public Transform playerTrans;
	GameObject dropCube;



	// Use this for initialization
	void Start () 
	{
		voxelGenerator = GetComponent<VoxelGenerator> ();
		terrainArray = new int[chunkSize, chunkSize, chunkSize];

		voxelGenerator.Initialise ();

		InitialiseTerrain ();
		CreateTerrain ();

		voxelGenerator.UpdateMesh ();

	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F1)) {
			//TEMP DISABLED SAVING
			//XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray,"VoxelChunk");
		}

		if (Input.GetKeyDown (KeyCode.F2)) {
			terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16,"VoxelChunk");
			CreateTerrain();
			voxelGenerator.UpdateMesh();


		}

	}

	public bool IsTraverable(Vector3 voxel)
	{
		//is block empty
		bool isEmpty = terrainArray [(int)voxel.x, (int)voxel.y, (int)voxel.z] == 0;
		//is block below stone
		bool isBelowStone = terrainArray [(int)voxel.x, (int)voxel.y-1, (int)voxel.z] == 3;
		return isEmpty && isBelowStone;

	}

	public int GetChunkSize()
	{
		return chunkSize;
	}


	public void SetBlock(Vector3 index , int blockType)
	{
		if((index.x > 0 && index.x < terrainArray.GetLength(0))
		   && (index.y > 0 && index.y < terrainArray.GetLength(1))
		   && (index.z > 0 && index.z < terrainArray.GetLength(2)))
		{
			//change block to the required type
			terrainArray [(int)index.x, (int)index.y, (int)index.z] = blockType;
			//create terrain
			CreateTerrain ();
			voxelGenerator.UpdateMesh ();
			OnEventBlockChanged(blockType);


		}
	}

	public void DropBlock(Vector3 index, int blockType)
	{
		dropCube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		dropCube.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		dropCube.transform.position = new Vector3(index.x+0.5f,index.y+0.5f,index.z+0.5f);
		dropCube.AddComponent<DroppedCubeScript> ();
	
	}

	void OnEnable()
	{
		PlayerScript.OnEventSetBlock += SetBlock;
		PlayerScript.OnEventDropBlock += DropBlock;

	}
	void OnDisable()
	{
		PlayerScript.OnEventSetBlock -= SetBlock;
		PlayerScript.OnEventDropBlock -= DropBlock;

	}


	void InitialiseTerrain()
	{

		for (int x = 0; x < terrainArray.GetLength(0); x++) 
		{
			for (int y = 0; y < terrainArray.GetLength(1);y++)
			{
				for (int z = 0; z < terrainArray.GetLength(2);z++)
				{
					if (y == 3)
					{
						terrainArray[x,y,z] = 1;
					}
					else if (y < 3)
					{
						terrainArray[x,y,z] = 2;
					}
					terrainArray[0, 3, 1] = 3;
					terrainArray[0, 3, 2] = 3;
					terrainArray[0, 3, 3] = 3;
					terrainArray[1, 3, 3] = 3;
					terrainArray[1, 3, 4] = 3;
					terrainArray[2, 3, 4] = 3;
					terrainArray[3, 3, 4] = 3;
					terrainArray[4, 3, 4] = 3;
					terrainArray[5, 3, 4] = 3;
					terrainArray[5, 3, 3] = 3;
					terrainArray[5, 3, 2] = 3;
					terrainArray[6, 3, 2] = 3;
					terrainArray[7, 3, 2] = 3;
					terrainArray[8, 3, 2] = 3;
					terrainArray[9, 3, 2] = 3;
					terrainArray[10, 3, 2] = 3;
					terrainArray[11, 3, 2] = 3;
					terrainArray[12, 3, 2] = 3;
					terrainArray[13, 3, 2] = 3;
					terrainArray[13, 3, 3] = 3;
					terrainArray[14, 3, 3] = 3;
					terrainArray[15, 3, 3] = 3;

				}
			}
		}
	}

	void CreateTerrain()
	{
		for (int x = 0; x < terrainArray.GetLength(0); x++) 
		{
			for (int y = 0; y < terrainArray.GetLength(1);y++)
			{
				for (int z = 0; z < terrainArray.GetLength(2);z++)
				{
					if (terrainArray[x,y,z] != 0)
					{
						string tex;

						switch(terrainArray[x,y,z])
						{
						case 1:
							tex = "Grass";
							break;
						case 2:
							tex = "Dirt";
							break;
						case 3:
							tex = "Stone";
							break;
						case 4:
							tex = "Sand";
							break;
						default:
							tex = "Grass";
							break;

						}
						//voxelGenerator.CreateVoxel(x,y,z,tex);


						if (x == 0||terrainArray[x - 1, y , z] == 0)
						{
							voxelGenerator.CreateNegativeXFace(x,y,z,tex);
						}

						if (x == terrainArray.GetLength(0)-1 || terrainArray[x+1,y,z] == 0)
						{
							voxelGenerator.CreatePositiveXFace(x,y,z,tex);
						}

						// check if we need to draw the negative y face
						if (y == 0 || terrainArray[x, y - 1, z] == 0)
						{
							voxelGenerator.CreateNegativeYFace(x, y, z, tex);
						}
						// check if we need to draw the positive y face
						if (y == terrainArray.GetLength(1) - 1 || terrainArray[x, y + 1, z] == 0)
						{
							voxelGenerator.CreatePositiveYFace(x,y,z,tex);
						} 

						// check if we need to draw the negative z face
						if (z == 0||terrainArray[x,y,z-1]==0)
						{
							voxelGenerator.CreateNegativeZFace(x, y, z, tex);
						}
						// check if we need to draw the positive z face
						if (z==terrainArray.GetLength(2)-1||terrainArray[x,y,z+1] == 0)
						{
							voxelGenerator.CreatePositiveZFace(x, y, z, tex);
						}

						//print("Create " + tex + " block,");
					}
					
				}
			}
		}
	}


	

}
