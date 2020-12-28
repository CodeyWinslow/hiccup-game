using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Vector3 camForward;

    private void Awake()
    {
        camForward = Vector3.forward;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }

        camForward = (target.position - transform.position).normalized;
        camForward.y = 0;
    }
}
