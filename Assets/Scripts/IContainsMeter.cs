using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainsMeter
{
    void BindMeterChanged(EventHandler<float> handler);

    void UnbindMeterChanged(EventHandler<float> handler);
}
