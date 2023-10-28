using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private float timer;
    private Vector3 movement = Vector3.zero;
    private readonly int blockingHash = Animator.StringToHash("Block");
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.currentStateID = PlayerStateMachine.States.BLOCK;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.CrossFadeInFixedTime(blockingHash, 0.1f);
    }


    public override void Tick(float deltaTime)
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            movement = CalculateTargetingMovement();
            FaceTarget();
        }
        else
        {
            movement = CalculateFreeLookMovement();
            if (stateMachine.InputReader.MovementValue != Vector2.zero)
            {
                FaceMovementDirection(movement);
            }
        }

        Move(movement * stateMachine.BlockingMovementSpeed, deltaTime);

        timer += deltaTime * 4;
        stateMachine.Health.SetBlocking(true, Math.Clamp(timer, 0.1f, 0.7f));

        if(!stateMachine.InputReader.isBlocking)
        {
            ReturnToLocomotion();
        }
    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.Health.SetBlocking(false, 1f);
    }

    private void OnDodge()
    {
        if (movement == Vector3.zero) { return; }
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, movement));
    }
    private void OnTarget()
    {
        stateMachine.Targeter.SelectTarget();
    }
}
