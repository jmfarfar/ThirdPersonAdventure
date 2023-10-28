using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("ImpactEnemy");
    private float stunTime;
    private const float CrossFadeDuration = 0.1f;

    public EnemyImpactState(EnemyStateMachine stateMachine,float stunTime) : base(stateMachine)
    {
        this.stunTime = stunTime;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);       

        stunTime -=deltaTime;

        if (stunTime <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

}




