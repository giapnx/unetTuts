using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

[NetworkSettings (channel = 0, sendInterval = 0.1f)]
public class Player_SyncPosition : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPos;

	[SerializeField] Transform myTransform;
	private float lerpRate = 15;

	private Vector3 lastPos;
	private float threshold = 0.5f;

	void Update()
	{
		
	}

	void FixedUpdate () {
		TransmitPosition ();
		LerpPosition ();
	}

	void LerpPosition()
	{
		if(!isLocalPlayer)
		{
			myTransform.position = Vector3.Lerp (myTransform.position, syncPos, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer && Vector3.Distance (myTransform.position, lastPos) > threshold) {
			CmdProvidePositionToServer (myTransform.position);
			lastPos = myTransform.position;
		}
	}
}
