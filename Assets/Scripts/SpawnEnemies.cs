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
    public GameObject Defender;

    private GameObject Camera; // Needed to add enemies to the list
    private GameObject Player;
    private VictoryOrDeath _enemyList;

    private float PortalOpenDelay = 30.0f;
    private float LastPortalOpen = -25.0f;

    private float SpawnDelay = 30.0f;
    private float LastSpawn = -20.0f;

    private float PortalCloseDelay = 30.0f;
    private float LastPortalClose = -15.0f;

    private float SmallDelay = 0.5f;
    private float LastSmallDelay = -15.0f;

    private float LastWaveStart = 0.0f; 

    private GameObject EnemyTarget;
    private GameObject EnemyTargetAlternative;

    public bool status = false;  // Allows for Pause & Play in ControlLayout

    public int WaveSize = 5;
    private int Count = 0;

    public static bool PortalIsOpen = false;

    // Use this for initialization
    void Start ()
    {
        Portal.GetComponent<ParticleSystem>().Stop();

        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Player = GameObject.FindGameObjectWithTag("Player");
        _enemyList = Camera.GetComponent<VictoryOrDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status)  // Needed for ControlLayout
        {
            if (PortalOpenDelay < (Time.time - LastPortalOpen))
            {
                OpenPortal();
                PortalIsOpen = true;
            }

            if (SpawnDelay < (Time.time - LastSpawn))
            {
                if (Count == 0)
                {
                    LastWaveStart = Time.time;
                }

                if ( (SmallDelay < (Time.time - LastSmallDelay)) & (Count < WaveSize) )
                {
                    LastSmallDelay = Time.time;
                    SpawnEnemyShips();
                }

                if (Count == WaveSize)
                {
                    LastSpawn = LastWaveStart;
                    Count = 0;
                }
            }

            if (PortalCloseDelay < (Time.time - LastPortalClose))
            {
                ClosePortal();
                PortalIsOpen = false;
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
                _enemyList.enemyList.Add(1); // Adds New Enemy to the List                

                //// DESIGNATE TARGETS ////
                EnemyShip.GetComponent<SeekAndDestroy>().Target = EnemyTarget;
                EnemyShip.GetComponent<SeekAndDestroy>().TargetAlternative = EnemyTargetAlternative;

                if( (Count > 3) & (MothershipHealth.MothershipBelowHalf) )
                {
                    RelativePos = Player.transform.position - transform.position;
                    GameObject DefenderShip = Instantiate(Defender, SpawnLocation.transform.position, Quaternion.LookRotation(RelativePos));
                    _enemyList.enemyList.Add(1); // Adds New Enemy to the List
                }

                Count++;
    }
    void ClosePortal()
    {
        LastPortalClose = Time.time;
        Portal.GetComponent<ParticleSystem>().Stop();
    }
}
