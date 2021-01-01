using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIMeter : MonoBehaviour
{
    [SerializeField]
    protected float maxAmount;

    protected IContainsMeter meter;

    // Start is called before the first frame update
    protected void Start()
    {
        meter?.BindMeterChanged(OnMeterChanged);
    }

    protected void OnDestroy()
    {
        meter?.UnbindMeterChanged(OnMeterChanged);
    }

    public virtual void OnMeterChanged(object sender, float amount)
    {
        if (amount == 0)
            transform.localScale = Vector3.zero;

        else
        {
            float normalized = amount / maxAmount;

            Vector3 scale = Vector3.one;
            scale.x = normalized;
            transform.localScale = scale;
        }
    }
}
