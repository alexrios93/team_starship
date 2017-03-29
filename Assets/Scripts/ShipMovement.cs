using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    public Transform target;

    public float movementSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0);
        Vector3 finalDirection = new Vector3(horizontal, vertical, 5.0f);
        
        // Movement direction
        transform.position += direction * movementSpeed * Time.deltaTime;

        // Aiming direction
        // Rotate towards reticle using keyboard
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 50.0f);

        //Rotate towards reticle using mouse cursor
        //transform.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 50.0f);
    }
}
