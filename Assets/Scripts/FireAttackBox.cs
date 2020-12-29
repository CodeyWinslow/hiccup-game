using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireAttackBox : MonoBehaviour
{
    //State Vars
    ParticleSystem part;
    float damage;
    bool activated = false;

    //Monobehavior Lifecycle
    void Awake()
    {
        part = GetComponent<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (activated)
        {
            Flammable f = other.GetComponent<Flammable>();
            if (f)
                f.Ignite(damage);
        }
    }

    //State Logic
    public void Activate(float damagePerSec)
    {
        damage = damagePerSec;
        activated = true;
        part.Play();
    }

    public void Deactivate()
    {
        activated = false;
        part.Stop();
    }
}
