using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField]
    float initialHealth;

    EnemyAttacks combat;

    public static event EventHandler WasHurt;

    //Monobehavior Lifecycle
    private void Awake()
    {
        combat = GetComponent<EnemyAttacks>();
        Asserts.AssertNotNull(combat, "Enemy must have EnemyAttacks component");
    }

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        HealthChanged();
    }

    //State Logic
    public override void Damage(float amount)
    {
        WasHurt?.Invoke(this, null);

        if (!combat.TakingDamage)
        {
            combat.StartTakingDamage();
            base.Damage(amount);
            HealthChanged();
        }
    }
}
