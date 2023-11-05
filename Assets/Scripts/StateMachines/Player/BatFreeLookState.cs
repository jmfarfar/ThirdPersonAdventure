using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class BatFreeLookState : PlayerBaseState
{
    private float colliderVerticalPosition, colliderHeight, colliderRadius;
    private Vector3 movement;

    Gamepad gamepad => stateMachine.InputReader.CurrentGamepad;
    
    public BatFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.ChangeCharacterEvent += OnChangeCharacterDeborah;
        stateMachine.InputReader.AttackEvent += OnBatAttack;

        stateMachine.ForceReceiver.enabled = false;

        colliderVerticalPosition = stateMachine.CharacterController.center.y;
        colliderHeight = stateMachine.CharacterController.height;
        colliderRadius = stateMachine.CharacterController.radius;

        stateMachine.CharacterController.center = new Vector3(0f, 1.3f);
        stateMachine.CharacterController.height = 0.4f;
        stateMachine.CharacterController.radius = 0.4f;

        base.Enter();

        ChangeModel();

        stateMachine.ChangeToBatEvent?.Invoke(true);
    }

    private void OnBatAttack(int obj)
    {
        stateMachine.SwitchState(new BatBloodCryState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        movement = Fly();

        stateMachine.CharacterController.Move(movement * stateMachine.BatMovementSpeed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            FaceMovementDirection(movement);
        }

        if (gamepad != null)
        {
            stateMachine.BatAnimator.SetFloat("FlightSpeed", 1f + (gamepad.leftStick.ReadValue().magnitude));
        }
    }

    public override void Exit()
    {
        base.Exit();
        ChangeModel();

        stateMachine.ChangeToBatEvent?.Invoke(false);

        stateMachine.CharacterController.center = new Vector3(0f, colliderVerticalPosition);
        stateMachine.CharacterController.height = colliderHeight;
        stateMachine.CharacterController.radius = colliderRadius;

        stateMachine.InputReader.ChangeCharacterEvent -= OnChangeCharacterDeborah;
        stateMachine.InputReader.AttackEvent += OnBatAttack;

        stateMachine.ForceReceiver.enabled = true;

        Vector3 forwardDirection = stateMachine.transform.forward;
        forwardDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(forwardDirection);
    }

    private Vector3 Fly()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        Vector3 movement;

        Gamepad gamepad = stateMachine.InputReader.CurrentGamepad;

        if (gamepad != null)
        {
            Vector2 input = gamepad.leftStick.ReadValue();

            movement = forward * input.y +
                right * input.x;

            if (input.magnitude > 0.1f)
            {
                return movement;
            }
        }

        movement = forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;

        return movement;
    }

    private void ChangeModel()
    {
        stateMachine.deborahModel.SetActive(stateMachine.batModel.activeSelf);
        stateMachine.batModel.SetActive(!stateMachine.deborahModel.activeSelf);
    }
}
