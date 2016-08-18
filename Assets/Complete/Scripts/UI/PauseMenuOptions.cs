﻿using UnityEngine;
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
        quitMenu.enabled = false;
        controlsMenu.enabled = false;
        pauseMenu.enabled = false;
        restartMenu.enabled = false;
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
        restart = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}