using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //State Vars
    [SerializeField]
    protected float health;

    //Events
    public UnityAction OnDeath;
    public event FloatEventHandler OnHealthChanged;

    //State Logic
    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) OnDeath?.Invoke();
        HealthChanged();
    }

    protected void HealthChanged()
    {
        OnHealthChanged?.Invoke(health);
    }

    public void Bind(FloatEventHandler onHealthChanged)
    {
        OnHealthChanged += onHealthChanged;
        OnHealthChanged(health);
    }

    public void Unbind(FloatEventHandler listener)
    {
        OnHealthChanged -= listener;
    }
}
