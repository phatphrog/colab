using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuOptions : MonoBehaviour
{

    public Canvas quitMenu;
    public Canvas restartMenu;
    public Canvas controlsMenu;
    public Button controlsButton;
    public Button resumeButton;
    public Button exitButton;
    public Canvas pauseMenu;
    public Canvas controllerImage;
    public Canvas keyboardText;

    private string pauseButton;
    public bool isPaused = false;
    public bool restart = false;

    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        controlsMenu = controlsMenu.GetComponent<Canvas>();
        resumeButton = resumeButton.GetComponent<Button>();
        controlsButton = controlsButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        pauseMenu = this.GetComponent<Canvas>();
        controllerImage = controllerImage.GetComponent<Canvas>();
        keyboardText = keyboardText.GetComponent<Canvas>();
        keyboardText.enabled = false;
        controllerImage.enabled = false;
        quitMenu.enabled = false;
        controlsMenu.enabled = false;
        pauseMenu.enabled = false;
        restartMenu.enabled = false;
        keyboardText.enabled = false;
        pauseButton = "Cancel";
    }

    private void Update()
    {
        if (Input.GetButtonDown(pauseButton) && !isPaused)
        {
            resumeButton.enabled = true;
            exitButton.enabled = true;
            pauseMenu.enabled = true;
            controlsButton.enabled = true;
            isPaused = true;
            //TODO: figure out a way to effectively "pause" gameplay
        }
        else if (Input.GetButtonDown(pauseButton) && isPaused && quitMenu.enabled == false && controlsMenu.enabled == false)
        {
            resumeButton.enabled = false;
            exitButton.enabled = false;
            pauseMenu.enabled = false;
            controlsButton.enabled = false;
            isPaused = false;
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
        controllerImage.enabled = false;
        keyboardText.enabled = false;
    }

    public void ControlsPress()
    {
        controlsMenu.enabled = true;
        resumeButton.enabled = false;
        exitButton.enabled = false;
        pauseMenu.enabled = false;
        controlsButton.enabled = false;
        controllerImage.enabled = true;
    }

    public void ResumePress()
    {
        resumeButton.enabled = false;
        exitButton.enabled = false;
        pauseMenu.enabled = false;
        controlsButton.enabled = false;
        isPaused = false;
    }

    public void RestartPress()
    {
        restartMenu.enabled = true;
        resumeButton.enabled = false;
        exitButton.enabled = false;
        pauseMenu.enabled = false;
        controlsButton.enabled = false;
    }

    public void RestartNoPress()
    {
        restartMenu.enabled = false;
        resumeButton.enabled = true;
        exitButton.enabled = true;
        pauseMenu.enabled = true;
        controlsButton.enabled = true;
    }

    public void RestartYesPress()
    {
        resumeButton.enabled = false;
        exitButton.enabled = false;
        pauseMenu.enabled = false;
        controlsButton.enabled = false;
        restartMenu.enabled = false;
        isPaused = false;
        RestartLevel(2);
       
    }

    private void RestartLevel(int num)
    {
        if (num < 0 || num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Can't load scene num" + num + " , SceneManager only has " + SceneManager.sceneCountInBuildSettings + "scenes in BuildSettings!");
            return;
        }

        LoadingScreenManager.LoadScene(num);
    }

    public void GamePadPress()
    {
        controllerImage.enabled = true;
        keyboardText.enabled = false;
    }

    public void KeyboardPress()
    {
        controllerImage.enabled = false;
        keyboardText.enabled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}