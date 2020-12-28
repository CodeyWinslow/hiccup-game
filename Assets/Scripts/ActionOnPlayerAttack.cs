using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnPlayerAttack : MonoBehaviour
{

    AttackBehavior att;

    private void Awake()
    {
        CharCombat.PlayerAttack += PlayerAttacked;
        att = GetComponent<AttackBehavior>();
    }

    private void OnDestroy()
    {
        CharCombat.PlayerAttack -= PlayerAttacked;
    }


    public void PlayerAttacked(float damage)
    {
        att?.Attack(damage);
    }
}
