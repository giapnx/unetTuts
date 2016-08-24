using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Player_SyncRotation : NetworkBehaviour {

	[SyncVar (hook = "OnPlayerRotSynced")] private float syncPlayerRotation;
	[SyncVar (hook = "OnCamRotSynced")] private float syncCamRotation;

	[SerializeField] private Transform playerTrans;
	[SerializeField] private Transform camTrans;
	private float lerpRate = 15;

	private float lastPlayerRot;
	private float lastCamRot;
	private float threshold = 1;

	private List<float> syncPlayerRotList = new List<float> ();
	private List<float> syncCamRotList = new List<float> ();
	private float closeEnough = 0.3f;
	[SerializeField] private bool useHistoricalInterpolation;

	void Update()
	{
		LerpRotation ();
	}

	void FixedUpdate () {
		TransmitRotation ();

	}

	void LerpRotation()
	{
		if(!isLocalPlayer)
		{
			if(useHistoricalInterpolation)
			{
				HistoricalInterpolation ();
			}
			else
			{
				OrdinaryLerping ();
			}
//			playerTrans.rotation = Quaternion.Lerp (playerTrans.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
//			camTrans.rotation = Quaternion.Lerp (camTrans.rotation, syncCamRotation, Time.deltaTime * lerpRate);
		}
	}

	void HistoricalInterpolation()
	{
		if(syncPlayerRotList.Count > 0)
		{
			LerpPlayerRot (syncPlayerRotList[0]);

			if(Mathf.Abs (playerTrans.localEulerAngles.y - syncPlayerRotList[0]) < closeEnough)
			{
				syncPlayerRotList.RemoveAt (0);
			}
		}

		print (syncPlayerRotList.Count + "PlayerList Count");

		if(syncCamRotList.Count > 0)
		{
			LerpCamROt (syncCamRotList[0]);

			if(Mathf.Abs (camTrans.localEulerAngles.x - syncCamRotList[0]) < closeEnough)
			{
				syncCamRotList.RemoveAt (0);
			}
		}
		print (syncCamRotList.Count + "CamList Count");

	}

	void OrdinaryLerping()
	{
		LerpPlayerRot (syncPlayerRotation);
		LerpCamROt (syncCamRotation);
	}

	void LerpPlayerRot(float rotAngle)
	{
		Vector3 playerNewRot = new Vector3 (0, rotAngle, 0);
		playerTrans.rotation = Quaternion.Lerp (playerTrans.rotation, Quaternion.Euler (playerNewRot), lerpRate * Time.deltaTime);
	}

	void LerpCamROt(float rotAngle)
	{
		Vector3 camNewRot = new Vector3 (rotAngle, 0, 0);
		camTrans.localRotation = Quaternion.Lerp (camTrans.localRotation, Quaternion.Euler (camNewRot), lerpRate * Time.deltaTime);
	}

	[Command]
	void CmdProvideRotationToServer(float playerRot, float camRot)
	{
		syncPlayerRotation = playerRot;
		syncCamRotation = camRot;
	}
	[Client]
	void TransmitRotation()
	{
		if(isLocalPlayer)
		{
			if(CheckBeyondThreshold (playerTrans.localEulerAngles.y, lastPlayerRot) || CheckBeyondThreshold (camTrans.localEulerAngles.x, lastCamRot))
			{
				lastPlayerRot = playerTrans.localEulerAngles.y;
				lastCamRot = camTrans.localEulerAngles.x;
				CmdProvideRotationToServer (lastPlayerRot, lastCamRot);
			}
		}
	}

	bool CheckBeyondThreshold(float rot1, float rot2)
	{
		if (Mathf.Abs (rot1 - rot2) > threshold)
			return true;
		else
			return false;
	}

	[Client]
	void OnPlayerRotSynced(float latestPlayerRot)
	{
		syncPlayerRotation = latestPlayerRot;
		syncPlayerRotList.Add (syncPlayerRotation);
	}
	[Client]
	void OnCamRotSynced(float latestCamRot)
	{
		syncCamRotation = latestCamRot;
		syncCamRotList.Add (syncCamRotation );
	}
}
