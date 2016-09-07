using UnityEngine;
using System.Collections;

public class RhinoController : MonoBehaviour {

    public float time;
    public GameObject pointForward;
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("RhinoPath"), "time", time, "easetype", iTween.EaseType.linear,"looptype",iTween.LoopType.loop));
	}
	void Update()
    {
        transform.LookAt(pointForward.transform);

    }
}
