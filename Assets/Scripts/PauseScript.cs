using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

	public bool paused;
	public GameObject pausePanel;
	public GameObject loadPanel;
	public GameObject savePanel;

	public InputField loadInputField;
	public InputField saveInputField;
	VoxelGenerator voxelGenerator;
	VoxelChunk voxelChunk;

	PlayerInventoryScript playerInv;

	// Use this for initialization 
	void Start () {
		playerInv = GetComponent<PlayerInventoryScript> ();
	
		voxelGenerator = GameObject.Find ("Voxel_Object").GetComponent<VoxelGenerator> ();
		voxelChunk = GameObject.Find ("Voxel_Object").GetComponent<VoxelChunk> ();

		if (voxelGenerator == null) {
			Debug.LogError ("VOXEL GEN NOT FOUND");
		} else if (voxelChunk == null) {
			Debug.LogError ("VOXEL CHUNK NOT FOUND");
		} else {
			Debug.Log("chunk and generator found");
		}

		paused = false;
		pausePanel.SetActive (false);
		loadPanel.SetActive (false);
		savePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (paused == false)
			{
				paused = true;
			}
			else
			{
				paused = false;
			}
		}

		if (paused) {
			Time.timeScale = 0;
			pausePanel.SetActive (true);

		}
		else 
		{
			playerInv.mainCamera.enabled = true;
			Time.timeScale = 1;
			pausePanel.SetActive (false);

		}

		 if (paused || playerInv.invOpen) {
			Screen.showCursor = true;
			playerInv.mainCamera.enabled = false;

		} else {
			Screen.showCursor = false;
			playerInv.mainCamera.enabled = true;

		}
	}

	public void LoadChunk()
	{
		voxelChunk.terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile (16, loadInputField.text.ToString ());
		voxelChunk.CreateTerrain ();
		voxelGenerator.UpdateMesh ();
		paused = false;
	}

	public void SaveCunk()
	{
		XMLVoxelFileWriter.SaveChunkToXMLFile(voxelChunk.terrainArray,saveInputField.text.ToString());
	}
}
