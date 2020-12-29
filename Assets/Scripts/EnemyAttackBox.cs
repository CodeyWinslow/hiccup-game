using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackBox : AttackBox
{
    public UnityAction PlayerInRange;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            PlayerInRange?.Invoke();
    }

    public override void Attack(float damage)
    {
        PlayerHealth health;
        foreach (GameObject o in inRange)
        {
            if ((health = o.GetComponent<PlayerHealth>()))
            {
                health.Damage(damage);
            }
        }
    }
}
