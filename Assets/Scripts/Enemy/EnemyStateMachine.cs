using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    NECROMANCER,
    FIGHTER,
    GUNMAN,
}

public class EnemyStateMachine : StateMachine
{

    [field: SerializeField] public EnemyType EnemyType { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }        
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }   
    [field: SerializeField] public HitboxDamage Weapon { get; private set; }  
    [field: SerializeField] public Health Health { get; private set; } 
    [field: SerializeField] public Target Target { get; private set; } 
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }    

    [field: SerializeField] public float MovementSpeed { get; private set; }      
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }

    public Health Player { get; private set; }


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Agent.updatePosition = false;
        Agent.updateRotation = false;        

        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
        Health.OnStun += HandleTakeDamage;
        Health.OnDie += HandleDie;        
    }

    private void OnDisable()
    {
        Health.OnStun -= HandleTakeDamage;
        Health.OnDie -= HandleDie;        
    }

    private void HandleTakeDamage(float stunTime)
    {
        SwitchState(new EnemyImpactState(this,stunTime));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

}
