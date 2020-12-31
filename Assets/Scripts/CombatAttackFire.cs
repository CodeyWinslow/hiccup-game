using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombat))]
public class CombatAttackFire : Attack
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
        AttackPressed += OnAttack;
        AttackReleased += OnAttackRelease;
        combat.Hurt += OnAttackRelease;
        fireMeter.OnValueZero += OnMeterZero;
    }

    private void OnDestroy()
    {
        AttackPressed -= OnAttack;
        AttackReleased -= OnAttackRelease;
        combat.Hurt -= OnAttackRelease;
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
            combat.CurrentAttack = this;
            combat.CanMove = true;
            combat.CanAttack = false;
            fire.Activate(fireDamagePerSecond);
        }
    }

    void OnAttackRelease()
    {
        if (combat.CurrentAttack == this)
        {
            attacking = false;
            combat.CurrentAttack = null;
            combat.CanAttack = true;
            fire.Deactivate();
        }
    }

    void OnMeterZero(object sender, EventArgs e)
    {
        OnAttackRelease();
    }

    public bool AddFirePoints(float points)
    {
        if (fireMeter.Value == fireMeter.MaxValue)
            return false;

        fireMeter.Value += points;
        return true;
    }

    public void BindFireMeter(System.EventHandler<float> handler)
    {
        fireMeter.OnValueChanged += handler;
        handler(this, fireMeter.Value);
    }

    public void UnbindFireMeter(System.EventHandler<float> handler)
    {
        fireMeter.OnValueChanged -= handler;
    }
}
