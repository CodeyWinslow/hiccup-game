using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharCombat : MonoBehaviour
{
    public static UnityAction<float> PlayerAttack;

    public float punchDamage;
    private bool canAttack = true;
    public bool CanAttack
    {
        get { return (canAttack && !takingDamage); }
    }

    public bool CanMove
    {
        get { return canAttack; }
    }

    private Animator anim;
    private bool takingDamage;
    public bool TakingDamage
    {
        get { return takingDamage; }
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Player must have Animator component");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && CanAttack)
            canAttack = false;
    }

    public void StartTakingDamage()
    {
        takingDamage = true;
        canAttack = true;
        anim.SetTrigger("Hit");
    }

    public void FemalePunchHit()
    {
        PlayerAttack?.Invoke(punchDamage);
    }

    public void FemalePunchEnd()
    {
        canAttack = true;
    }

    public void FemaleStaggerEnd()
    {
        takingDamage = false;
    }
}
