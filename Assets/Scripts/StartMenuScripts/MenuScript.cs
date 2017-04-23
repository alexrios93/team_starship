using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas QuitMenu;
    public Button playText;
    public Button exitText;

	// Use this for initialization
	void Start () {
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        playText = playText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        QuitMenu.enabled = false;
    }

    public void ExitPress()
    {
        QuitMenu.enabled = true;
        playText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        QuitMenu.enabled = false;
        playText.enabled = true;
        exitText.enabled = true;
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        SceneManager.UnloadSceneAsync("StartScreen");
    }
}
