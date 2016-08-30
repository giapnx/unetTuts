using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager_Reference : NetworkBehaviour {

	[SerializeField] GameObject playerPrefab;
	[SerializeField] GameObject playerSpawn;
	[SerializeField] GameObject joystick;
	[SerializeField] Camera mainCamera;

	public override void  OnStartServer()
	{
		GameObject go = GameObject.Instantiate (playerPrefab, playerSpawn.transform.position, Quaternion.identity) as GameObject;
		mainCamera.transform.SetParent (go.transform);
		mainCamera.transform.localPosition = new Vector3 (0, 0.8f, 0);
		NetworkServer.Spawn (go);
	}

	public void Start()
	{
		if (!isServer)
		{
			joystick.SetActive (false);
		}
	}
}
