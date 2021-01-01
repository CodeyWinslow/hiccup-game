using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    //State Vars
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float minRunSpeed = 5f;
    [SerializeField]
    float maxRunSpeed = 5f;

    NavMeshAgent agent;
    EnemySight sight;
    Animator anim;
    Transform playerPosition;
    Vector3 defaultPosition;
    bool hostile = false;

    //Monobehavior Lifecycle
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Asserts.AssertNotNull(agent, "Enemy must have NavMeshAgent component");
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Must have an Animator component on Enemy mesh");
        sight = GetComponentInChildren<EnemySight>();
        Asserts.AssertNotNull(sight, "Must have an EnemySight component on Enemy mesh");

        EnemyHealth.WasHurt += OnHurt;
    }

    private void OnDestroy()
    {
        if (!hostile)
            EnemyHealth.WasHurt -= OnHurt;
    }

    private void Start()
    {
        agent.speed = walkSpeed;
        if (Player.GetPlayer())
        playerPosition = Player.GetPlayer().gameObject.transform;
    }

    void FixedUpdate()
    {
        if (agent.enabled)
        {
            if (hostile
                && playerPosition != null
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
                agent.SetDestination(defaultPosition);
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

    public void SetDefaultPosition(Vector3 position)
    {
        defaultPosition = position;
    }

    public void OnHurt(object sender, System.EventArgs e)
    {
        EnemyHealth.WasHurt -= OnHurt;
        agent.speed = Random.Range(minRunSpeed, maxRunSpeed);
        hostile = true;
    }
}
