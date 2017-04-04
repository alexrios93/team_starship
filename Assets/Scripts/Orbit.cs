using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    public GameObject OrbitTarget;
    public float Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    //this makes ibjects orbit around orbit an target
	void Update () {
        transform.RotateAround(OrbitTarget.transform.position, Vector3.up, Speed);
	}
}
