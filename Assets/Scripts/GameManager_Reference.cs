using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager_Reference : NetworkBehaviour {

	[SerializeField] GameObject playerPrefab;
	[SerializeField] GameObject playerSpawn;
	[SerializeField] GameObject joystick;
	[SerializeField] Camera mainCamera;

	[SerializeField] GameObject gvrRef;
	[SerializeField] GameObject mapRef;
	[SerializeField] GameObject backgroundRef;

	const string BODY = "MAX";

	private Vector3 camLocalPosition = new Vector3(0.02f, 0.8f, 0.15f);

	public override void  OnStartServer()
	{
		GameObject go = GameObject.Instantiate (playerPrefab, playerSpawn.transform.position, Quaternion.identity) as GameObject;
		mainCamera.transform.SetParent (go.transform);
		mainCamera.transform.localPosition = camLocalPosition;
		NetworkServer.Spawn (go);
	}

	public void Start()
	{
		if (!isServer)
		{
			joystick.SetActive (false);
			GameObject go = GameObject.FindWithTag ("Player");
			mainCamera.transform.SetParent (go.transform);
			mainCamera.transform.localPosition = camLocalPosition;
		}
		else
		{
			// display screen only control
			backgroundRef.SetActive (true);
			gvrRef.SetActive (false);
//			mapRef.SetActive (false);
			var bodyRef = GameObject.Find (BODY);
			bodyRef.SetActive (false);
			mainCamera.transform.gameObject.SetActive (false);
		}
	}
}
