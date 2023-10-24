using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float animatorDampTime = 0.05f;
    private Vector3 movement;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.currentStateID = PlayerStateMachine.States.LOCOMOTION;
        base.Enter();
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.Animator.CrossFadeInFixedTime(freeLookBlendTreeHash, 0.1f);
    }


    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.isBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        movement = CalculateFreeLookMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        float movementSpeed = movement.magnitude;

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(freeLookSpeedHash, 0, animatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(freeLookSpeedHash, movementSpeed, animatorDampTime, deltaTime);
        FaceMovementDirection(movement);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    private void OnDodge()
    {
        if (movement == Vector3.zero) { return; }
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, movement));
    }

    private void OnAttack(int attackIndex)
    {
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attackIndex));
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}
