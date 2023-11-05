using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInputListener : MonoBehaviour, Controls.IUIActions
{
    public UnityEvent backButtonPress;
    private Controls controls;

    private void Awake() 
    {
        controls = new Controls();
        controls.UI.Enable();
        controls.UI.SetCallbacks(this);
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        backButtonPress.Invoke();
    }
}
