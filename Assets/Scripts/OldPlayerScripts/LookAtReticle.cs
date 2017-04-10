using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtReticle : MonoBehaviour {

    public Transform target;
    //var target : Transform;

	// Update is called once per frame
	void Update () {
        transform.LookAt(target);		
	}
}
