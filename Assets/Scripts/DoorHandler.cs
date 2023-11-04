using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private Animator doorPivot;
    [SerializeField] private TextMeshProUGUI alertsText;
    private bool isPlayerNear;
    private bool isDoorOpen;
    private InputReader playerInputReader;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            playerInputReader = other.gameObject.GetComponent<InputReader>();
            playerInputReader.InteractEvent += OpenDoor;
            isPlayerNear = true;
            alertsText.gameObject.SetActive(true);
            alertsText.text = "Press B to open";
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            playerInputReader.InteractEvent -= OpenDoor;
            isPlayerNear = false;
            alertsText.gameObject.SetActive(false);
        }
    }

    private void OpenDoor()
    {
        if (isPlayerNear)
        {
            isDoorOpen = true;
            doorPivot.Play("DoorOpen");
            alertsText.gameObject.SetActive(false);
            playerInputReader.InteractEvent -= OpenDoor;
        }
    }
}
