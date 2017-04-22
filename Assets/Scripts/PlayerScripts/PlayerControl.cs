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
	public int maxSpeed = 70;
	public int minSpeed = 10;
    public int normalSpeed = 30;
	public float rotationSpeed = 150;
	public bool status = false;
	
	private int currrentSpeed = 30;
	private GameObject[] boosters;

	public float movementSpeed = 1.5f;

    public Texture2D crosshair;
	
	void Start(){
		boosters = GameObject.FindGameObjectsWithTag("Booster");

        cachedY = boostTransform.position.y;
        maxXValue = boostTransform.position.x;
        minXValue = boostTransform.position.x - (boostTransform.rect.width * 2);
        currentBoost = maxBoost;
        onCD = false;
    }
	
    void LateUpdate()
    {
		//Coordinates pause - play with manager object
		if (status){
			//Rotation manager
			if (Input.GetButton("Left Bumper") || Input.GetKey(KeyCode.Q))
				transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
			else if (Input.GetButton("Right Bumper") || Input.GetKey(KeyCode.E))
				transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed);
			
			//Max Speed
			if (Input.GetButton("Right Trigger") || Input.GetButton("Right Thumb") || Input.GetKey(KeyCode.Mouse1)){
                //  currrentSpeed = maxSpeed;
                //  MaxBoosters(0.65f);
                if (!onCD && currentBoost > 0)
                {
                    currrentSpeed = maxSpeed;
                    //	MaxBoosters(0.65f);
                    StartCoroutine(CoolDown());
                    CurrentBoost -= 1;
                }
                if (currentBoost <= 0) // If boost reaches 0%
                {
                    currrentSpeed = normalSpeed;
                }          

            }
            //Min Speed
			else if (Input.GetButton("Left Trigger") || Input.GetButton("Left Thumb") || Input.GetKey(KeyCode.Space)){
                currrentSpeed = minSpeed;
                // MaxBoosters(0.3f);
                if (!onCD && currentBoost < maxBoost)
                {                    
                    StartCoroutine(CoolDown());
                    CurrentBoost += 1;
                }
			}
            //Normal speed
			else {
                //	currrentSpeed = 30;
                currrentSpeed = normalSpeed;
                // MaxBoosters(0.55f);
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
 //            //booster.GetComponent<ParticleSystem>().main.startSizeMultiplier = intensity;
 //        }
	// }   
    
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
}

