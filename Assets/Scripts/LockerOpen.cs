using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerOpen : MonoBehaviour
{
    [SerializeField] GameObject activator;

    public void EnableActivator()
    {
        activator.SetActive(true);
    }
}
