using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    //State Vars
    [SerializeField]
    public float minMoveSpeed = 5f;
    [SerializeField]
    public float maxMoveSpeed = 5f;

    NavMeshAgent agent;
    EnemySight sight;
    Animator anim;
    Transform playerPosition;
    Vector3 originalPosition;

    //Monobehavior Lifecycle
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Asserts.AssertNotNull(agent, "Enemy must have NavMeshAgent component");
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Must have an Animator component on Enemy mesh");
        sight = GetComponentInChildren<EnemySight>();
        Asserts.AssertNotNull(sight, "Must have an EnemySight component on Enemy mesh");
    }

    private void Start()
    {
        originalPosition = transform.position;
        agent.speed = Random.Range(minMoveSpeed, maxMoveSpeed);
        if (Player.GetPlayer())
        playerPosition = Player.GetPlayer().gameObject.transform;
    }

    void FixedUpdate()
    {
        if (agent.enabled)
        {
            if (playerPosition != null
                && sight.CanSeePlayer)
            {
                agent.SetDestination(playerPosition.position);

                //face player
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    Vector3 nextRot = transform.rotation.eulerAngles;
                    float desiredRotation = nextRot.y;
                    desiredRotation = Quaternion.LookRotation(
                        (agent.destination - transform.position).normalized,
                        Vector3.up)
                        .eulerAngles.y;

                    nextRot.y = desiredRotation;
                    Quaternion newRot = Quaternion.Euler(nextRot);

                    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.2f);
                }
            }
            else
            {
                agent.SetDestination(originalPosition);
            }
        }
    }

    private void Update()
    {
        anim.SetFloat("MoveSpeed", (agent.velocity.magnitude < 1 ? agent.velocity.magnitude : 1));
    }

    public override void ChangeSpeed(float speed)
    {
        agent.speed = speed;
    }
}
