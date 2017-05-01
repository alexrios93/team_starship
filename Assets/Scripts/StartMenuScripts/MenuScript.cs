using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{

    public Canvas QuitMenu;
    public Canvas InfoMenu;
    public Canvas StartMenu;
    public Button playText;
    public Button exitText;
    public Button yesText;
    public Button noText;
    public Button continueText;

    public AudioClip selectSound;
    public AudioClip scrollSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private bool hasPlayed = false;

    // Use this for initialization
    void Start()
    {
        InfoMenu = InfoMenu.GetComponent<Canvas>();
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        StartMenu = StartMenu.GetComponent<Canvas>();

        StartMenu.enabled = true;
        QuitMenu.enabled = false;
        InfoMenu.enabled = false;

        playText.interactable = true;
        exitText.interactable = true;
        yesText.interactable = false;
        noText.interactable = false;
        continueText.interactable = false;
    }

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void ExitPress()
    {
        StartMenu.enabled = true;
        QuitMenu.enabled = true;
        InfoMenu.enabled = false;

        playText.interactable = false;
        exitText.interactable = false;
        yesText.interactable = true;
        noText.interactable = true;
        continueText.interactable = false;

        noText.Select();

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);
    }

    public void NoPress()
    {
        StartMenu.enabled = true;
        QuitMenu.enabled = false;
        InfoMenu.enabled = false;

        playText.interactable = true;
        exitText.interactable = true;
        yesText.interactable = false;
        noText.interactable = false;
        continueText.interactable = false;

        playText.Select();

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);
    }

    public void PlayPress() //Loads up the InfoMenu
    {
        StartMenu.enabled = true;
        QuitMenu.enabled = false;
        InfoMenu.enabled = true;

        playText.interactable = false;
        exitText.interactable = false;
        yesText.interactable = false;
        noText.interactable = false;
        continueText.interactable = true;

        continueText.Select();

        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);

        //if (Input.GetButtonDown("B Button")) {
        //     NoPress();
        //}
    }

    public void StartScene()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);

        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        // float vol = Random.Range(volLowRange, volHighRange);
        // source.PlayOneShot(selectSound, vol);

        SceneManager.LoadScene("StartScreen");
    }

    public void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            //print(Input.GetAxis("Horizontal"));
            if (!hasPlayed)
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(scrollSound, vol);
                hasPlayed = true;
            }
        }
        else if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            hasPlayed = false;
        }
    }

}
