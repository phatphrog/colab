using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuOptions : MonoBehaviour
{

    public Canvas quitMenu;
    public Canvas controlsMenu;
    public Button controlsButton;
    public Button resumeButton;
    public Button exitButton;
    public Canvas pauseMenu;
    private string pauseButton; 

    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        controlsMenu = controlsMenu.GetComponent<Canvas>();
        resumeButton = resumeButton.GetComponent<Button>();
        controlsButton = controlsButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        pauseMenu = this.GetComponent<Canvas>();
        quitMenu.enabled = false;
        controlsMenu.enabled = false;
        pauseMenu.enabled = false;
        pauseButton = "Cancel"; 
    }

    private void Update()
    {
        if (Input.GetButtonDown(pauseButton))
        {
            resumeButton.enabled = true;
            exitButton.enabled = true;
            pauseMenu.enabled = true;
            controlsButton.enabled = true;

            //TODO: figure out a way to stop input from players
        }
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        resumeButton.enabled = false;
        exitButton.enabled = false;
        pauseMenu.enabled = false;
        controlsButton.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        resumeButton.enabled = true;
        exitButton.enabled = true;
        pauseMenu.enabled = true;
        controlsButton.enabled = true;
    }

    public void CloseControlsPress()
    {
        controlsMenu.enabled = false;
        resumeButton.enabled = true;
        exitButton.enabled = true;
        pauseMenu.enabled = true;
        controlsButton.enabled = true;
    }

    public void ControlsPress()
    {
        controlsMenu.enabled = true;
        resumeButton.enabled = false;
        exitButton.enabled = false;
        pauseMenu.enabled = false;
        controlsButton.enabled = false;
    }

    public void ResumePress()
    {
        resumeButton.enabled = false;
        exitButton.enabled = false;
        pauseMenu.enabled = false;
        controlsButton.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

