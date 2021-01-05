using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCulling : MonoBehaviour
{
    [SerializeField]
    float raycastRadius;
    [SerializeField]
    LayerMask environmentLayerMask;
    [SerializeField]
    string environmentTag;
    [SerializeField]
    Transform stopTransform;
    [SerializeField]
    Material hideMat;

    int envLayer;
    int ignoreLayer;
    List<Cullable> envObjs;

    // Start is called before the first frame update
    void Start()
    {
        envLayer = LayerMask.NameToLayer(environmentTag);
        ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");

        GameObject[] taggedItems = GameObject.FindGameObjectsWithTag(environmentTag);
        envObjs = new List<Cullable>(taggedItems.Length);

        Cullable c;
        foreach (GameObject g in taggedItems)
            if ((c = g.GetComponent<Cullable>()))
                envObjs.Add(c);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Cullable obj in envObjs)
        {
            if (obj.gameObject.layer == ignoreLayer)
            {
                obj.gameObject.layer = envLayer;
                obj.Show();
            }
        }
        //raycast to disable things in the way
        Vector3 playerDir = stopTransform.position - transform.position;
        playerDir = playerDir.normalized
                        * (playerDir.magnitude - 
                            (raycastRadius * 1.1f));

        RaycastHit hit;
        while (Physics.SphereCast(transform.position, raycastRadius, playerDir, out hit, playerDir.magnitude, environmentLayerMask))
        {
            Cullable c = hit.collider.gameObject.GetComponent<Cullable>();
            if (c)
                c.Hide(hideMat);
            hit.collider.gameObject.layer = ignoreLayer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Cullable c = other.GetComponent<Cullable>();
        if (c)
            c.Disappear();
    }

    private void OnTriggerExit(Collider other)
    {
        Cullable c = other.GetComponent<Cullable>();
        if (c)
            c.Show();
    }

}
