using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Shoot : NetworkBehaviour {

	private int damage = 15;
	private float range = 200;
	[SerializeField] private Transform camTransform;
	private RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfShooting ();
	}

	void CheckIfShooting()
	{
		if (!isLocalPlayer)
			return;

		if(Input.GetKeyDown (KeyCode.Return))
		{
			Shoot ();
		}
	}

	void Shoot()
	{
		if(Physics.Raycast (camTransform.TransformPoint (0,0,0.5f), camTransform.forward, out hit,range))
		{
			if(hit.transform.tag == "Player")
			{
				string uIdentity = hit.transform.name;
				print (hit.transform.name);
				CmdTellServerWhoWasShot (uIdentity, damage);
			}
		}
	}

	[Command]
	void CmdTellServerWhoWasShot(string uniqueID, int damage)
	{
		GameObject go = GameObject.Find (uniqueID);
		// Apply damage to that player.
		go.GetComponent <Player_Health> ().DeductHealth (damage);
	}
}
