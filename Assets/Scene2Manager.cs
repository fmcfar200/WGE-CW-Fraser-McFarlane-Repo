using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;

public class Scene2Manager : MonoBehaviour {

	public GameObject voxelChunkObj;
	public InputField inputField;
	VoxelGenerator voxelGen;
	VoxelChunk voxChunkScript;

	public GameObject startPanel;
	public Button startLerpButton;
	public Button switchMapButton;

	public Vector3 startPos;
	public Vector3 endPos;
	Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);

	bool startLerp = false;
	bool traversing = false;
	bool chunkLoaded = false;

	GameObject cube;

	// Use this for initialization
	void Start () {
		voxelGen = voxelChunkObj.GetComponent<VoxelGenerator> ();
		voxChunkScript = voxelChunkObj.GetComponent<VoxelChunk> ();
		startPanel.SetActive (true);
		startLerpButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (chunkLoaded == true) 
		{
			startLerpButton.gameObject.SetActive (true);
			switchMapButton.gameObject.SetActive(true);
			chunkLoaded = false;
		}

		if (startLerp)
		{

		}
	
	}

	public void LoadChunkAndPositions()
	{
		voxChunkScript.terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile (16, inputField.text.ToString ());
		voxChunkScript.CreateTerrain ();
		voxelGen.UpdateMesh ();
		startPanel.SetActive (false);
		chunkLoaded = true;
		GetStartAndEndPos ();
	}



	void GetStartAndEndPos()
	{
		XmlReader xmlReader = XmlReader.Create(inputField.text.ToString() + ".xml");
		while (xmlReader.Read()) 
		{
			if (xmlReader.IsStartElement ("start")) 
			{
				float x = float.Parse (xmlReader ["x"]);
				float y = float.Parse (xmlReader ["y"]);
				float z = float.Parse (xmlReader ["z"]);
				xmlReader.Read ();
				startPos = new Vector3 (x, y, z);

			} 
			else if (xmlReader.IsStartElement ("end")) 
			{
				int x = int.Parse (xmlReader ["x"]);
				int y = int.Parse (xmlReader ["y"]);
				int z = int.Parse (xmlReader ["z"]);
				xmlReader.Read ();
				endPos = new Vector3 (x, y, z);


			
			}
		}

	}

}
