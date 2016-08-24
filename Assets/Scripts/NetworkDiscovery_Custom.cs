using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkDiscovery_Custom : NetworkDiscovery
{
	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		print ("IP: " + fromAddress);
		NetworkManager.singleton.networkAddress = fromAddress;
		NetworkManager.singleton.StartClient();
	}

}