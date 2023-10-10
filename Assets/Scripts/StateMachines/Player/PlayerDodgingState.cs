using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private Vector3 dodgeDirection;
    private int dodgingBlendTreeHash = Animator.StringToHash("DodgingBlendTree");
    private int rollForwardHash = Animator.StringToHash("RollForward");
    private int dodgeForwardHash = Animator.StringToHash("DodgeForward");
    private int dodgeRightHash = Animator.StringToHash("DodgeRight");
    private bool canBuffer;
    private int bufferedAttackIndex = -1;
    private bool isRolling;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgeDirection) : base(stateMachine) 
    {
        this.dodgeDirection = dodgeDirection.normalized;
    }

    public override void Enter()
    {
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.InputReader.TargetEvent += OnTarget;
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.Animator.CrossFadeInFixedTime(rollForwardHash, 0.1f);
            isRolling = true;
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(dodgingBlendTreeHash, 0.1f);
        }
    }

    public override void Tick(float deltaTime)
    {
        if(!isRolling)
        {
            FaceTarget();
        }

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime <= 0.8f)
        {
            Move(dodgeDirection * stateMachine.DodgeSpeed, deltaTime);
        }

        if (normalizedTime >= 0.5f)
        {
            canBuffer = true;
        }

        if (bufferedAttackIndex >= 0)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, bufferedAttackIndex));
            return;
        }

        if (normalizedTime >= 0.8f & stateMachine.InputReader.MovementValue.magnitude > 0.5f)
        {
            ReturnToLocomotion();
            return;
        }
        else if (normalizedTime >= 1f)
        {
            ReturnToLocomotion();
            return;
        }

        UpdateAnimator(deltaTime);
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }

        float valueX = Mathf.Round(stateMachine.InputReader.MovementValue.x);
        float valueY = Mathf.Round(stateMachine.InputReader.MovementValue.y);

        stateMachine.Animator.SetFloat(dodgeForwardHash, valueY, 0.05f, deltaTime);
        stateMachine.Animator.SetFloat(dodgeRightHash, valueX, 0.05f, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) & currentInfo.IsTag("Dodge"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    private void OnAttack(int attackIndex)
    {
        bool canJumpAttack = stateMachine.InputReader.MovementValue.y < -0.6f & 
            attackIndex == 1 & 
            stateMachine.Targeter.CurrentTarget != null;
        
        if (canBuffer)
        {
            if (canJumpAttack)
            {
                bufferedAttackIndex = 4;
            }
            else
            {
                bufferedAttackIndex = attackIndex;
            }
        }
    }
    private void OnTarget()
    {
        stateMachine.Targeter.SelectTarget();
    }
}
