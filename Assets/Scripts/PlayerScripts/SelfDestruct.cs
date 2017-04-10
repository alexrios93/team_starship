using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float selfDestructTime = 1f;

	// Update is called once per frame
	void Update () {
		selfDestructTime -= Time.deltaTime;

		if(selfDestructTime <= 0)
		{
			Destroy (gameObject);
		}		
	}
}
