using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenMovement : MonoBehaviour {

    public float speed = 1.0f;
    public float distance = 1.0f;
    public Transform startObject;
    public Transform endObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position += transform.forward * speed * Time.deltaTime;

        if(Vector3.Distance(startObject.position, endObject.position) <= distance)
        {
            transform.position = new Vector3(0, -25, -25);
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

	}
}