using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnEnemies : MonoBehaviour {
    public GameObject Enemy;
    public GameObject Target;
    public GameObject TargetAlternative;
    public GameObject SpawnLocation;
    public GameObject Portal;

    

    private float PortalOpenDelay = 30.0f;
    private float LastPortalOpen = -25.0f;

    private float SpawnDelay = 30.0f;
    private float LastSpawn = -20.0f;

    private float PortalCloseDelay = 30.0f;
    private float LastPortalClose = -15.0f;

    private GameObject EnemyTarget;
    private GameObject EnemyTargetAlternative;

    public bool status = false;  // Allows for Pause & Play in ControlLayout

    public int WaveSize = 5;
    private int Count = 0;

    // Use this for initialization
    void Start ()
    {
        Portal.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (status)  // Needed for ControlLayout
        {
            if (PortalOpenDelay < (Time.time - LastPortalOpen))
            {
                OpenPortal();
            }

            if (SpawnDelay < (Time.time - LastSpawn))
            {
                SpawnEnemyShips();
            }

            if (PortalCloseDelay < (Time.time - LastPortalClose))
            {
                ClosePortal();
            }
        }
    }

    void OpenPortal()
    {
        LastPortalOpen = Time.time;
        Portal.GetComponent<ParticleSystem>().Play();
    }

    void SpawnEnemyShips()
    {
        while (Count < WaveSize)
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

            Count++;
        }

        Count = 0;
    }
    void ClosePortal()
    {
        LastPortalClose = Time.time;
        Portal.GetComponent<ParticleSystem>().Stop();
    }
}
