using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendMothership : MonoBehaviour {
    public int MoveSpeed = 4;
    public int MaxDist = 100;
    public int MinDist = 10;
   

    private GameObject _player;
    public Rigidbody projectile;
    private bool fire = false;
    private float velocity = 100.0f;

    private float LastLaserFire = -2.5f;
    private float LaserCoolDown = 2.5f;

    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(_player.transform.position);

        if (Vector3.Distance(transform.position, _player.transform.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _player.transform.position) <= MaxDist)
            {
                LaserFire();
            }
        }
    }

    void LaserFire()
    {
        if (LaserCoolDown < (Time.time - LastLaserFire))
        {
            LastLaserFire = Time.time;            
            Rigidbody newProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            newProjectile.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
        }
    }
}