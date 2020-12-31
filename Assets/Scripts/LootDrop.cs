using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class LootDrop : MonoBehaviour
{
    [SerializeField]
    GameObject[] drops;
    [SerializeField]
    [Range(0.01f, 1)]
    float chanceOfDrop;

    Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += Drop;
    }

    private void OnDestroy()
    {
        health.OnDeath -= Drop;
    }

    public void Drop()
    {
        if (drops.Length > 0)
        {
            float chance = Random.Range(0, 1f);
            int item = Random.Range(0, drops.Length);

            if (chance <= chanceOfDrop)
                Instantiate(drops[item], transform.position, Quaternion.identity);
        }
    }
}
