using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour {

    public float playerSpeed = 1.0f;

    public Transform target;

    Vector3 direction;
    public float epsilon = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Original
        //transform.position += transform.forward * playerSpeed * Time.deltaTime;

        //Test
        direction = (target.position - transform.position).normalized;
        if((transform.position - target.position).magnitude > epsilon)
            transform.Translate(direction * playerSpeed * Time.deltaTime);
		
	}
}
