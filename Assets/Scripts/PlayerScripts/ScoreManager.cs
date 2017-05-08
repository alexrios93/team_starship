using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public Font retroFont;
    private int size = 11;

    public static int playerScore = 0;

    // Use this for initialization
    void Start () {
		
	}

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ScoreManager");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    void OnGUI()
    {
        GUIStyle retroStyle = new GUIStyle();
        retroStyle.font = retroFont;
        retroStyle.fontSize = size;
        retroStyle.normal.textColor = Color.white;

        // Display Score
        GUI.Label(new Rect(Screen.width/2 - 20, 20, 500, 50), "SCORE " + playerScore.ToString(), retroStyle);
    }

    // Update is called once per frame
    void Update () {
        // Reset the Score
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            playerScore = 0;
           //Destroy(this.gameObject);
        }
    }
}
