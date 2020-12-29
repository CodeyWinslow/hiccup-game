using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireEffect : MonoBehaviour
{
    Flammable flame;
    ParticleSystem part;

    //Monobehavior Lifecycle
    void Awake()
    {
        part = GetComponent<ParticleSystem>();
        flame = GetComponentInParent<Flammable>();
        Asserts.AssertNotNull(flame, "Enemy must have Flammable component");
        flame.OnIgnite += OnIgnite;
        flame.OnExtinguish += OnExtinguish;
    }

    private void OnDestroy()
    {
        flame.OnExtinguish -= OnExtinguish;
        flame.OnIgnite -= OnIgnite;
    }

    //State Logic
    public void OnIgnite()
    {
        part.Play();
    }

    public void OnExtinguish()
    {
        part.Stop();
    }
}
