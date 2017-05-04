using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShootForward : MonoBehaviour {

    public Rigidbody projectile;
    //public Rigidbody tracker;
    public float velocity = 10.0f;
    public bool status = false;

    public AudioClip laserSound;
    private AudioSource source;
    private float volLowRange = .2f;
    private float volHighRange = .3f;

    private GameObject _player;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Coordinates pause - play with manager object
        if (status)
        {
            if (Input.GetButtonDown("A Button") || Input.GetButtonDown("Fire1"))
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(laserSound, vol);

                Rigidbody newProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
                newProjectile.AddForce(transform.forward * velocity, ForceMode.VelocityChange);

                Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), _player.GetComponent<Collider>());
            }
            //if (Input.GetButtonDown("B Button"))
            //{
            //    Rigidbody newTracker = Instantiate(tracker, transform.position, transform.rotation) as Rigidbody;
            //    //newTracker.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
            //}
        }
    }
}
