using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, Controls.IPauseMenuActions
{

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Selectable firstElementToSelect;
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
            if (!pausePanel.activeSelf)
            {
                firstElementToSelect?.Select();
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
            }

        }
    }
}
