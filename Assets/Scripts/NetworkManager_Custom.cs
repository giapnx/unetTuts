using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

public class NetworkManager_Custom : NetworkManager {

	public void StartupHost()
	{
		NetworkManager.singleton.networkAddress = Network.player.ipAddress;
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame()
	{
//		var ipList = Dns.GetHostAddresses (Dns.GetHostName ());
		print (Network.player.ipAddress);
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}

	void SetIPAddress()
	{
		string ipAddress = GameObject.Find ("IPAddressInput").transform.FindChild ("Text").GetComponent <Text> ().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	public void ScanHost()
	{
		IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

		foreach (IPAddress ip in host.AddressList) {
			print (ip.ToString ());
			if (ip.AddressFamily == AddressFamily.InterNetwork) {
				print ("Rip");
			}
		}
	}

	void OnLevelWasLoaded(int level)
	{
		print ("Loaded " + level);
		if(level == 0)
		{
			StartCoroutine (SetupMenuSceneButtons ());
		}
		else
		{
			SetupOtherSceneButtons ();
		}
	}

	IEnumerator SetupMenuSceneButtons()
	{
		yield return new WaitForSeconds (0.03f);
		var startupHostBtn = GameObject.Find ("StartupHostBtn").GetComponent <Button>();
		startupHostBtn.onClick.RemoveAllListeners ();
		startupHostBtn.onClick.AddListener (StartupHost);

		var joinGameBtn = GameObject.Find ("JoinGameBtn").GetComponent <Button>();
		joinGameBtn.onClick.RemoveAllListeners ();
		joinGameBtn.onClick.AddListener (JoinGame);

	}

	void SetupOtherSceneButtons()
	{
		var disconnectBtn = GameObject.Find ("DisconnectBtn").GetComponent <Button>();
		disconnectBtn.onClick.RemoveAllListeners ();
//		disconnectBtn.onClick.AddListener (NetworkManager.singleton.StopHost);
	}
}
