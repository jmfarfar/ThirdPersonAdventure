using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToogleRagdoll(true);
        stateMachine.CharacterController.enabled = false;
        stateMachine.Animator.enabled = false;
        GameObject.Destroy(stateMachine.Health);
    }
    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }

}
