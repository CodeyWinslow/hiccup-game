using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FoamEffect : MonoBehaviour
{
    ParticleSystem part;

    private void Awake()
    {
        part = GetComponent<ParticleSystem>();
    }

    public void StartEffect()
    {
        part.Play();
    }

    public void StopEffect()
    {
        part.Stop();
    }
}
