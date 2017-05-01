/**
 * Controls player based on mouse movement. Static speed value 
 * and no rotation.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    // Player Variables
	public int maxSpeed = 70;
	public int minSpeed = 10;
    public int normalSpeed = 30;
    private int currrentSpeed = 30;
    public float rotationSpeed = 150;
    public float movementSpeed = 1.5f;
    public bool status = false;  	
	
    // Booster GameObject
	private GameObject[] boosters;
    private Transform[] boostersT;    

    public Texture2D crosshair;

    // Booster Particle Scale
    private Vector3 maxBoostScale = new Vector3(3.5f,3.5f,3.5f);
	private Vector3 minBoostScale = new Vector3(0.95f,0.95f,0.95f);
    private Vector3 normalBoostScale = new Vector3(1.5f,1.5f,1.5f);

    // SoundFx
    public AudioClip boostSound;
    public AudioClip idleSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private bool hasPlayed = false;

    void Start(){        
		boosters = GameObject.FindGameObjectsWithTag("Booster");
        boostersT = new Transform[boosters.Length];

        cachedY = boostTransform.position.y;
        maxXValue = boostTransform.position.x;
        minXValue = boostTransform.position.x - (boostTransform.rect.width * 2);
        currentBoost = maxBoost;
        onCD = false;
    }

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    void LateUpdate()
    {
		//Coordinates pause - play with manager object
		if (status) {
            //Rotation Manager
            if (!inBarrelRoll)
            {
                //Return to Original Z Rotation
                Vector3 newRotationEuler = transform.rotation.eulerAngles;
                newRotationEuler.z = 0;
                Quaternion newQuat = Quaternion.identity;
                newQuat.eulerAngles = newRotationEuler;
                //transform.rotation = newQuat;

                if ((Input.GetButton("Left Bumper") && Input.GetButton("Right Bumper")) == false)
                {
                    TriggerDown = false;
                }

                //Rotate Left
                if ((Input.GetButtonDown("Left Bumper") || Input.GetKey(KeyCode.Q)) && TriggerDown == false)
                {
                    //transform.Rotate(0, 0, Time.deltaTime * rotationSpeed); //ORIGINAL
                    TriggerDown = true;
                    if (time < doubleTapDelay)
                    {
                        StartCoroutine("BarrelRollLeft");
                    }
                    time = 0.0f;
                    transform.rotation = newQuat;
                }

                //Rotate Right
                else if (Input.GetButton("Right Bumper") || Input.GetKey(KeyCode.E))
                {
                    //transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed); //ORIGINAL
                    TriggerDown = true;
                    if (time < doubleTapDelay)
                    {
                        StartCoroutine("BarrelRollRight");
                    }
                    time = 0.0f;
                    transform.rotation = newQuat;
                }                
            }                  
  
            //Max Speed SoundFX
            if (Input.GetAxisRaw("Right Trigger") < 0 || Input.GetButtonDown("Right Thumb") || Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (!hasPlayed)
                {
                    float vol = Random.Range(volLowRange, volHighRange);
                    source.PlayOneShot(boostSound, vol);
                    hasPlayed = true;
                }
            }
            else if(Input.GetAxisRaw("Right Trigger") < 0 == false)
            {
                hasPlayed = false;
            }

            //Max Speed
            if (Input.GetAxis("Right Trigger") < 0 || Input.GetButton("Right Thumb") || Input.GetKey(KeyCode.Mouse1))
            {
                //  currrentSpeed = maxSpeed;
                //  MaxBoosters(0.65f);
                if (!onCD && currentBoost > 0)
                {
                    //float vol = Random.Range(volLowRange, volHighRange);
                    //source.PlayOneShot(boostSound, vol);

                    currrentSpeed = maxSpeed;
                    for (int i = 0; i < boostersT.Length; ++i)
                    {
                        //boosters[i].transform.localScale += new Vector3(0.05f,0.05f,0.05f);
                        boosters[i].transform.localScale = Vector3.Lerp(boosters[i].transform.localScale, maxBoostScale, Time.deltaTime);
                    }
                    StartCoroutine(CoolDown());
                    CurrentBoost -= 1;
                }
                if (currentBoost <= 0) // If boost reaches 0%
                {
                    //source.Stop();

                    currrentSpeed = normalSpeed;
                    for (int i = 0; i < boostersT.Length; ++i)
                    {
                        //boosters[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        boosters[i].transform.localScale = Vector3.Lerp(boosters[i].transform.localScale, normalBoostScale, Time.deltaTime);
                    }
                }

            }

            //Min Speed SoundFX
            //if (Input.GetButtonDown("Left Trigger") || Input.GetButtonDown("Left Thumb") || Input.GetKeyDown(KeyCode.Space))
            //{
            //    float vol = Random.Range(.01f, .02f);
            //    source.PlayOneShot(idleSound, vol);
            //}

            //Min Speed
            else if (Input.GetAxis("Left Trigger") > 0.15 || Input.GetButton("Left Thumb") || Input.GetKey(KeyCode.Space))
            {
                currrentSpeed = minSpeed;
                for (int i = 0; i < boostersT.Length; ++i)
                {
                    //boosters[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    boosters[i].transform.localScale = Vector3.Lerp(boosters[i].transform.localScale, minBoostScale, Time.deltaTime);
                }
                //MaxBoosters(0.3f);
                if (!onCD && currentBoost < maxBoost)
                {
                    StartCoroutine(CoolDown());
                    CurrentBoost += 1;
                }
            }
            //Normal speed
            else {
                //source.Stop();
                float vol = Random.Range(.01f, .02f);
                source.PlayOneShot(idleSound, vol);

                currrentSpeed = normalSpeed;
                for (int i = 0; i < boostersT.Length; ++i)
                {
                    //boosters[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    boosters[i].transform.localScale = Vector3.Lerp(boosters[i].transform.localScale, normalBoostScale, Time.deltaTime);
                }
                if (!onCD && currentBoost < maxBoost)
                {
                    StartCoroutine(CoolDown());
                    CurrentBoost += 1;
                }
            }
			
			// Mouse Control
			//Vector3 mouseMovement = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f)) * 1; 
			//transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, -mouseMovement.x) * 0.025f);

			// Joystick Control
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.Rotate(new Vector3(-vertical, horizontal, 0) * movementSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * currrentSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * currrentSpeed);

        }
    }

	// Booster FXs [WORK IN PROGRESS]
	// void MaxBoosters(float intensity){
	// 	foreach (GameObject booster in boosters)
 //        {
 //            ///booster.GetComponent<ParticleSystem>().main.startSizeMultiplier = intensity;
 //            ParticleSystem m_System = booster.GetComponent<ParticleSystem>();
 //            ParticleSystem.MainModule main = m_System.main;
 //            ParticleSystem.MinMaxCurve minMaxCurve = main.startSize; //Get Size

 //            minMaxCurve.constant *= intensity; //Modify Size
 //            main.startSize = minMaxCurve; //Assign the modified startSize back

 //        }
 //    }   
    
    //DELETE OnGUI IF FAULTY 4/17
    void OnGUI()
    {
        float vectorx = Input.mousePosition.x;
        float vectory = Input.mousePosition.y;
        GUI.DrawTexture(new Rect(vectorx - 15f, -vectory + Screen.height - 15f, 30f, 30f), crosshair);        
    }

    // Player Boost Variables and Functions
    public RectTransform boostTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public int currentBoost;

    private int CurrentBoost
    {
        get { return currentBoost; }
        set
        {
            if (value >= 0)
            {
                currentBoost = value;
            }
            else
            {
                currentBoost = 0;
            }

            handleBoost();
        }
    }

    public int maxBoost;
    public Image visualBoost;
    public float coolDown;
    private bool onCD;

    private void handleBoost()
    {
        float currentXValue = MapValues(currentBoost, 0, maxBoost, minXValue, maxXValue);
        boostTransform.position = new Vector3(currentXValue, cachedY);

        if (currentBoost > maxBoost / 2)  // Greater than 50%
        {
            visualBoost.GetComponent<Image>().color = new Color32((byte)MapValues(currentBoost, maxBoost / 2, maxBoost, 255, 0), 0, (byte)MapValues(currentBoost, maxBoost / 2, maxBoost, 0, 255), 255);            
        }

        else //Less than 50%
        {
            //visualBoost.GetComponent<Image>().color = new Color32((byte)MapValues(currentBoost, 0, maxBoost / 2, 0, 125), 0, 255, 255);
            visualBoost.GetComponent<Image>().color = new Color32(255, 0, (byte)MapValues(currentBoost, maxBoost / 2, maxBoost, 255, 0), 255);
        }
    }

    IEnumerator CoolDown()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;

    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    // End of Player Boost Variables and Functions

    // Barrel Roll Values and Functions
    private bool inBarrelRoll = false;
    private float doubleTapDelay = 0.2f;
    private float barrelRollDuration = 1.0f;

    private float time = float.MaxValue;
    private bool TriggerDown = false;

    IEnumerator BarrelRollLeft()
    {
        inBarrelRoll = true;
        float t = 0.0f;

        Vector3 originalRotation = transform.rotation.eulerAngles;

        Vector3 goalRotation = originalRotation;
        goalRotation.z += 180.0f;

        Vector3 currentRotation = originalRotation;

        while (t < barrelRollDuration / 2.0f)
        {
            currentRotation.z = Mathf.Lerp(originalRotation.z, goalRotation.z, t / (barrelRollDuration / 2.0f));
            transform.rotation = Quaternion.Euler(currentRotation);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0;

        originalRotation = transform.rotation.eulerAngles;
        goalRotation = originalRotation;
        goalRotation.z += 180.0f;

        while (t < barrelRollDuration / 2.0f)
        {
            currentRotation.z = Mathf.Lerp(originalRotation.z, goalRotation.z, t / (barrelRollDuration / 2.0f));
            transform.rotation = Quaternion.Euler(currentRotation);
            t += Time.deltaTime;
            yield return null;
        }
        inBarrelRoll = false;
    }

    IEnumerator BarrelRollRight()
    {
        inBarrelRoll = true;
        float t = 0.0f;

        Vector3 originalRotation = transform.rotation.eulerAngles;

        Vector3 goalRotation = originalRotation;
        goalRotation.z -= 180.0f;

        Vector3 currentRotation = originalRotation;

        while (t < barrelRollDuration / 2.0f)
        {
            currentRotation.z = Mathf.Lerp(originalRotation.z, goalRotation.z, t / (barrelRollDuration / 2.0f));
            transform.rotation = Quaternion.Euler(currentRotation);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0;

        originalRotation = transform.rotation.eulerAngles;
        goalRotation = originalRotation;
        goalRotation.z -= 180.0f;

        while (t < barrelRollDuration / 2.0f)
        {
            currentRotation.z = Mathf.Lerp(originalRotation.z, goalRotation.z, t / (barrelRollDuration / 2.0f));
            transform.rotation = Quaternion.Euler(currentRotation);
            t += Time.deltaTime;
            yield return null;
        }
        inBarrelRoll = false;
    }
    // End of Barrel Roll Values and Functions
}


