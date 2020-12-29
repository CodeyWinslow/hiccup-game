using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //State Vars
    protected float health;

    //Events
    public UnityAction OnDeath;

    //State Logic
    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) OnDeath?.Invoke();
    }
}
