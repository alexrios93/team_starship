using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroudMusic : MonoBehaviour
{
    public Font retroFont;
    private int size = 11;

    public AudioClip[] playList;
    private string musicName;

    private AudioSource source;
    private float volLowRange = .25f;
    private float volHighRange = .5f;

    private bool playLoop = true;   // Needed playList Loop

    IEnumerator Start()
    {
        float vol = Random.Range(volLowRange, volHighRange);

        if (playList.Length == 0)
        {
            playLoop = false;   // If there's no playList, then there's no playLoop
        }

        while (playLoop)
        {
            for (int i = 0; i < playList.Length; i++)
            {
                playLoop = false;

                //Debug.Log("The numerator is now " + i.ToString());
                                
                source.PlayOneShot(playList[i], vol);

                musicName = playList[i].name; // Get current song name

                yield return new WaitForSeconds(playList[i].length + 2.5f); // Wait for song to finish, before heading to the next one       
                
                if (i == playList.Length - 1)
                {
                    playLoop = true;    // Sets to the beginning of playList
                }
            }
        }        
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        source = gameObject.AddComponent<AudioSource>();
    }

    void OnGUI()
    {
        GUIStyle retroStyle = new GUIStyle();
        retroStyle.font = retroFont;
        retroStyle.fontSize = size;
        retroStyle.normal.textColor = Color.white;

        // Display Song Name
        GUI.Label(new Rect(10, Screen.height / 2 + 300, 500, 50), musicName, retroStyle);
        //GUI.Label(new Rect(10, Screen.height / 2 + 300, 500, 50), BackgroudMusic.name, retroStyle);   ///Original
    }

    void Update()
    {
        //This kills the music
        //if (SceneManager.GetActiveScene().name == "DeathScene")
        //{
        //   Destroy(this.gameObject);
        //}
    }
}