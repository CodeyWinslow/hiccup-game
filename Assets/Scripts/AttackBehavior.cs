using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    protected HashSet<GameObject> inRange = new HashSet<GameObject>();
    public virtual void Attack(float damage)
    {
        Health health;
        foreach (GameObject o in inRange)
        {
            if ((health = o.GetComponent<Health>()))
            {
                health.Damage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        inRange.Remove(other.gameObject);
    }
}
