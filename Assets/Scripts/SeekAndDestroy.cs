using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : MonoBehaviour {
    public GameObject Target;
    public GameObject TargetAlternative;
    private float MaxSeekSpeed = 10.0f;
    private float MaxOrbitSpeed = 43.0f;
    private float TargetDistance;
    private float MinOrbitDistance = 75.0f;
    private float MaxOrbitDistance = 125.0f;
    private float OrbitDistance = 100.0f;
    //private float RotationSpeed = 10.0f;

    public Rigidbody projectile;
    public float velocity = 10.0f;
    private float LastLaserFire = -10.0f;
    private float LaserCoolDown = 10.0f;
    
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        ////CHECK IF BOTH TARGETS ARE DESTROYED//
        if ((Target == null) & (TargetAlternative == null))
        {
            ////VICTORY SCREECH REEEEEEEEEEEEEEEE////
        }
        else
        {
            ////SWITCH TARGET IF CURRENT IS DESTROYED////
            if (Target == null)
            {
                Target = TargetAlternative;
            }

            ////CHECK TARGET DISTANCE////
            TargetDistance = Vector3.Distance(transform.position, Target.transform.position);

            ////SEEK////
            if (TargetDistance > MaxOrbitDistance)
            {
                transform.LookAt(Target.transform);
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, MaxSeekSpeed);
            }

            ////DESTROY////
            if ((MinOrbitDistance < TargetDistance) & (TargetDistance < MaxOrbitDistance))
            {
                ////ORBIT TARGET////
                transform.RotateAround(Target.transform.position, Vector3.up, MaxOrbitSpeed * Time.deltaTime);
                var desiredPosition = (transform.position - Target.transform.position).normalized * OrbitDistance + Target.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * MaxOrbitSpeed);

                ////FIRE LASERS////
                if (LaserCoolDown < (Time.time - LastLaserFire))
                {
                    LastLaserFire = Time.time;
                    transform.LookAt(Target.transform);
                    Rigidbody newProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
                    newProjectile.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
                    newProjectile.tag = "EnemyLaser";
                }
            }
        }
    }
}
