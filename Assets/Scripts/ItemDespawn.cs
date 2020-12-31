using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDespawn : MonoBehaviour
{
    [SerializeField]
    float despawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, despawnTimer);
    }
}
