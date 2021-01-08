using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnDeath : MonoBehaviour
{
    [SerializeField]
    Health whoDies;

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
        gameObject.SetActive(false);
    }
}
