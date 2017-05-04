using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SeamlessMusic : MonoBehaviour
{
    public Font retroFont;
    private int size = 11;

    public AudioClip BackgroudMusic;
    //public AudioClip scrollSound;
    private AudioSource source;
    private float volLowRange = .25f;
    private float volHighRange = .5f;
    //private bool hasPlayed = false;

    void Start()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(BackgroudMusic, vol);
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
        GUI.Label(new Rect(10, Screen.height / 2 + 300, 500, 50), BackgroudMusic.name, retroStyle);
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