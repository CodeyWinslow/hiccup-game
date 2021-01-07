using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamBuyStation : BuyStation
{
    [SerializeField]
    float powerup;

    protected override void Awake()
    {
        base.Awake();
        Buy += OnBuy;
    }

    void OnBuy(object sender, System.EventArgs e)
    {
        if (p)
        {
            CombatAttackFoam f = p.GetComponent<CombatAttackFoam>();
            if (f)
                f.AddFoamPoints(powerup);
        }
    }
}
