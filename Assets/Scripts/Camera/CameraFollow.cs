using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Vector3 camForward;

    private void Start()
    {
        camForward = offset * -1;
        camForward.y = 0;
        camForward.Normalize();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        LockOnTarget();

        //Vector3 toTarget = target.position - transform.position;
        //toTarget.y = 0;
        //camForward = toTarget.normalized;
    }

    void LockOnTarget()
    {
        if (target)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
