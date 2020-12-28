using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public float initialHealth;
    CharCombat combat;

    private void Awake()
    {
        combat = GetComponent<CharCombat>();
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
            base.Damage(amount);
            combat.StartTakingDamage();
            Debug.Log("Player health is now " + health);
        }
    }
}
