using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas QuitMenu;
    public Canvas InfoMenu;
    public Button playText;
    public Button exitText;

    public AudioClip selectSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Use this for initialization
    void Start () {
        InfoMenu = InfoMenu.GetComponent<Canvas>();
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        playText = playText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        QuitMenu.enabled = false;
        InfoMenu.enabled = false;
    }

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void ExitPress()
    {
        QuitMenu.enabled = true;
        InfoMenu.enabled = false;
        playText.enabled = false;
        exitText.enabled = false;

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);

        if (Input.GetButtonDown("Start Button")  || Input.GetButtonDown("A Button")) {
            ExitGame();
        }
        else if(Input.GetButtonDown("Back Button")  || Input.GetButtonDown("B Button")) {
            NoPress();
        }
    }

    public void NoPress()
    {
        QuitMenu.enabled = false;
        InfoMenu.enabled = false;
        playText.enabled = true;
        exitText.enabled = true;

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);
    }

    public void PlayPress() //Loads up the InfoMenu
    {
        QuitMenu.enabled = false;
        InfoMenu.enabled = true;
        playText.enabled = false;
        exitText.enabled = false;

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);

        if (Input.GetButtonDown("A Button")) {
            StartScene();
        }
    }

    public void StartScene()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);

        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);

        SceneManager.LoadScene("StartScreen");
    }

    // Joystick Control Start
    void Update()
    {
        // if(Input.GetButtonDown("Start Button")  || Input.GetButtonDown("A Button")) {
        //     StartScene();
        // }
        if(Input.GetButtonDown("Start Button")) {
            PlayPress();
        }
        else if(Input.GetButtonDown("Back Button")  || Input.GetButtonDown("B Button")) {
            ExitPress();
        }
    }
}
