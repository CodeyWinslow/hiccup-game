using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamSpawn : MonoBehaviour
{
    public GameObject foamPrefab;

    [SerializeField]
    Transform spawnPoint;

    public void Spawn()
    {
        Instantiate(foamPrefab, spawnPoint.position, transform.rotation);
    }
}
