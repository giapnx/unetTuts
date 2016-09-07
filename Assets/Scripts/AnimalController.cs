using UnityEngine;
using System.Collections;

public class AnimalController : MonoBehaviour {

    public float time;
    public GameObject pointForward;
    public string path;
    void Start()
    {
        path=GetComponent<iTweenPath>().pathName;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(path), "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
    }
    void Update()
    {
        transform.LookAt(pointForward.transform);

    }
}
