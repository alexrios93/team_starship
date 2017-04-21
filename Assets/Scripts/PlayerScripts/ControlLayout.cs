using UnityEngine;
using System.Collections;

public class ControlLayout : MonoBehaviour {

	private bool _run = false;
	private PlayerControl _playerControl;
	
	void Start(){
		_playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}

    void OnGUI () {
	    if (_run == false && GUI.Button (new Rect (Screen.width/2-125, Screen.height/2-35, 250, 70), "Play")) {
			_playerControl.status = true;
	        _run = true;
	    } else if (_run == true && GUI.Button (new Rect (10, Screen.height / 2 + 25, 100, 50), "Stop")) {
			_playerControl.status = false;
	        _run = false;
	    }

        GUI.Label (new Rect(10, Screen.height / 2 - 150, 250, 25), "CONTROLS:");
		GUI.Label (new Rect(10, Screen.height / 2 - 125, 250, 25), "Left Stick - Control Ship");
		GUI.Label (new Rect(10, Screen.height / 2 - 100, 250, 25), "A Button - Fire Laser");
		GUI.Label (new Rect(10, Screen.height / 2 - 75, 250, 25), "Left Bumper - Rotate Left");
		GUI.Label (new Rect(10, Screen.height / 2 - 50, 250, 25), "Right Bumper - Rotate Right");				
		GUI.Label (new Rect(10, Screen.height / 2 - 25, 250, 25), "Right Stick Press - Boost");
		GUI.Label (new Rect(10, Screen.height / 2, 250, 25), "Left Stick Press - Break");
	}

    void Update()
    {
        // Joystick Controls
        if (Input.GetButtonDown("Start Button"))
        {
            _playerControl.status = true;
            _run = true;
        }
        else if (Input.GetButtonDown("Start Button") || Input.GetButtonDown("Back Button"))
        {
            _playerControl.status = false;
            _run = false;
        }
    }
}

