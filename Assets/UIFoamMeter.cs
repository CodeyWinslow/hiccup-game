using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFoamMeter : MonoBehaviour
{
    [SerializeField]
    float maxAmount;

    CombatAttackFoam combat;

    // Start is called before the first frame update
    void Start()
    {
        combat = Player.GetPlayer()?.GetComponent<CombatAttackFoam>();
        Asserts.AssertNotNull(combat, "Player must have a CombatAttackFoam component");
        combat.BindValueChanged(MeterChanged);
    }

    private void OnDestroy()
    {
        combat.BindValueChanged(MeterChanged);
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
