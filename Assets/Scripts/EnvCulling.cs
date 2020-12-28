using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCulling : MonoBehaviour
{

    public LayerMask environmentLayerMask;
    public string environmentTag;
    public Transform stopTransform;
    public Material hideMat;

    int envLayer;
    int ignoreLayer;
    GameObject[] envObjs;

    // Start is called before the first frame update
    void Start()
    {
        envLayer = LayerMask.NameToLayer(environmentTag);
        ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
        envObjs = GameObject.FindGameObjectsWithTag(environmentTag);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in envObjs)
        {
            if (obj.layer == ignoreLayer)
            {
                obj.layer = envLayer;
                Cullable c;
                if ((c = obj.GetComponent<Cullable>()))
                {
                    c.Show();
                }
            }
        }

        //raycast to disable things in the way
        Vector3 playerDir = stopTransform.position - transform.position;

        RaycastHit hit;
        while (Physics.Raycast(transform.position, playerDir, out hit, playerDir.magnitude, environmentLayerMask))
        {
            Cullable c = hit.collider.gameObject.GetComponent<Cullable>();
            if (c)
                c.Hide(hideMat);
            hit.collider.gameObject.layer = ignoreLayer;
        }
    }

}
