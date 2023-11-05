using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, Controls.IPauseMenuActions
{
    public GameObject pausePanel;
    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.PauseMenu.Enable();
        controls.PauseMenu.SetCallbacks(this);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);

        }
    }
}
