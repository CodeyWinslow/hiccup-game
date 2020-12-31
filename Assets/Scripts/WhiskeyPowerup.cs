using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskeyPowerup : MonoBehaviour
{
    [SerializeField]
    float points;

    private void OnTriggerEnter(Collider other)
    {
        CombatAttackFire fire;
        if ((fire = other.GetComponent<CombatAttackFire>()))
        {
            if (fire.AddFirePoints(points))
                Destroy(gameObject);
        }
    }
}
