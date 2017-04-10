using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCameraScript : MonoBehaviour {

    public Transform target;
    public Vector3 distance = new Vector3 (0.0f, 2.0f, -7.0f);

    public float positionDamping = 2.0f;
    public float rotateDamping = 2.0f;

    private Transform thisTransform;
	// Use this for initialization
	void Start () {
        thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null)
        {
            return;
        }

        // Controls Camera Position
        Vector3 wantedPosition = target.position + (target.rotation * distance);

        Vector3 currentPosition = Vector3.Lerp(thisTransform.position, wantedPosition, positionDamping * Time.deltaTime);

        thisTransform.position = currentPosition;

        //Controls Camera Rotation
        Quaternion wantedRotation = Quaternion.LookRotation(target.position - thisTransform.position, target.up);
        thisTransform.rotation = wantedRotation;

	}
}
