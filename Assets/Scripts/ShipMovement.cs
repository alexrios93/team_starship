using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    public Transform target;

    //public float movementSpeed = 10.0f; // Original
    public Vector2 movementSpeed = Vector2.one;
    public float angleChangeSpeed = 50.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed.x;
        float vertical = Input.GetAxis("Vertical") * movementSpeed.y;

        Vector3 direction = new Vector3(horizontal, vertical, 0);
        Vector3 finalDirection = new Vector3(horizontal, vertical, 1.0f); //Original 5.0f
        
        // Movement direction
        transform.position += direction * Time.deltaTime;

        // Aiming direction
        // Rotate towards reticle using keyboard
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * angleChangeSpeed);

        //Rotate towards reticle using mouse cursor
        //transform.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * angleChangeSpeed);
    }
}
