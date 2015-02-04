using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour {

	public string GameId = "HiveMind-v0.1";
	public HostData[] Hosts;
	public bool HostsReady = false;

	public void CreateServer (string name) {
		//MasterServer.ipAddress = "10.1.20.40";
		//MasterServer.port = 23466;
		//Network.natFacilitatorIP = "10.1.20.40";
		//Network.natFacilitatorPort = 50005;
		Network.InitializeServer(2, 19541, !Network.HavePublicAddress());
		MasterServer.RegisterHost(GameId, name);
	}

	public void RefreshHosts()
	{
		HostsReady = false;
		MasterServer.RequestHostList(GameId);
	}

	public void JoinServer(HostData host)
	{
		Network.Connect(host);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}

	void OnMasterServerEvent(MasterServerEvent evt)
	{
		if(evt == MasterServerEvent.HostListReceived)
		{
			Hosts = MasterServer.PollHostList();
			HostsReady = true;
			Debug.Log ("Host List Received");
		}
	}
}
