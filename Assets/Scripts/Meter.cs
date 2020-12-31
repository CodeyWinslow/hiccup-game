using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter<T>
{
    protected T _value;

    public virtual T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(this, _value);
        }
    }

    public bool ClampZero { get; set; }

    public event System.EventHandler OnValueZero;
    public event EventHandler<T> OnValueChanged;

    protected void ValueIsZero()
    {
        OnValueZero?.Invoke(this, null);
    }
}

public class FloatMeter : Meter<float>
{
    public float MaxValue = float.MaxValue;
    public override float Value
    {
        get => _value;
        set
        {
            value = Mathf.Min(value, MaxValue);
            base.Value = value;
            if (_value <= 0)
            {
                if (ClampZero)
                    base.Value = 0;
                ValueIsZero();
            }
        }
    }
}

public class IntMeter : Meter<int>
{
    public int MaxValue = int.MaxValue;

    public override int Value
    {
        get => _value;
        set
        {
            value = Mathf.Min(value, MaxValue);
            base.Value = value;
            if (_value <= 0)
            {
                if (ClampZero)
                    base.Value = 0;
                ValueIsZero();
            }
        }
    }
}
