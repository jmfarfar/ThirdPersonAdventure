using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    public enum States
    {
        ATTACK,
        BLOCK,
        CHASE,
        COMBAT,
        DODGE,
        LOCOMOTION
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
