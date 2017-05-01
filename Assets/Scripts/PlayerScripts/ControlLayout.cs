using UnityEngine;
using System.Collections;

public class ControlLayout : MonoBehaviour {

	private bool _run = false;
	private PlayerControl _playerControl;

    private GameObject[] Blasters;
    ShootForward[] _shootForward;

    // public GameObject[] Enemies;
    // SeekAndDestroy[] _seekAndDestroy;

    void Start(){
		_playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        Blasters = GameObject.FindGameObjectsWithTag("Blaster");
        _shootForward = new ShootForward[Blasters.Length];

        // Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // _seekAndDestroy = new SeekAndDestroy[Enemies.Length];
    }

    void OnGUI () {
	    if (_run == false && GUI.Button (new Rect (Screen.width/2-125, Screen.height/2-35, 250, 70), "Play")) {
			_playerControl.status = true;
            for(int i = 0; i < Blasters.Length; i++)
            {
                _shootForward[i] = Blasters[i].GetComponent<ShootForward>();
                _shootForward[i].status = true;
                //Debug.Log(i);
            }
            // for (int i = 0; i < Enemies.Length; i++)
            // {
            //     _seekAndDestroy[i] = Enemies[i].GetComponent<SeekAndDestroy>();
            //     _seekAndDestroy[i].status = true;
            //     Debug.Log(i);
            // }
            _run = true;
	    } else if (_run == true && GUI.Button (new Rect (10, Screen.height / 2 + 25, 100, 50), "Stop")) {
			_playerControl.status = false;
            for (int i = 0; i < Blasters.Length; i++)
            {
                _shootForward[i] = Blasters[i].GetComponent<ShootForward>();
                _shootForward[i].status = false;
            }
            // for (int i = 0; i < Enemies.Length; i++)
            // {
            //     _seekAndDestroy[i] = Enemies[i].GetComponent<SeekAndDestroy>();
            //     _seekAndDestroy[i].status = false;
            // }
            _run = false;
	    }

        GUI.Label (new Rect(10, Screen.height / 2 - 150, 250, 25), "CONTROLS:");
		GUI.Label (new Rect(10, Screen.height / 2 - 125, 250, 25), "Left Stick - Control Ship");
		GUI.Label (new Rect(10, Screen.height / 2 - 100, 250, 25), "A Button - Fire Laser");
		GUI.Label (new Rect(10, Screen.height / 2 - 75, 250, 25), "Left Bumper - Rotate Left");
		GUI.Label (new Rect(10, Screen.height / 2 - 50, 250, 25), "Right Bumper - Rotate Right");				
		GUI.Label (new Rect(10, Screen.height / 2 - 25, 250, 25), "Right Trigger - Boost");
		GUI.Label (new Rect(10, Screen.height / 2, 250, 25), "Left Trigger - Soft Break");
	}

    void Update()
    {
        // Joystick Controls
        if (Input.GetButtonDown("Start Button"))
        {
            _playerControl.status = true;
            for (int i = 0; i < Blasters.Length; i++)
            {
                _shootForward[i] = Blasters[i].GetComponent<ShootForward>();
                _shootForward[i].status = true;
            }
            // for (int i = 0; i < Enemies.Length; i++)
            // {
            //     _seekAndDestroy[i] = Enemies[i].GetComponent<SeekAndDestroy>();
            //     _seekAndDestroy[i].status = true;
            // }
            _run = true;
        }
        else if (Input.GetButtonDown("Start Button") || Input.GetButtonDown("Back Button"))
        {
            _playerControl.status = false;
            for (int i = 0; i < Blasters.Length; i++)
            {
                _shootForward[i] = Blasters[i].GetComponent<ShootForward>();
                _shootForward[i].status = false;
            }
            // for (int i = 0; i < Enemies.Length; i++)
            // {
            //     _seekAndDestroy[i] = Enemies[i].GetComponent<SeekAndDestroy>();
            //     _seekAndDestroy[i].status = false;
            // }
            _run = false;
        }
    }
}

