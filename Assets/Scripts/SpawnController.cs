using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //Singleton
    static SpawnController instance;

    public static SpawnController GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<SpawnController>();
            if (instance == null)
            {
                GameObject container = new GameObject("SpawnController");
                instance = container.AddComponent<SpawnController>();
            }
        }

        return instance;
    }

    [SerializeField]
    string spawnerTag;
    [SerializeField]
    int enemyLimit = 20;

    EnemySpawn[] spawners;

    int numEnemies = 0;

    public event EventHandler TurnOffSpawners;
    public event EventHandler TurnOnSpawners;
    public event EventHandler ForceSpawn;

    private SpawnController()
    {
        
    }

    ~SpawnController()
    {
        foreach (EnemySpawn sp in spawners)
            sp.OnSpawnEnemy -= EnemySpawned;
    }

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(spawnerTag);
        spawners = new EnemySpawn[objs.Length];

        for (int ii = 0; ii < objs.Length; ++ii)
        {
            spawners[ii] = objs[ii].GetComponent<EnemySpawn>();
            spawners[ii].OnSpawnEnemy += EnemySpawned;
        }
    }

    private void Start()
    {
    }

    public void EnemySpawned(object sender, EventArgs e)
    {
        if (++numEnemies >= enemyLimit)
        {
            TurnOffSpawners?.Invoke(this, null);
        }
    }
}
