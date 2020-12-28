using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public float initialHealth;

    EnemyAttacks combat;

    private void Awake()
    {
        combat = GetComponent<EnemyAttacks>();
        Asserts.AssertNotNull(combat, "Enemy must have EnemyAttacks component");
    }

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
    }

    public override void Damage(float amount)
    {
        if (!combat.TakingDamage)
        {
            combat.StartTakingDamage();
            base.Damage(amount);
            Debug.Log("Enemy has " + health + " health");
        }
    }
}
