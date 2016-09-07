using UnityEngine;
using System.Collections;

public class RhinoController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("RhinoPath"), "time", 30f, "easetype", iTween.EaseType.linear,"looptype",iTween.LoopType.loop));
	}
	
	
}
