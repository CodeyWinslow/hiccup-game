using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombat))]
public class CombatAttackFire : Attack, IContainsMeter
{
    //State vars
    [SerializeField]
    float fireDamagePerSecond;
    [SerializeField]
    float fireCostPerSecond;
    [SerializeField]
    float maxFirePoints;

    FloatMeter fireMeter;
    FireAttackBox fire;
    bool attacking;

    //Monobehavior Lifecycle
    private void Awake()
    {
        attacking = false;

        fireMeter = new FloatMeter();
        fireMeter.ClampZero = true;
        fireMeter.MaxValue = maxFirePoints;
        fireMeter.Value = 0;

        combat = GetComponent<CharCombat>();
        fire = GetComponentInChildren<FireAttackBox>();
        Asserts.AssertNotNull(fire, "Player must have FireAttackBox component");

        //subscriptions
        //AttackPressed += OnAttack;
        //AttackReleased += OnAttackRelease;
        combat.Hurt += AttackReleased;
        fireMeter.OnValueZero += OnMeterZero;
    }

    private void OnDestroy()
    {
        //AttackPressed -= OnAttack;
        //AttackReleased -= OnAttackRelease;
        combat.Hurt -= AttackReleased;
        fireMeter.OnValueZero -= OnMeterZero;
    }

    private void FixedUpdate()
    {
        if (attacking)
            fireMeter.Value -= Time.fixedDeltaTime * fireCostPerSecond;
    }

    //State Logic
    public override void OnAttack()
    {
        if (combat.CanAttack && fireMeter.Value > 0)
        {
            attacking = true;
            combat.CanMove = true;
            combat.CanAttack = false;
            fire.Activate(fireDamagePerSecond);
        }
    }

    public override void AttackPressed()
    {
        OnAttack();
    }

    public override void AttackReleased()
    {
        if (combat.CurrentAttack == this)
        {
            attacking = false;
            combat.CanAttack = true;
            fire.Deactivate();
        }
    }

    void OnMeterZero(object sender, EventArgs e)
    {
        AttackReleased();
    }

    public bool AddFirePoints(float points)
    {
        if (fireMeter.Value == fireMeter.MaxValue)
            return false;

        fireMeter.Value += points;
        return true;
    }

    public void BindMeterChanged(System.EventHandler<float> handler)
    {
        fireMeter.OnValueChanged += handler;
        handler(this, fireMeter.Value);
    }

    public void UnbindMeterChanged(System.EventHandler<float> handler)
    {
        fireMeter.OnValueChanged -= handler;
    }
}
