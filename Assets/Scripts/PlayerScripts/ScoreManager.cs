using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public Font retroFont;
    private int size = 11;

    public static int playerScore = 0;
    public static int highScore = 0;
    private int displayScore;

    private static string scoreText = "";

    // Use this for initialization
    void Start () {
        // Reset the Player Score
        if (SceneManager.GetActiveScene().name == "Main")
        {
            playerScore = 0;
        }
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
        retroStyle.alignment = TextAnchor.UpperCenter;

        // Display Score
        GUI.Label(new Rect(0, 20, Screen.width, 50), scoreText + displayScore.ToString(), retroStyle);
    }

    // Update is called once per frame
    void Update () {
        // Display Highest Score      
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {            
            scoreText = "HIGH SCORE ";            
            if(playerScore > highScore)     // If Player Score is Greater Than Current High Score
            {
                highScore = playerScore;
                displayScore = highScore;   // Display New High Score
            }
            if (playerScore < highScore)
            {
                displayScore = highScore;   // Keep Current High Score
            }
            
           //Destroy(this.gameObject);
        }

        // In the Remaining Scenes, Display the Current Player Score
        else if((SceneManager.GetActiveScene().name == "Main") || (SceneManager.GetActiveScene().name == "VictoryScene") || (SceneManager.GetActiveScene().name == "DeathScene"))
        {
            scoreText = "SCORE ";
            displayScore = playerScore;
        }      
    }
}
