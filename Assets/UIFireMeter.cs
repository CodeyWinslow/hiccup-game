using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFireMeter : MonoBehaviour
{
    [SerializeField]
    float maxAmount;

    CombatAttackFire combat;

    // Start is called before the first frame update
    void Start()
    {
        combat = Player.GetPlayer()?.GetComponent<CombatAttackFire>();
        Asserts.AssertNotNull(combat, "Player must have a CombatAttackFire component");
        combat.BindFireMeter(MeterChanged);
    }

    private void OnDestroy()
    {
        combat.UnbindFireMeter(MeterChanged);
    }

    public void MeterChanged(object sender, float amount)
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
