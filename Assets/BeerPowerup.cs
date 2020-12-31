using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerPowerup : MonoBehaviour
{
    [SerializeField]
    float points;

    private void OnTriggerEnter(Collider other)
    {
        CombatAttackFoam foam;
        if ((foam = other.GetComponent<CombatAttackFoam>()))
        {
            if (foam.AddFoamPoints(points))
                Destroy(gameObject);
        }
    }
}
