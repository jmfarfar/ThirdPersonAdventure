using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private Vector3 momentum;
    private int fallingHash = Animator.StringToHash("Falling");
    public PlayerFallingState(PlayerStateMachine stateMachine, Vector3 momentum) : base(stateMachine)
    {
        this.momentum = momentum;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(fallingHash, 0.1f);

        stateMachine.InputReader.ChangeCharacterEvent += OnChangeCharacterBat;
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.ChangeCharacterEvent -= OnChangeCharacterBat;
    }

}
