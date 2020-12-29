using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    protected HashSet<GameObject> inRange = new HashSet<GameObject>();
    public virtual void Attack(float damage)
    {
        Health health;
        foreach (GameObject o in inRange)
        {
            if (o != null &&
                (health = o.GetComponent<Health>()))
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
