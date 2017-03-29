using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    } 

}
