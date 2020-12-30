using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HurtCircle : MonoBehaviour
{
    [SerializeField]
    float HealthToAppear;

    SpriteRenderer rend;
    Health health;

    // Start is called before the first frame update
    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
        health = GetComponentInParent<Health>();
        Asserts.AssertNotNull(health, "Player must have Health component");
    }

    private void OnDestroy()
    {
        health.Unbind(HealthChanged);
    }

    private void Start()
    {
        health.Bind(HealthChanged);
    }

    public void HealthChanged(float health)
    {
        if (health <= HealthToAppear)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }
    }
}
