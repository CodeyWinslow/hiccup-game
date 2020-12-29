using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField]
    float initialHealth;

    CharCombat combat;

    //Monobehavior Lifecycle
    private void Awake()
    {
        combat = GetComponent<CharCombat>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
    }

    //State Logic
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
