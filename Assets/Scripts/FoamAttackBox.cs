using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamAttackBox : MonoBehaviour
{
    HashSet<Slowable> releaseOnDestroy = new HashSet<Slowable>();

    private void OnDestroy()
    {
        foreach (Slowable s in releaseOnDestroy)
        {
            if (s) s.Unslow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Slowable s;
        if ((s = other.GetComponent<Slowable>()))
            releaseOnDestroy.Add(s);
    }

    private void OnTriggerStay(Collider other)
    {
        Slowable s;
        if ((s = other.GetComponent<Slowable>()))
        {
            s.Slow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Slowable s;
        if ((s = other.GetComponent<Slowable>()))
        {
            s.Unslow();
            releaseOnDestroy.Remove(s);
        }
    }
}
