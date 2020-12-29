﻿using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    [SerializeField]
    float timeToExtinguish;

    Health health;
    float damageToTake;
    bool ablaze = false;

    float damageTime;
    float extinguishTime;

    //Events
    public event EventHandler OnIgnite;
    public event EventHandler OnDoDamage;
    public event EventHandler OnExtinguish;

    //Monobehavior Lifecycle
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        if (ablaze)
        {
            if (Time.time >= extinguishTime)
            {
                Extinguish();
            }
            else if (Time.time >= damageTime)
            {
                DoDamage();
            }
        }
    }

    //State Logic
    public void Ignite(float damagePerSec)
    {
        damageToTake = Mathf.Max(damagePerSec, damageToTake);
        extinguishTime = Time.time + timeToExtinguish;
        health.Damage(damageToTake);
        if (!ablaze)
            damageTime = Time.time + 1; //take damage every second

        ablaze = true;

        OnIgnite?.Invoke();
    }

    void Extinguish()
    {
        ablaze = false;
        damageToTake = 0;

        OnExtinguish?.Invoke();
    }

    void DoDamage()
    {
        health.Damage(damageToTake);
        damageTime = Time.time + 1;

        OnDoDamage?.Invoke();
    }
}
