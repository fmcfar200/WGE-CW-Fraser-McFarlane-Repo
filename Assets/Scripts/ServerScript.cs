using UnityEngine;
using System.Collections;

public class ServerScript : MonoBehaviour {

	string typeName = "WGEFraserMcFarlaneGame";
	string gameName = "WGEFraserMcFarlaneRoom";
	HostData[] hostList;
	string ipAddress = "192.168.0.10";

	public GameObject voxelChunkPrefab;
	public GameObject networkCubePrefab;

	public Vector3 spawnPoint = new Vector3(8,8,8);


	// Use this for initialization
	void Start () 
	{
		MasterServer.ipAddress = ipAddress;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartServer()
	{
		if ((!Network.isServer) && (!Network.isClient)) 
		{
			Network.InitializeServer(4,25000,!Network.HavePublicAddress());
			MasterServer.RegisterHost(typeName,gameName);
			
		}
	}

	void OnServerInitialized()
	{
		GameObject startServerButton = GameObject.Find("StartServerButton");
		Debug.Log("Server Initializied");
		if (startServerButton != null) {
			startServerButton.SetActive (false);
		}
		Network.Instantiate(voxelChunkPrefab, Vector3.zero,
		                    Quaternion.identity, 0);
	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList = MasterServer.PollHostList();
			foreach (HostData hd in hostList)
			{
				if (hd.gameName == gameName)
				{
					Network.Connect(hd);
				}
			}
		}
	}
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		
		GameObject joinServerButton = GameObject.Find("JoinServerButton");
		
		if (joinServerButton != null) {
			joinServerButton.SetActive(false);
		}
		
		Network.Instantiate (networkCubePrefab, spawnPoint, Quaternion.identity, 0);
		
		
	}
}
