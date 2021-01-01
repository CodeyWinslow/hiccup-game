using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFoamMeter : UIMeter
{
    [SerializeField]
    CombatAttackFoam foam;

    private void Awake()
    {
        meter = foam;
    }
}
