using UnityEngine;
using System.Collections;

public class ElephantController : MonoBehaviour {
    
	void Start () {
        iTween.MoveTo(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z+300),100f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
