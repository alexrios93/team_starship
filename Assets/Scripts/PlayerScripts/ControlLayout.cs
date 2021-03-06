using UnityEngine;
using System.Collections;

public class ControlLayout : MonoBehaviour {

    public Font retroFont;
    private int size = 11;
	private bool _run = false;

	private PlayerControl _playerControl;

    private GameObject[] Blasters;
    ShootForward[] _shootForward;

    public GameObject Mothership;
    private SpawnEnemies _spawnEnemies;

    private GameObject[] Satellites;
    private SatelliteHealth[] _satelliteHealth;

    private PlayerHealth _playerHealth;

    public AudioClip startSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    private bool hasPressed = false;

    // public GameObject[] Enemies;
    // SeekAndDestroy[] _seekAndDestroy;

    void Start(){
		_playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        Blasters = GameObject.FindGameObjectsWithTag("Blaster");
        _shootForward = new ShootForward[Blasters.Length];

        _spawnEnemies = Mothership.GetComponent<SpawnEnemies>();

        Satellites = GameObject.FindGameObjectsWithTag("Satellite");
        _satelliteHealth = new SatelliteHealth[Satellites.Length];

        Play();
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

        GUI.skin.font = retroFont;

	    if (_run == false && GUI.Button (new Rect (Screen.width/2-125, Screen.height/2-35, 250, 70), "PLAY")) {
            Play();

            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(startSound, vol);
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

                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(startSound, vol);
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
        _playerHealth.status = true;
        _spawnEnemies.status = true;

        for (int i = 0; i < Satellites.Length; i++)
        {
            _satelliteHealth[i] = Satellites[i].GetComponent<SatelliteHealth>();
            _satelliteHealth[i].status = true;
            //Debug.Log(i);
        }

        for (int i = 0; i < Blasters.Length; i++)
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
    }

    void Pause(){
        _playerControl.status = false;
        _playerHealth.status = false;
        _spawnEnemies.status = false;
        for (int i = 0; i < Satellites.Length; i++)
        {
            _satelliteHealth[i] = Satellites[i].GetComponent<SatelliteHealth>();
            _satelliteHealth[i].status = false;
            //Debug.Log(i);
        }
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

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(startSound, vol);
    }
}

