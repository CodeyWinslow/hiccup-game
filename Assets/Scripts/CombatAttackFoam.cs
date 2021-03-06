﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombat))]
public class CombatAttackFoam : Attack, IContainsMeter
{
    [SerializeField]
    float timeToSpawnFoam;
    [SerializeField]
    float foamCost;
    [SerializeField]
    float maxFoamPoints;

    FloatMeter foamMeter;
    FoamSpawn spawn;
    FoamEffect effect;
    bool foaming;
    float nextSpawnTime;

    private void Awake()
    {
        foaming = false;

        foamMeter = new FloatMeter();
        foamMeter.ClampZero = true;
        foamMeter.MaxValue = maxFoamPoints;
        foamMeter.Value = 0;

        combat = GetComponent<CharCombat>();
        spawn = GetComponentInChildren<FoamSpawn>();
        Asserts.AssertNotNull(spawn, "Player must have FoamSpawn component");
        effect = GetComponentInChildren<FoamEffect>();
        Asserts.AssertNotNull(effect, "Player must have FoamEffect component");

        foamMeter.OnValueZero += OnMeterZero;
        combat.Hurt += AttackReleased;
    }

    private void OnDestroy()
    {
        foamMeter.OnValueZero -= OnMeterZero;
        combat.Hurt -= AttackReleased;
    }

    private void FixedUpdate()
    {
        if (foaming && Time.time >= nextSpawnTime)
        {
            SpawnFoam();
            nextSpawnTime = Time.time + timeToSpawnFoam;
            foamMeter.Value -= foamCost;
        }
    }

    private void SpawnFoam()
    {
        spawn.Spawn();
    }

    public override void OnAttack()
    {

        if (combat.CanAttack && foamMeter.Value >= foamCost)
        {
            //combat.CurrentAttack = this;
            combat.CanAttack = false;
            combat.CanMove = false;
            foaming = true;
            nextSpawnTime = Time.time + timeToSpawnFoam;
            effect.StartEffect();
        }
    }

    public override void AttackPressed()
    {
        OnAttack();
    }

    public bool AddFoamPoints(float points)
    {
        if (foamMeter.Value == foamMeter.MaxValue)
            return false;

        foamMeter.Value += points;
        return true;
    }

    public override void AttackReleased()
    {
        if (combat.CurrentAttack == this)
        {
            combat.CanAttack = true;
            combat.CanMove = true;
            foaming = false;
            effect.StopEffect();
        }
    }

    void OnMeterZero(object sender, EventArgs e)
    {
        AttackReleased();
    }

    public void BindMeterChanged(EventHandler<float> handler)
    {
        foamMeter.OnValueChanged += handler;
        handler(this, foamMeter.Value);
    }

    public void UnbindMeterChanged(EventHandler<float> handler)
    {
        foamMeter.OnValueChanged -= handler;
    }
}
