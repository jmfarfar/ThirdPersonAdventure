using System;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag;
    private Vector3 impact;
    private Vector3 dampingVelocity;
    private float verticalVelocity;
    private float fallingVelocity;
    public event Action fallingEvent;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y;
            fallingVelocity = -1f;
        }
        else
        {
            fallingVelocity += Physics.gravity.y * Time.deltaTime;
            verticalVelocity = fallingVelocity;
            fallingEvent?.Invoke();
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void OnDisable()
    {
        fallingVelocity = 0f;
    }
}
