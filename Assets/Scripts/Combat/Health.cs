using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    private bool isBlocking;
    private float stunTimeMultiplier;
    public event Action<float> OnStun;
    public event Action OnDie;
    public bool IsDead => health == 0; //**eSantiago
    private void Start()
    {
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void DealDamage(int damage, float stunTime)
    {
        if (health <= 0) { return; }

        if (isBlocking)
        {
            OnStun?.Invoke(stunTime * stunTimeMultiplier);
            Debug.Log("Blocked");
            return;
        }

        health = Mathf.Max(health - damage, 0);
        OnStun?.Invoke(stunTime);

        if (health == 0)
        {
            OnDie?.Invoke();
        }

        //Debug.Log(health);
    }

    public void SetBlocking(bool isBlocking, float stunTimeMultiplier)
    {
        this.isBlocking = isBlocking;
        this.stunTimeMultiplier = stunTimeMultiplier;
    }
}
