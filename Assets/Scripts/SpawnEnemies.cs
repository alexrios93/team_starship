using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnEnemies : MonoBehaviour {
    public GameObject Enemy;
    public GameObject Target;
    public GameObject TargetAlternative;
    public GameObject SpawnLocation;
    
    private float SpawnDelay = 5.0f;
    private float LastSpawn = 0.0f;

    private GameObject EnemyTarget;
    private GameObject EnemyTargetAlternative;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SpawnDelay < (Time.time - LastSpawn))
        {
            LastSpawn = Time.time;
            //// SELECT TARGET ////
            if ((UnityEngine.Random.Range(0, 2) == 0) & (Target != null))
            {
                EnemyTarget = Target;
                EnemyTargetAlternative = TargetAlternative;
            }
            else if (TargetAlternative != null)
            {
                EnemyTarget = TargetAlternative;
                EnemyTargetAlternative = Target;
            }
            else
            {
                return;
            }

            //// SPAWN ENEMY UNIT ////
            Vector3 RelativePos = Target.transform.position - transform.position;
            GameObject EnemyShip = Instantiate(Enemy, SpawnLocation.transform.position, Quaternion.LookRotation(RelativePos));

            //// DESIGNATE TARGETS ////
            EnemyShip.GetComponent<SeekAndDestroy>().Target = EnemyTarget;
            EnemyShip.GetComponent<SeekAndDestroy>().TargetAlternative = EnemyTargetAlternative;
        }   
    }
}
