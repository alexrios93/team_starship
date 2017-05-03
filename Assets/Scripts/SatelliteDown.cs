using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SatelliteDown : MonoBehaviour {

    private GameObject[] Satellites;
    public int satelliteDown = 0;

    //public GameObject Camera;
    private FadeScreen _fadeScreen;

    // Use this for initialization
    void Start () {
        Satellites = GameObject.FindGameObjectsWithTag("Satellite");
        _fadeScreen = gameObject.GetComponent<FadeScreen>();
    }
	
	// Update is called once per frame
	void Update () {
		if(satelliteDown == Satellites.Length)
        {
            _fadeScreen.fadeOut = true;
            StartCoroutine(DeathSceneCountDown());
        }
	}

    IEnumerator DeathSceneCountDown()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("DeathScene");
    }
}
