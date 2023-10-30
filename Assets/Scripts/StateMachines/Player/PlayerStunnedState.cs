using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedState : PlayerBaseState
{
    private readonly int impactHash = Animator.StringToHash("Impact");
    private readonly int blockImpactHash = Animator.StringToHash("BlockImpact");
    private float stunTime;
    public PlayerStunnedState(PlayerStateMachine stateMachine, float stunTime) : base(stateMachine)
    {
        this.stunTime = stunTime;
    }

    public override void Enter()
    {
        stateMachine.currentStateID = PlayerStateMachine.States.LOCOMOTION;
        Debug.Log(stateMachine.Health.GetHealth());
        if(stateMachine.InputReader.isBlocking)
        {
            stateMachine.Animator.CrossFadeInFixedTime(blockImpactHash, 0.1f);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(impactHash, 0.1f);
        }
    }
    public override void Tick(float deltaTime)
    {
        stunTime -= deltaTime;

        if(stunTime <= 0)
        {
            ReturnToLocomotion();
        }

        Move(deltaTime);
    }
    public override void Exit()
    {
    }
}
