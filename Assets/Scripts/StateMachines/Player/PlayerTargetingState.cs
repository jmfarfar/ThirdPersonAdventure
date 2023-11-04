using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int targetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int targetingRightHash = Animator.StringToHash("TargetingRight");
    private Vector3 movement;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.currentStateID = PlayerStateMachine.States.LOCOMOTION;
        
        SetCameraControls();
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.StartCoroutine(Suscribe());
        stateMachine.Animator.CrossFadeInFixedTime(targetingBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.isBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        movement = CalculateTargetingMovement();

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();

        CameraMovement(deltaTime);
    }

    public override void Exit()
    {
        
        stateMachine.InputReader.AttackEvent -= OnAttack;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }


    private void UpdateAnimator(float deltaTime)
    {
        var gamepad = stateMachine.InputReader.CurrentGamepad;
        if (gamepad != null && gamepad.leftStick.ReadValue() != Vector2.zero)
        {
            Vector2 input = gamepad.leftStick.ReadValue();

            stateMachine.Animator.SetFloat(targetingForwardHash, input.y * 2, 0.03f, deltaTime);
            stateMachine.Animator.SetFloat(targetingRightHash, input.x * 2, 0.03f, deltaTime);
        }
        else
        {
            if (stateMachine.InputReader.MovementValue.y == 0)
            {
                stateMachine.Animator.SetFloat(targetingForwardHash, 0f, 0.03f, deltaTime);
            }
            else
            {
                //float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
                float value = stateMachine.InputReader.MovementValue.y;
                stateMachine.Animator.SetFloat(targetingForwardHash, value, 0.03f, deltaTime);
            }

            if (stateMachine.InputReader.MovementValue.x == 0)
            {
                stateMachine.Animator.SetFloat(targetingRightHash, 0f, 0.03f, deltaTime);
            }
            else
            {
                //float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
                float value = stateMachine.InputReader.MovementValue.x;
                stateMachine.Animator.SetFloat(targetingRightHash, value, 0.03f, deltaTime);
            }
        }
        
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if (movement == Vector3.zero) { return; }
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, movement));
    }

    public IEnumerator Suscribe()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.InputReader.CancelEvent += OnCancel;
    }

    private void OnAttack(int attackIndex)
    {
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attackIndex));
    }
}
