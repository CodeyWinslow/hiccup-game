using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool grounded = false;

    public float timeToDisable = 0.5f;
    public float timeLeft = 0;

    private bool disabled;

    public void TempDisable()
    {
        timeLeft = timeToDisable;
        grounded = false;
        disabled = true;
    }

    private void FixedUpdate()
    {
        if (disabled)
        {
            if (timeLeft > 0)
                timeLeft -= Time.deltaTime;
            else
                onReenable();
        }

    }

    private void onReenable()
    {
        disabled = false;
    }

    private void OnTriggerStay(Collider col)
    {
        if (!disabled)
            grounded = true;
    }

    private void OnTriggerExit(Collider col)
    {
        grounded = false;
    }
}
