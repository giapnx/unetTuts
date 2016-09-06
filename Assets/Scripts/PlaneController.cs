using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneController : MonoBehaviour {

    void Start () {

        //iTween.MoveTo(gameObject, iTween.Hash("x", 3, "time", 4, "delay", 1, "onupdate", "myUpdateFunction", "looptype", iTween.LoopType.pingPong));
        iTween.MoveTo(gameObject, gameObject.transform.forward,10);
        
    }
	
	// Update is called once per frame
	void Update () {

        

    }
    
    
}
