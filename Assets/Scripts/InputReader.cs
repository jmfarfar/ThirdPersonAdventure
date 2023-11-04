using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public bool isBlocking { get; private set; }
    public Vector2 MovementValue { get; private set; }
    public Gamepad CurrentGamepad { get; private set; }
    public event Action TargetEvent;
    public event Action CancelEvent;
    public event Action DodgeEvent;
    public event Action<int> AttackEvent;
    public event Action ChangeCharacterEvent;
    public event Action QuestToggleEvent;
    private Controls controls;

    private void OnEnable()
    {
        CurrentGamepad = Gamepad.current;
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        DisableInputReader();
    }

    private void OnDestroy()
    {
        DisableInputReader();
    }

    private void DisableInputReader()
    {
        controls?.Player.Disable();
        CurrentGamepad = null;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.started) { return; }

        CancelEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnAttackLight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackEvent?.Invoke(0);
        }
    }

    public void OnAttackHeavy(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackEvent?.Invoke(1);
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBlocking = true;
        }
        else if (context.canceled)
        {
            isBlocking = false;
        }
    }

    public void OnChangeCharacter(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ChangeCharacterEvent?.Invoke();
        }
    }

    public void OnQuestToggle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            QuestToggleEvent?.Invoke();
        }
    }
}
