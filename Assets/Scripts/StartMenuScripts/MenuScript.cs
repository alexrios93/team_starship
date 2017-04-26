using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas QuitMenu;
    public Canvas InfoMenu;
    public Button playText;
    public Button exitText;

    // Use this for initialization
    void Start () {
        InfoMenu = InfoMenu.GetComponent<Canvas>();
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        playText = playText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        QuitMenu.enabled = false;
        InfoMenu.enabled = false;
    }

    public void ExitPress()
    {
        QuitMenu.enabled = true;
        InfoMenu.enabled = false;
        playText.enabled = false;
        exitText.enabled = false;

        if(Input.GetButtonDown("Start Button")  || Input.GetButtonDown("A Button")) {
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
    }

    public void PlayPress() //Loads up the InfoMenu
    {
        QuitMenu.enabled = false;
        InfoMenu.enabled = true;
        playText.enabled = false;
        exitText.enabled = false;

        if(Input.GetButtonDown("A Button")) {
            StartScene();
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
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
