using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{

    private  int AttackHash = Animator.StringToHash("Attack");

    private const float TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
           switch (stateMachine.EnemyType)
            {
                case EnemyType.NECROMANCER :
stateMachine.SwitchState(new EnemyNecromancerState(stateMachine,stateMachine.EnemyType ));
                    break;
                case EnemyType.FIGHTER:
stateMachine.SwitchState(new EnemyMeleeState(stateMachine));
                    break;
                case EnemyType.GUNMAN:
stateMachine.SwitchState(new EnemyGunmanState(stateMachine));
                    break;
                default:
                    break;
            }


 //       stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
 //       stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
 /*       if (GetNormalizedTime(stateMachine.Animator) >=1)
        {

            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

        FacePlayer();*/
    }

    public override void Exit()
    {

    }

}
