using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombat))]
public class CombatAttackPunch : Attack
{
    //State vars
    [SerializeField]
    float punchDamage;

    Animator anim;
    AttackBox att;

    //Monobehavior Lifecycle
    void Awake()
    {
        combat = GetComponent<CharCombat>();
        anim = GetComponentInChildren<Animator>();
        att = GetComponentInChildren<AttackBox>();
        Asserts.AssertNotNull(att, "Player must have an AttackBox component");
        //AttackPressed += OnAttack;
    }

    private void OnDestroy()
    {
        //AttackPressed -= OnAttack;
    }

    //State Logic
    public override void OnAttack()
    {
        if (combat.CanAttack)
        {
            //combat.CurrentAttack = this;
            combat.CanAttack = false;
            combat.CanMove = false;
            anim.SetTrigger("AttackPunch");
        }
    }

    public override void AttackPressed()
    {
        OnAttack();
    }

    public override void AttackReleased()
    {
    }

    //Animation Events
    public void FemalePunchHit()
    {
        att.Attack(punchDamage);
    }

    public void FemalePunchEnd()
    {
        if (combat.CurrentAttack == this)
        {
            combat.CanAttack = true;
            combat.CanMove = true;
        }
    }
}
