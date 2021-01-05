using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOffset : MonoBehaviour
{
    [SerializeField]
    Vector3 negOffset;
    [SerializeField]
    Vector3 posOffset;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = new Vector3();
        offset.x = Random.Range(negOffset.x, posOffset.x);
        offset.y = Random.Range(negOffset.y, posOffset.y);
        offset.z = Random.Range(negOffset.z, posOffset.z);
        transform.position += offset;
    }
}
