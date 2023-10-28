using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeState : EnemyAttackingState
{

    private  int AttackHash;
    private string attackName;
       private int randomNumber;

   //private  int AttackHash = Animator.StringToHash("Attack");

    private const float TransitionDuration = 0.1f;

    public EnemyMeleeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

   public override void Enter()
   {
   randomNumber = Random.Range(0, 2);
    if(randomNumber ==0)
    {
        attackName ="Attack1";
       AttackHash  = Animator.StringToHash(attackName);
    }
    else if(randomNumber ==1)
    {
        attackName ="Attack2";
       AttackHash  = Animator.StringToHash(attackName);
    }

        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
   }

   public override void Tick(float deltaTime)
   {
         if (GetNormalizedTime(stateMachine.Animator,attackName) >=1)
        {

            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

        FacePlayer();   
   }

   public override void Exit()
   {
    
   }

     protected float GetNormalizedTime(Animator animator, string attack)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if(animator.IsInTransition(0) && nextInfo.IsTag(attack))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(attack))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    

}
