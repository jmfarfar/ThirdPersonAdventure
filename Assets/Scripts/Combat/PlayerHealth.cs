using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealth : Health
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float healCooldownTime = 1f;
    [SerializeField] private int autoHealingAmount = 2;
    [SerializeField] private Volume healthOverlay;
    private int health;
    private bool isBlocking;
    private float stunTimeMultiplier;
    public event Action<float> OnStun;
    public event Action OnDie;

    private float healCooldown;
    private bool canHeal;
    public bool IsDead => health == 0; //**eSantiago

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healCooldown += Time.deltaTime;

        UpdateHealthOverlay();
    }

    private IEnumerator StartHealing()
    {
        yield return new WaitUntil(() => healCooldown >= healCooldownTime);
        canHeal = true;

        while (canHeal)
        {
            yield return new WaitForSeconds(1f);
            Heal(autoHealingAmount);
        }
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        Debug.Log(health);
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

        Debug.Log(health);
        AutoHeal();
    }

    private void UpdateHealthOverlay()
    {
        float damagePercentage = 1f - ((float)health / (float)maxHealth);
        healthOverlay.weight = Mathf.Lerp(healthOverlay.weight, damagePercentage, Time.deltaTime * 10f);
    }

    private void AutoHeal()
    {
        StopAllCoroutines();
        healCooldown = 0f;
        canHeal = false;
        StartCoroutine(StartHealing());
    }

    public void SetBlocking(bool isBlocking, float stunTimeMultiplier)
    {
        this.isBlocking = isBlocking;
        this.stunTimeMultiplier = stunTimeMultiplier;
    }
}
