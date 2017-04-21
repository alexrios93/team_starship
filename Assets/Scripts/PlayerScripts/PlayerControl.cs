/**
 * Controls player based on mouse movement. Static speed value 
 * and no rotation.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	}
	
    void LateUpdate()
    {
		//Coordinates pause - play with manager object
		if (status){
			//Rotation manager
			if (Input.GetButton("Left Bumper") || Input.GetKey(KeyCode.A))
				transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
			else if (Input.GetButton("Right Bumper") || Input.GetKey(KeyCode.D))
				transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed);
			
			//Max speed
			if (Input.GetButton("Right Trigger") || Input.GetButton("Right Thumb") || Input.GetKey(KeyCode.W)){
				currrentSpeed = maxSpeed;
			//	MaxBoosters(0.65f);
			}//Min speed
			else if (Input.GetButton("Left Trigger") || Input.GetButton("Left Thumb") || Input.GetKey(KeyCode.S)){
				currrentSpeed = minSpeed;
			//	MaxBoosters(0.3f);
			}//Cruise speed
			else{
                //	currrentSpeed = 30;
                currrentSpeed = normalSpeed;
			//	MaxBoosters(0.55f);
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
    
    // //Test DAMAGE
    // void OnTriggerStay(Collider other)
    // {
    //     if(other.name == "Health")
    //     {
    //         Debug.Log("Healing");
    //     }
    //     if (other.name == "Damage")
    //     {
    //         Debug.Log("Hurting");
    //     }
    // }

    //DELETE HERE IF FAULTY 4/17
    void OnGUI()
    {
        float vectorx = Input.mousePosition.x;
        float vectory = Input.mousePosition.y;
        GUI.DrawTexture(new Rect(vectorx - 15f, -vectory + Screen.height - 15f, 30f, 30f), crosshair);
        
    }
}

