using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour {
    public GameObject _player;
    public float distance;

    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + distance, _player.transform.position.z);
    }
}
