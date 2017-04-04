using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowCursor : MonoBehaviour {

    public float depth = 10.0f;
    public float movementSpeed = 10.0f;

    //public Transform target;
    //public Vector3 posInWorld;
    // Vector3 posInScreen;


    // Use this for initialization
    void Start () {
        Cursor.visible = false;
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = depth;

        this.transform.position = Camera.main.ScreenToWorldPoint(mousePos); //ORIGINAL

        // NEW WORK IN PROGRESS

        //posInWorld = Camera.main.ScreenToWorldPoint(target.position);
        //posInWorld.x = Mathf.Clamp(mousePos.x, 22.8f, Screen.width);
        //posInWorld.y = Mathf.Clamp(mousePos.y, 350.0f, Screen.height);

        //posInScreen = Camera.main.ScreenToWorldPoint(posInWorld);

        //mousePos.x = Mathf.Clamp(mousePos.x, 22.8f, Screen.width);
        //mousePos.y = Mathf.Clamp(mousePos.y, 350.0f, Screen.height);

        //print("Position in world coordinates is " + posInWorld);

    } 

}
