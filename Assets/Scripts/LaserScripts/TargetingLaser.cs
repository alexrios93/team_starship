using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingLaser : MonoBehaviour {
    public GameObject _enemy;
    private float LaserSpeed = 10.0f;
    private float _enemyDistance;

    

    // Use this for initialization
    void Start () {
        //TargetingSystem ts = (TargetingSystem)GetComponent("TargetingSystem");
        //_enemy = ts.selectedTarget.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        _enemyDistance = Vector3.Distance(transform.position, _enemy.transform.position);
        
        if (_enemyDistance > 1)
        {
            transform.LookAt(_enemy.transform);
            transform.position = Vector3.Lerp(transform.position, _enemy.transform.position, Time.deltaTime * LaserSpeed);
        }
        else
        {
            transform.position = transform.position;
        }
    }
}
