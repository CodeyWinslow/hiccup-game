using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public EnemyHealth enemyPrefab;

    [SerializeField]
    float spawnTime;

    float nextSpawnTime;
    int objsInCollider;

    private void Awake()
    {
        objsInCollider = 0;
    }

    private void Start()
    {
        SpawnEnemy();
        nextSpawnTime = Time.time + spawnTime;
    }

    void FixedUpdate()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objsInCollider++;
    }

    private void OnTriggerExit(Collider other)
    {
        objsInCollider--;
        if (objsInCollider < 0) objsInCollider = 0;
    }

    private void SpawnEnemy()
    {
        if (objsInCollider == 0)
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
