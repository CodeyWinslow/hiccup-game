using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuyStation : BuyStation
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
            CombatAttackFire f = p.GetComponent<CombatAttackFire>();
            if (f)
                f.AddFirePoints(powerup);
        }
    }
}
