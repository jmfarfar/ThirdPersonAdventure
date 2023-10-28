using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public int lightAttackComboIndex { get; private set; } = -1;
    [field: SerializeField] public int heavyAttackComboIndex { get; private set; } = -1;
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float StunTime { get; private set; }
    [field: SerializeField] public Vector3 Movement { get; private set; }
    [field: SerializeField] public bool IsMovementActive { get; private set; }

    public void SetMovement(bool isMovementActive)
    {
        this.IsMovementActive = isMovementActive;
    }
}
