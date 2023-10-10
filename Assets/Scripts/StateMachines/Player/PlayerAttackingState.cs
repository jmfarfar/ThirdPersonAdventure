using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private Attack attack;
    private float normalizedTime;
    private bool canBuffer;
    private int bufferedIndex = -1;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
        stateMachine.CurrentAttack = attack;
    }

    public override void Enter()
    {
        stateMachine.currentStateID = PlayerStateMachine.States.ATTACK;
        stateMachine.InputReader.AttackEvent += OnAttack;
        FaceTarget();
        stateMachine.Hitbox.SetAttack(attack.Damage, attack.StunTime);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        HandleMovement(deltaTime);

        normalizedTime = GetNormalizedTime();


        if (normalizedTime < 1f)
        {
            if (bufferedIndex >= 0 & normalizedTime > attack.ComboAttackTime)
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, bufferedIndex));
                return;
            }
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (normalizedTime >= 0.5f)
            {
                canBuffer = true;
            }
        }
        else
        {
            ReturnToLocomotion();
        }

        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    private void HandleMovement(float deltaTime)
    {
        if (attack.IsMovementActive)
        {
            Move(stateMachine.transform.TransformDirection(attack.Movement), deltaTime);
        }
        else
        {
            Move(deltaTime);
        }
    }

    private bool TryComboAttack(float normalizedTime, int attackWeightIndex)
    {
        switch (attackWeightIndex)
        {
            case 0:
                if (attack.lightAttackComboIndex == -1) { return false; }

                if (normalizedTime < attack.ComboAttackTime) { return false; }

                stateMachine.SwitchState(new PlayerAttackingState(stateMachine,attack.lightAttackComboIndex));
                return true;
            case 1:
                if (attack.heavyAttackComboIndex == -1) { return false; }

                if (normalizedTime < attack.ComboAttackTime) { return false; }

                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.heavyAttackComboIndex));
                return true;
        }
        return false;
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }

    private void OnAttack(int attackIndex)
    {
        if(TryComboAttack(normalizedTime, attackIndex)) { return; }
        if(!canBuffer) { return; }
        
        switch(attackIndex)
        {
            case 0:
                bufferedIndex = attack.lightAttackComboIndex;
                break;
            case 1:
                bufferedIndex = attack.heavyAttackComboIndex;
                break;
        }
    }
}
