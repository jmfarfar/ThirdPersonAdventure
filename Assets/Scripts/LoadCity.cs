using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCity : MonoBehaviour
{
    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("City");
    }
}
