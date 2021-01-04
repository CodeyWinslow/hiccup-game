using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public GameObject targetedGraphic;

    public Transform targetTransform { get; private set; }

    private void Awake()
    {
        targetTransform = transform;
    }

    public void OnTargetChanged(object sender, EnemyTargeting.TargetEventArgs e)
    {
        if (e.target == this)
        {
            if (e.isTargeting)
                Target();
            else
                UnTarget();
        }
        else
        {
            UnTarget();
        }
    }

    void Target()
    {
        targetedGraphic?.SetActive(true);
    }

    void UnTarget()
    {
        targetedGraphic?.SetActive(false);
    }
}
