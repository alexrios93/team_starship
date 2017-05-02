using UnityEngine;
using System.Collections;

public class ControlLayout : MonoBehaviour {

    public Font retroFont;
    private int size = 11;
	private bool _run = false;
	private PlayerControl _playerControl;

    private GameObject[] Blasters;
    ShootForward[] _shootForward;

    public AudioClip startSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    private bool hasPressed = false;

    // public GameObject[] Enemies;
    // SeekAndDestroy[] _seekAndDestroy;

    void Start(){
		_playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        Blasters = GameObject.FindGameObjectsWithTag("Blaster");
        _shootForward = new ShootForward[Blasters.Length];

        // Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // _seekAndDestroy = new SeekAndDestroy[Enemies.Length];
    }

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    void OnGUI () {
        GUIStyle retroStyle = new GUIStyle();
        retroStyle.font = retroFont;
        retroStyle.fontSize = size;
        retroStyle.normal.textColor = Color.white;

	    if (_run == false && GUI.Button (new Rect (Screen.width/2-125, Screen.height/2-35, 250, 70), "PLAY")) {
            Play();
	    } else if (_run == true && GUI.Button (new Rect (10, Screen.height / 2 + 25, 100, 50), "PAUSE")) {
            Pause();
	    }

        GUI.Label (new Rect(10, Screen.height / 2 - 150, 250, 25), "CONTROLS:", retroStyle);
		GUI.Label (new Rect(10, Screen.height / 2 - 125, 250, 25), "Left Stick - Control Ship", retroStyle);
		GUI.Label (new Rect(10, Screen.height / 2 - 100, 250, 25), "A Button - Fire Laser", retroStyle);
		GUI.Label (new Rect(10, Screen.height / 2 - 75, 250, 25), "Left Bumper - Barrel Roll Left", retroStyle);
		GUI.Label (new Rect(10, Screen.height / 2 - 50, 250, 25), "Right Bumper - Barrel Roll Right", retroStyle);				
		GUI.Label (new Rect(10, Screen.height / 2 - 25, 250, 25), "Right Trigger - Boost", retroStyle);
		GUI.Label (new Rect(10, Screen.height / 2, 250, 25), "Left Trigger - Soft Break", retroStyle);
	}

    void Update()
    {
        // Joystick Controls
        if (Input.GetButtonDown("Start Button") || Input.GetButtonDown("Back Button"))
        {            
            if (!hasPressed)
            {
                Play();
                hasPressed = true;
            }
            else
            {
                if(hasPressed)
                {
                    Pause();
                    hasPressed = false;
                }

            }
        }
    }

    void Play() 
    {
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

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(startSound, vol);
    }

    void Pause(){
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

