using UnityEngine;
using System.Collections;
using System.Text;

public class HUD : MonoBehaviour
{
	public GUISkin resourceSkin;
	public GUISkin selectionSkin;
	public NetworkController networkController;

	private Player player;

	private const int RESOURCE_BAR_WIDTH = 320;
	private const int RESOURCE_BAR_HEIGHT = 120;
	private const int SELECTION_LIST_WIDTH = 320;
	private const int SELECTION_LIST_HEIGHT = 1024;

	void Start()
	{
		player = transform.root.GetComponent<Player>();
	}

	void OnGUI()
	{
		GUI.skin = resourceSkin;
		GUI.BeginGroup(new Rect(Screen.width/2 - RESOURCE_BAR_WIDTH/2, 0, RESOURCE_BAR_WIDTH, RESOURCE_BAR_HEIGHT));
		GUI.Label(new Rect(0, 0, RESOURCE_BAR_WIDTH, RESOURCE_BAR_HEIGHT), "RESOURCES");
		GUI.EndGroup();

		GUI.skin = selectionSkin;
		GUI.BeginGroup(new Rect(0, 0, SELECTION_LIST_WIDTH, SELECTION_LIST_HEIGHT));

		GUI.Label(new Rect(0, 0, SELECTION_LIST_WIDTH, 30), "SELECTED UNITS");

		Selector compie = player.GetComponent<Selector>();
		if(compie && compie.SelectedUnits.Count > 0) {
			StringBuilder buffer = new StringBuilder();

			foreach(var obj in compie.SelectedUnits) {
				buffer.Append(obj.transform.root.gameObject.name + "\n");
			}

			GUI.Label(new Rect(0, 30, SELECTION_LIST_WIDTH, SELECTION_LIST_HEIGHT - 30), buffer.ToString());
		} else
			GUI.Label(new Rect(0, 30, SELECTION_LIST_WIDTH, SELECTION_LIST_HEIGHT - 30), "NO CURRENT SELECTIONS");

		GUI.EndGroup();

		// Networking section
		GUI.BeginGroup(new Rect(Screen.width - 200, Screen.height - 100, 200, 100));

		if(networkController.HostsReady)
		{
			Vector2 ScrollViewVector = Vector2.zero;
			HostData[] hosts = networkController.Hosts;
			ScrollViewVector = GUI.BeginScrollView(new Rect(5, 5, 110, 90), ScrollViewVector, new Rect(0, 0, 110, 500*hosts.Length));

			for(int i = 0; i < hosts.Length; ++i)
			{
				if(GUI.Button(new Rect(5, 50*i + 5, 110, 40), hosts[i].gameName))
				{
					networkController.JoinServer(hosts[i]);
				}
			}

			GUI.EndScrollView();
		}

		if(GUI.Button(new Rect(135, 5, 60, 40), "Host"))
		{
			networkController.CreateServer("My New Server");
		}

		if(GUI.Button(new Rect(135, 55, 60, 40), "Refresh"))
		{
			networkController.RefreshHosts();
		}

		GUI.EndGroup();
	}
}
