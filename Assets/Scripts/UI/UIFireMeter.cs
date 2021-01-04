using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFireMeter : UIMeter
{
    [SerializeField]
    CombatAttackFire fire;

    private void Awake()
    {
        meter = fire;
    }
}
