using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAttacks : MonoBehaviour
{
    public float attackDamage;
    public float attackDelay;
    public float timeToDestroyAfterDeath;

    EnemyAttackBehavior attacking;
    EnemyHealth health;
    NavMeshAgent agent;
    Animator anim;
    bool canAttack = true;
    bool takingDamage = false;
    float nextAttackTime;

    public bool TakingDamage
    {
        get { return takingDamage; }
    }

    private void Awake()
    {
        attacking = GetComponentInChildren<EnemyAttackBehavior>();
        Asserts.AssertNotNull(attacking, "Enemy must have EnemyAttackBehavior component");
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Must have an Animator component on Enemy mesh");
        agent = GetComponent<NavMeshAgent>();
        Asserts.AssertNotNull(agent, "Enemy must have NavMeshAgent component");
        health = GetComponent<EnemyHealth>();
        Asserts.AssertNotNull(health, "Enemy must have EnemyHealth component");

        attacking.PlayerInRange += WhenInRange;
        health.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        attacking.PlayerInRange -= WhenInRange;
        health.OnDeath -= OnDeath;
    }

    // Update is called once per frame
    void WhenInRange()
    {
        if (canAttack && !takingDamage)
        {
            agent.enabled = false;
            nextAttackTime = Time.time + attackDelay;
            canAttack = false;
            anim.SetTrigger("Attack");
        }
    }

    public void StartTakingDamage()
    {
        takingDamage = true;
        canAttack = true;
        anim.SetTrigger("Hit");
    }

    public void OnDeath()
    {
        agent.enabled = false;
        canAttack = false;
        anim.SetTrigger("Die");
        attacking.enabled = false;
        health.enabled = false;
        GameObject.Destroy(gameObject, timeToDestroyAfterDeath);
    }

    public void PunchHit()
    {
        attacking.Attack(attackDamage);
        agent.enabled = true;
    }

    public void HitEnd()
    {
        takingDamage = false;
        agent.enabled = true;
    }

    private void FixedUpdate()
    {
        if (!canAttack && Time.time >= nextAttackTime)
            canAttack = true;
    }
}
