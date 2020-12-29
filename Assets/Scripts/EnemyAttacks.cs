using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAttacks : MonoBehaviour
{
    //State vars
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float timeToDestroyAfterDeath;

    EnemyAttackBox attacking;
    EnemyHealth health;
    NavMeshAgent agent;
    Animator anim;
    bool canAttack = true;
    bool takingDamage = false;
    float nextAttackTime;

    //Attributes
    public bool TakingDamage
    {
        get { return takingDamage; }
    }

    //Monobehavior Lifecycle
    private void Awake()
    {
        attacking = GetComponentInChildren<EnemyAttackBox>();
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

    private void FixedUpdate()
    {
        if (!canAttack && Time.time >= nextAttackTime)
            canAttack = true;
    }

    //State Logic
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
        if (!takingDamage)
        {
            takingDamage = true;
            canAttack = true;
            anim.SetTrigger("Hit");
            agent.enabled = false;
        }
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

    //Animation Events
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

}
