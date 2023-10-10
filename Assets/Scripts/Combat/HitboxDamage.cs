using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    [SerializeField] private Collider myColiider;
    private List<Collider> alreadyCollidedWith = new List<Collider>();
    private int damage;
    private float stunTime;
    public event Action HitEvent;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myColiider) { return; }
        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage, stunTime);
            HitEvent?.Invoke();
        }
    }

    public void SetAttack(int damage, float stunTime)
    {
        this.damage = damage;
        this.stunTime = stunTime;
    }
}
