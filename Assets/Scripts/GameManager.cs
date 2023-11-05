using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine player;
    [SerializeField] private Volume globalVolume;

    private bool isPlayerBat;

    void Start()
    {
        player.ChangeToBatEvent += OnPlayerChangeToBat;
    }

    private void OnPlayerChangeToBat(bool flag)
    {
        isPlayerBat = flag;
    }

    void Update()
    {
        if (isPlayerBat)
        {
            globalVolume.weight = Mathf.Min(globalVolume.weight + Time.deltaTime, 1f);
        }
        else
        {
            globalVolume.weight = Mathf.Max(globalVolume.weight - Time.deltaTime, 0f);
        }
    }

    

    private void OnDisable()
    {
        player.ChangeToBatEvent -= OnPlayerChangeToBat;
    }
}
