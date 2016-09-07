using UnityEngine;
using System.Collections;

public class PointForwardController : MonoBehaviour {

    public float time;
    public string path;
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(path), "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
    }
}
