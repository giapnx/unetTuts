using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneController : MonoBehaviour {

    void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path",iTweenPath.GetPath("PlanePath"),"time",50f));
        
    }
	
	
    
    
}
