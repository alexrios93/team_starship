using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {
    public string Text;
    //public GameObject Camera;

    private float Duration = 1.25f;
    private float BlinkingDuration = 1.0f;
    private float BlinkingStart = 1.0f;
    private float SpawnTime;
    
	// Use this for initialization
	void Start () {
        transform.Translate(0.0f, 10.0f, 0.0f);
        transform.GetComponent<TextMesh>().text = ("+ 100");
        transform.GetComponent<TextMesh>().characterSize = (2);
        SpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if ( (Duration) > (Time.time - SpawnTime))
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0.0f,180.0f,0.0f);
            transform.Translate(0.0f,0.003f * Time.deltaTime ,0.0f);  
        }
        else
        {
            //if(blinking)
            Destroy(gameObject);
        }
    }
}
