using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script formely known as SatelliteDown
public class VictoryOrDeath : MonoBehaviour {

    private GameObject[] Satellites;
    public int satelliteDown = 0;

    private GameObject[] Enemy;
    public List<int> enemyList;

    public GameObject Mothership;

    //public GameObject Camera;
    private FadeScreen _fadeScreen;

    // Use this for initialization
    void Start () {
        Satellites = GameObject.FindGameObjectsWithTag("Satellite");
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        _fadeScreen = gameObject.GetComponent<FadeScreen>();
    }
	
	// Update is called once per frame
	void Update () {
        // If all satellites are down, display DeathScene
		if(satelliteDown == Satellites.Length)
        {
            _fadeScreen.fadeOut = true;
            StartCoroutine(DeathSceneCountDown());
        }

        // If all enemies & the mothership are destroyed, display VictoryScene
        if (enemyList.Count == 0 && !Mothership)
        {
            _fadeScreen.fadeOut = true;
            StartCoroutine(VictorySceneCountDown());
        }
    }

    IEnumerator DeathSceneCountDown()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("DeathScene");
    }

    IEnumerator VictorySceneCountDown()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("VictoryScene");
    }
}
