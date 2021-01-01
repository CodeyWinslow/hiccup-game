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
    bool alive = true;
    bool hostile = false;

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

        if (!hostile)
            attacking.gameObject.SetActive(false);

        health.OnDeath += OnDeath;

        EnemyHealth.WasHurt += WasAttacked;
    }

    private void OnDestroy()
    {
        attacking.PlayerInRange -= WhenInRange;
        health.OnDeath -= OnDeath;

        if (!hostile)
            EnemyHealth.WasHurt -= WasAttacked;
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
        if (alive && !takingDamage)
        {
            takingDamage = true;
            canAttack = true;
            anim.SetTrigger("Hit");
            agent.enabled = false;
        }
    }

    public void OnDeath()
    {
        alive = false;
        agent.enabled = false;
        canAttack = false;
        anim.SetTrigger("Die");
        attacking.enabled = false;
        health.enabled = false;
        GetComponent<CharacterController>().enabled = false;
        GameObject.Destroy(gameObject, timeToDestroyAfterDeath);
    }

    public void WasAttacked(object sender, System.EventArgs e)
    {
        EnemyHealth.WasHurt -= WasAttacked;
        TurnHostile();
    }

    public void TurnHostile()
    {
        hostile = true;
        attacking.gameObject.SetActive(true);
        anim.SetBool("Fighting", true);
    }

    //Animation Events
    public void PunchHit()
    {
        if (alive)
        {
            attacking.Attack(attackDamage);
            agent.enabled = true;
        }
    }

    public void HitEnd()
    {
        if (alive)
        {
            takingDamage = false;
            agent.enabled = true;
        }
    }

}
