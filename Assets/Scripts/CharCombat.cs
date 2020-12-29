/*************************************************
 * CharCombat Component handles combat logic for character.
 * It is used to manage the combat state and controls
 * what the play is able to do with regards to combat.
 * It does not deliver damage to others or manage player
 * health
 */
using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharCombat : MonoBehaviour
{
    //State vars
    [SerializeField]
    private float punchDamage;
    [SerializeField]
    private float fireDamage;

    private Attack[] attacks;
    FireAttackBox fire;
    private Animator anim;
    private bool takingDamage;
    bool canAttack;
    Attack currentAttack;

    //Attributes
    public bool CanAttack
    {
        get => canAttack && !takingDamage;
        set => canAttack = value;
    }

    public bool CanMove {get; set;}

    public bool TakingDamage
    {
        get { return takingDamage; }
    }

    public Attack CurrentAttack
    {
        get => currentAttack;
        set => currentAttack = value;
    }

    //Events
    public event EventHandler Hurt;

    //Monobehavior Lifecycle
    private void Awake()
    {
        attacks = GetComponents<Attack>();
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Player must have Animator component");

        CanMove = true;
        CanAttack = true;
        currentAttack = null;
        takingDamage = false;
    }

    //State Logic
    public void StartTakingDamage()
    {
        Hurt?.Invoke();
        takingDamage = true;
        anim.SetTrigger("Hit");
    }

    //Animation Events
    public void FemaleStaggerEnd()
    {
        takingDamage = false;
    }
}
