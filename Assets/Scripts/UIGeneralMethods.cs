using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGeneralMethods : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("City");

    }

    public void Begin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DeborahsDepartment");

    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }

    public void Credits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void Controls()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Controls");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
