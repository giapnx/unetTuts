using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneController : MonoBehaviour {
    
    void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path",iTweenPath.GetPath("PlanePath"),"time",30f,"looptype",iTween.LoopType.loop,"easetype",iTween.EaseType.linear));
        
    }
	
	
    
    
}
