using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Work in Progress, Alternative to ShipMovement script

public class FollowCursor : MonoBehaviour {

    public float depth = 10.0f;
    public float movementSpeed = 10.0f;


	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = depth;

        this.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        //Vector3 direction = new Vector3(horizontal, vertical, 0);
        //Vector3 finalDirection = new Vector3(horizontal, vertical, 5.0f);

        // Movement direction
        //transform.position += direction * movementSpeed * Time.deltaTime;

        // Aiming direction
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 50.0f);
    }
    /*
	void Update () {
        var mousePos = Input.mousePosition;

        float horizontal = mousePos.x;
        float vertical = mousePos.y;

        Vector3 direction = new Vector3(horizontal, vertical, 0);
        Vector3 finalDirection = new Vector3(horizontal, vertical, 5.0f);
        
        // Movement direction
        transform.position += direction * movementSpeed * Time.deltaTime;

        // Aiming direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 50.0f);           
	}
    */
}
