using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityAction OnDeath;

    protected float health;
    // Start is called before the first frame update
    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) OnDeath?.Invoke();
    }
}
