using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenuOptions : MonoBehaviour {

    public Canvas quitMenu;
    public Button startButton;
    public Button exitButton;
    public Canvas startMenu;

	// Use this for initialization
	void Start () 
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startButton = startButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        startMenu = this.GetComponent<Canvas>();
        quitMenu.enabled = false;

	}
	
	public void ExitPress()
    {
        quitMenu.enabled = true;
        startButton.enabled = false;
        exitButton.enabled = false;
        //startMenu.enabled = false;

    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startButton.enabled = true;
        exitButton.enabled = true;
        //startMenu.enabled = true;
    }

    public void StartLevel()
    {
        int num = 2;
        if (num < 0 || num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Can't load scene num" + num + " , SceneManager only has " + SceneManager.sceneCountInBuildSettings + "scenes in BuildSettings!");
            return;
        }

        LoadingScreenManager.LoadScene(num);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
