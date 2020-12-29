using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //State Vars
    [SerializeField]
    public float minMoveSpeed = 5f;
    [SerializeField]
    public float maxMoveSpeed = 5f;

    NavMeshAgent agent;
    Animator anim;
    Transform playerPosition;

    //Monobehavior Lifecycle
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Asserts.AssertNotNull(agent, "Enemy must have NavMeshAgent component");
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Must have an Animator component on Enemy mesh");
    }

    private void Start()
    {
        agent.speed = Random.Range(minMoveSpeed, maxMoveSpeed);
        if (Player.GetPlayer())
        playerPosition = Player.GetPlayer().gameObject.transform;
    }

    void FixedUpdate()
    {
        if (playerPosition != null && agent.enabled)
            agent.SetDestination(playerPosition.position);
    }

    private void Update()
    {
        anim.SetFloat("MoveSpeed", (agent.velocity.magnitude < 1 ? agent.velocity.magnitude : 1));
    }
}
