using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using PixelCrushers.DialogueSystem;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public GameObject deborahModel { get; private set; }
    [field: SerializeField] public GameObject batModel { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Animator BatAnimator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public HitboxDamage Hitbox { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera TargetingCamera { get; set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float BlockingMovementSpeed { get; private set; }
    [field: SerializeField] public float BatMovementSpeed { get; private set; }
    [field: SerializeField] public float DodgeSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public QuestLogWindowHotkey QuestLogWindowHotkey { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    public States currentStateID;

    public bool IsIdle { get; set; }
    public Attack CurrentAttack { get; set; }

    public Action<bool> ChangeToBatEvent;

    private void OnEnable()
    {
        ForceReceiver.fallingEvent += OnFalling;
        Health.OnStun += OnStun;
        Health.OnDie += OnDie;
        MainCameraTransform = Camera.main.transform;

        if (Targeter == null) 
        {
            SwitchState(new PlayerFreeLookState(this));
            return; 
        }

        if (Targeter.CurrentTarget != null)
        {
            SwitchState(new PlayerTargetingState(this));
        }
        else
        {
            SwitchState(new PlayerFreeLookState(this));
        }
    }

    public new void Update()
    {
        base.Update();
    }

    private void OnDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    private void OnStun(float stunTime)
    {
        SwitchState(new PlayerStunnedState(this, stunTime));
    }

    private void OnFalling()
    {
        Vector3 currentVelocity = CharacterController.velocity;
        currentVelocity.y = 0f;
        SwitchState(new PlayerFallingState(this, currentVelocity));
    }

    private void OnDisable()
    {
        ForceReceiver.fallingEvent -= OnFalling;
        Health.OnStun -= OnStun;
        Health.OnDie -= OnDie;
    }

    public void ActivateAttackMovement()
    {
        CurrentAttack?.SetMovement(true);
    }

    public void DeactivateAttackMovement()
    {
        CurrentAttack?.SetMovement(false);
    }
}
