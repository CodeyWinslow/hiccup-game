using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    CharCombat combatBehavior;
    Animator anim;
    private Vector3 move;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        Asserts.AssertNotNull(anim, "Character model must have Animator component");
        combatBehavior = GetComponentInParent<CharCombat>();
        Asserts.AssertNotNull(combatBehavior, "Player must have CharCombat component");
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");
        anim.SetFloat("RunSpeed", (move.magnitude < 1 ? move.magnitude : 1));
        if (Input.GetButtonDown("Fire1") && combatBehavior.CanAttack)
            anim.SetTrigger("AttackPunch");
    }
}
