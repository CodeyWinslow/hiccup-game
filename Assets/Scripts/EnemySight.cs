using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField]
    bool canSeePlayer;

    public bool CanSeePlayer {get => canSeePlayer;}

    //Monobehavior Lifecycle
    private void Awake()
    {
        canSeePlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
            canSeePlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
            canSeePlayer = false;
    }

}
