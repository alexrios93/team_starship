using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootForward : MonoBehaviour {

    public Rigidbody projectile;
    public float velocity = 10.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody newProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            newProjectile.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
        }
    }
}
