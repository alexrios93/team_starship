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
	
	void Start(){
		boosters = GameObject.FindGameObjectsWithTag("Booster");		
	}
	
    void LateUpdate()
    {
		//Coordinates pause - play with manager object
		if (status){
			//Rotation manager
			if (Input.GetKey(KeyCode.A))
				transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
			else if (Input.GetKey(KeyCode.D))
				transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed);
			
			//Max speed
			if (Input.GetKey(KeyCode.W)){
				currrentSpeed = maxSpeed;
			//	MaxBoosters(0.65f);
			}//Min speed
			else if (Input.GetKey(KeyCode.S)){
				currrentSpeed = minSpeed;
			//	MaxBoosters(0.3f);
			}//Cruise speed
			else{
                //	currrentSpeed = 30;
                currrentSpeed = normalSpeed;
			//	MaxBoosters(0.55f);
			}
			
			Vector3 mouseMovement = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f)) * 1;
			transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, -mouseMovement.x) * 0.025f);
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

}

