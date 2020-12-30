using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamAttackBox : MonoBehaviour
{
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
        }
    }
}
