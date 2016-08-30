using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MotionSync : NetworkBehaviour {

	[SyncVar] private Vector3 syncPos;
	[SyncVar] private float syncRot;

	private Vector3 lastPos;
	private Quaternion lastRot;
	private Transform myTransform;
	private float lerpRate = 10;
	private float posThreshold = .5f;
	private float rotThreshold = 5;

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		TransmitMotion ();
		LerpMotion ();
	}

	void TransmitMotion()
	{
		if (!isServer) // not spawn
			return;

		if(Vector3.Distance (myTransform.position, lastPos) > posThreshold || Quaternion.Angle (myTransform.rotation, lastRot) > rotThreshold)
		{
			// tell server
			syncPos = myTransform.position;
			syncRot = myTransform.localEulerAngles.y;

			lastPos = myTransform.position;
			lastRot = myTransform.rotation;
		}
	}

	void LerpMotion()
	{
		if (isServer)
			return;

		myTransform.position = Vector3.Lerp (myTransform.position, syncPos, Time.deltaTime * lerpRate);
		Vector3 newRot = new Vector3 (0, syncRot, 0);
		myTransform.rotation = Quaternion.Lerp (myTransform.rotation, Quaternion.Euler (newRot), Time.deltaTime * lerpRate);
	}
}
