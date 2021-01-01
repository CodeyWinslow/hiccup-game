using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public EnemyHealth enemyPrefab;

    [SerializeField]
    float spawnTime;
    [SerializeField]
    Transform[] stopSpots;

    bool spawning;
    float nextSpawnTime;
    int objsInCollider;
    SpawnController controller;

    public event EventHandler OnSpawnEnemy;

    //Monobehavior Lifecycle
    private void Awake()
    {
        spawning = true;
        objsInCollider = 0;
        controller = SpawnController.GetInstance();
        if (controller)
        {
            controller.TurnOnSpawners += OnStartSpawning;
            controller.TurnOffSpawners += OnStopSpawning;
            controller.ForceSpawn += OnForceSpawn;
        }
    }

    private void OnDestroy()
    {
        if (controller)
        {
            controller.TurnOnSpawners -= OnStartSpawning;
            controller.TurnOffSpawners -= OnStopSpawning;
            controller.ForceSpawn -= OnForceSpawn;
        }
    }

    void FixedUpdate()
    {
        if (spawning
            && Time.time >= nextSpawnTime)
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

    //State logic
    private void SpawnEnemy()
    {
        if (objsInCollider == 0)
        {
            EnemyMovement move = Instantiate(enemyPrefab, transform.position, Quaternion.identity)
                                .GetComponent<EnemyMovement>();
            move.SetDefaultPosition(GetDefaultPosition());
            OnSpawnEnemy?.Invoke(this, null);
        }
    }

    private Vector3 GetDefaultPosition()
    {
        if (stopSpots.Length < 1)
            return transform.position;

        int spotInd = UnityEngine.Random.Range(0, stopSpots.Length);
        return stopSpots[spotInd].position;
    }

    public void SetSpawnTime(float time)
    {
        spawnTime = time;
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    public void StartSpawning()
    {
        nextSpawnTime = Time.time - 1;
        spawning = true;
    }

    public void OnStartSpawning(object sender, EventArgs e) => StartSpawning();

    public void OnStopSpawning(object sender, EventArgs e) => StopSpawning();

    public void OnForceSpawn(object sender, EventArgs e) => SpawnEnemy();
}
