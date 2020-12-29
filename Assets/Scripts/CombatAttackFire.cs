using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombat))]
public class CombatAttackFire : Attack
{
    //State vars
    [SerializeField]
    float fireDamagePerSecond;

    CharCombat combat;
    FireAttackBox fire;

    //Monobehavior Lifecycle
    private void Awake()
    {
        combat = GetComponent<CharCombat>();
        fire = GetComponentInChildren<FireAttackBox>();
        Asserts.AssertNotNull(fire, "Player must have FireAttackBox component");

        //subscriptions
        AttackPressed += OnAttack;
        AttackReleased += OnAttackRelease;
        combat.Hurt += OnAttackRelease;
    }

    private void OnDestroy()
    {
        AttackPressed -= OnAttack;
        AttackReleased -= OnAttackRelease;
        combat.Hurt -= OnAttackRelease;
    }

    public override void OnAttack()
    {
        if (combat.CanAttack)
        {
            combat.CurrentAttack = this;
            combat.CanMove = true;
            combat.CanAttack = false;
            fire.Activate(fireDamagePerSecond);
        }
    }

    //State Logic
    void OnAttackRelease()
    {
        if (combat.CurrentAttack == this)
        {
            combat.CurrentAttack = null;
            combat.CanAttack = true;
            fire.Deactivate();
        }
    }
}
