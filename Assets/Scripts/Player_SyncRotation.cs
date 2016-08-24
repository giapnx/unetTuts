using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Player_SyncRotation : NetworkBehaviour {

	[SyncVar] private Quaternion syncPlayerRotation;
	[SyncVar] private Quaternion syncCamRotation;

	[SerializeField] private Transform playerTrans;
	[SerializeField] private Transform camTrans;
	private float lerpRate = 15;

	private Quaternion lastPlayerRot;
	private Quaternion lastCamRot;
	private float threshold = 5;

	void FixedUpdate () {
		TransmitRotation ();
		LerpRotation ();
	}

	void LerpRotation()
	{
		if(!isLocalPlayer)
		{
			playerTrans.rotation = Quaternion.Lerp (playerTrans.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
			camTrans.rotation = Quaternion.Lerp (camTrans.rotation, syncCamRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotationToServer(Quaternion playerRot, Quaternion camRot)
	{
		syncPlayerRotation = playerRot;
		syncCamRotation = camRot;
		print ("Command angle called");
	}
	[Client]
	void TransmitRotation()
	{
		if(isLocalPlayer)
		{
			if(Quaternion.Angle (playerTrans.rotation, lastPlayerRot) > threshold || Quaternion.Angle (camTrans.rotation, lastCamRot) > threshold)
			{
				CmdProvideRotationToServer (playerTrans.rotation, camTrans.rotation);
				lastPlayerRot = playerTrans.rotation;
				lastCamRot = camTrans.rotation;
			}
		}
	}
}
