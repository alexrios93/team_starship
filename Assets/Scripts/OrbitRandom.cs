using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrbitRandom : MonoBehaviour {
    public GameObject OrbitTarget;
    float Speed = 4;
    int DirectionX = 0;
    int DirectionY = 0;
    bool X = false;
    bool Y = false;
    float radius  = 20.0f;
    // Use this for initialization
    Transform T;

    void Start()
    {
        if(UnityEngine.Random.Range(0, 2) == 1)
        {
            DirectionX = 1;
            DirectionY = UnityEngine.Random.Range(0, 2);
        }
        else
        {
            DirectionY = 1;
            DirectionX = UnityEngine.Random.Range(0, 2);
        }
    }

    // Update is called once per frame
    //this makes ibjects orbit around orbit an target
    void Update()
    {
        transform.RotateAround(OrbitTarget.transform.position, Vector3.up, (Speed * DirectionX));
        transform.RotateAround(OrbitTarget.transform.position, Vector3.right, (Speed * DirectionY));

        //transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        var desiredPosition = (transform.position - OrbitTarget.transform.position).normalized * radius + OrbitTarget.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Speed);
    }
}
