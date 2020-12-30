using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField]
    float initialHealth;
    [SerializeField]
    float healingCountdown;
    [SerializeField]
    float healingHealthPerSec;

    CharCombat combat;
    float nextHealTime;
    bool healing;

    //Monobehavior Lifecycle
    private void Awake()
    {
        combat = GetComponent<CharCombat>();
        healing = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        HealthChanged();
    }

    private void FixedUpdate()
    {
        if (healing && health < initialHealth)
        {
            Heal();
        }
        else if (Time.time >= nextHealTime)
        {
            healing = true;
            Heal();
        }
    }


    //State Logic
    private void Heal()
    {
        health += healingHealthPerSec * Time.fixedDeltaTime;
        if (health > initialHealth)
            health = initialHealth;
        HealthChanged();
    }

    public override void Damage(float amount)
    {
        if (!combat.TakingDamage)
        {
            base.Damage(amount);
            combat.StartTakingDamage();
            Debug.Log("Player health is now " + health);
            healing = false;
            nextHealTime = Time.time + healingCountdown;
            HealthChanged();
        }
    }
}
