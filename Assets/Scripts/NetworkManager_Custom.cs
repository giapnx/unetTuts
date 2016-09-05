using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class NetworkManager_Custom : NetworkManager {

	UdpClient sender;
	int remotePort = 19784;

	void SendData ()
	{
		string customMessage = "Haki"+" * "+Network.player.ipAddress+" * "+"UnetTuts";

		if (customMessage != "") {
			sender.Send (Encoding.ASCII.GetBytes (customMessage), customMessage.Length);
		}
	}

	UdpClient receiver;

	public void StartReceivingIP ()
	{
		try {
			if (receiver == null) {
				receiver = new UdpClient (remotePort);
				receiver.BeginReceive (new AsyncCallback (ReceiveData), null);
			}
		} catch (SocketException e) {
			Debug.Log (e.Message);
		}
	}

	private void ReceiveData (IAsyncResult result)
	{
		var receiveIPGroup = new IPEndPoint (IPAddress.Any, remotePort);
		byte[] received;
		if (receiver != null) {
			received = receiver.EndReceive (result, ref receiveIPGroup);
		} else {
			return;
		}
		receiver.BeginReceive (new AsyncCallback (ReceiveData), null);
		string receivedString = Encoding.ASCII.GetString (received);
		print ("receivedString: "+receivedString);
	}

	public void StartupHost()
	{
		NetworkManager.singleton.networkAddress = Network.player.ipAddress;
		SetPort ();
		NetworkManager.singleton.StartHost ();

		sender = new UdpClient (NetworkManager.singleton.networkPort, AddressFamily.InterNetwork);
		IPEndPoint groupEP = new IPEndPoint (IPAddress.Broadcast, remotePort);
		sender.Connect (groupEP);

		//SendData ();
		InvokeRepeating("SendData",0,5f);
	}

	public void JoinGame()
	{
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
		StartReceivingIP ();

		IPAddress[] host = Dns.GetHostAddresses("");

		foreach (IPAddress ip in host) {
			print (ip.ToString ());
			if (ip.AddressFamily == AddressFamily.InterNetwork) {
				print (ip.ToString ());
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
		yield return new WaitForSeconds (0.05f);
		var startupHostBtn = GameObject.Find ("StartupHostBtn").GetComponent <Button>();
		startupHostBtn.onClick.RemoveAllListeners ();
		startupHostBtn.onClick.AddListener (StartupHost);

		var joinGameBtn = GameObject.Find ("JoinGameBtn").GetComponent <Button>();
		joinGameBtn.onClick.RemoveAllListeners ();
		joinGameBtn.onClick.AddListener (JoinGame);

		var scanHostBtn = GameObject.Find ("ScanHost").GetComponent <Button>();
		scanHostBtn.onClick.RemoveAllListeners ();
		scanHostBtn.onClick.AddListener (ScanHost);

	}

	void SetupOtherSceneButtons()
	{
		var disconnectBtn = GameObject.Find ("DisconnectBtn").GetComponent <Button>();
		disconnectBtn.onClick.RemoveAllListeners ();
		disconnectBtn.onClick.AddListener (NetworkManager.singleton.StopHost);

		var ipText = GameObject.Find ("ip address").GetComponent <Text> ();
		string ipAddress = NetworkManager.singleton.networkAddress;
		ipAddress = (ipAddress == "localhost") ? Network.player.ipAddress : ipAddress;
		ipText.text = "ip: " + ipAddress;
	}
}
