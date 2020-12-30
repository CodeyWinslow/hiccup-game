using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombat))]
public class CombatAttackFoam : Attack
{
    [SerializeField]
    float timeToSpawnFoam;

    FoamSpawn spawn;
    FoamEffect effect;
    bool foaming;
    float nextSpawnTime;

    private void Awake()
    {
        foaming = false;

        combat = GetComponent<CharCombat>();
        spawn = GetComponentInChildren<FoamSpawn>();
        Asserts.AssertNotNull(spawn, "Player must have FoamSpawn component");
        effect = GetComponentInChildren<FoamEffect>();
        Asserts.AssertNotNull(effect, "Player must have FoamEffect component");

        AttackPressed += OnAttack;
        AttackReleased += OnRelease;
    }

    private void OnDestroy()
    {
        AttackPressed -= OnAttack;
        AttackReleased -= OnRelease;
    }

    private void FixedUpdate()
    {
        if (foaming && Time.time >= nextSpawnTime)
        {
            SpawnFoam();
            nextSpawnTime = Time.time + timeToSpawnFoam;
        }
    }

    private void SpawnFoam()
    {
        spawn.Spawn();
    }

    public override void OnAttack()
    {
        if (combat.CanAttack)
        {
            combat.CurrentAttack = this;
            combat.CanAttack = false;
            combat.CanMove = false;
            foaming = true;
            nextSpawnTime = Time.time + timeToSpawnFoam;
            effect.StartEffect();
        }
    }

    void OnRelease()
    {
        if (combat.CurrentAttack == this)
        {
            combat.CurrentAttack = null;
            combat.CanAttack = true;
            combat.CanMove = true;
            foaming = false;
            effect.StopEffect();
        }
    }
}
