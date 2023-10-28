using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState : State
{
    protected CinemachineGroupComposer gComposer;
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        stateMachine.IsIdle = true;
    }

    public override void Exit()
    {
        stateMachine.IsIdle = false;
    }

    protected void OnChangeCharacterBat()
    {
        stateMachine.SwitchState(new BatFreeLookState(stateMachine));
    }

    protected void OnChangeCharacterDeborah()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    protected void SetCameraControls()
    {
        gComposer = stateMachine.TargetingCamera.GetCinemachineComponent<CinemachineGroupComposer>();
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    protected Vector3 CalculateFreeLookMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

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

    protected Vector3 CalculateTargetingMovement()
    {
        Gamepad gamepad = stateMachine.InputReader.CurrentGamepad;

        if (gamepad != null)
        {
            Vector2 input = gamepad.leftStick.ReadValue();
            Vector3 move = stateMachine.transform.forward * input.y +
                stateMachine.transform.right * input.x;

            if (input.magnitude > 0.1f)
            {
                return move;
            }
        }

        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    protected void FaceMovementDirection(Vector3 movement)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            Time.deltaTime * stateMachine.RotationDamping);
    }

    protected void CameraMovement(float deltaTime)
    {
        var gamepad = Gamepad.current;

        if (gamepad == null) { return; }

        Vector2 leftStickInput = gamepad.rightStick.ReadValue() * deltaTime * 4;

        gComposer.m_TrackedObjectOffset.y = Math.Clamp(gComposer.m_TrackedObjectOffset.y + leftStickInput.y, 0, 1.5f);
    }
}
