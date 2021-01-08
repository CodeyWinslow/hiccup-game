using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffComponentsOnDeath : MonoBehaviour
{
    [SerializeField]
    Health whoDies;
    [SerializeField]
    MonoBehaviour[] components;

    // Start is called before the first frame update
    void Awake()
    {
        if (whoDies == null)
            enabled = false;

        whoDies.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        if (whoDies)
            whoDies.OnDeath -= OnDeath;
    }

    void OnDeath()
    {
        foreach (MonoBehaviour behavior in components)
            behavior.enabled = false;
    }
}
