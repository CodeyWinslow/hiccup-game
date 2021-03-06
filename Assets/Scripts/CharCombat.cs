﻿/*************************************************
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
    SpriteRenderer attackIndicator;
    [SerializeField]
    string attackButton;
    [SerializeField]
    string switchButton;

    private Attack[] attacks;
    private Animator anim;
    GameInput input;
    private bool takingDamage;
    bool canAttack;
    int attackIndex;
    Attack currentAttack;

    //Attributes
    public bool CanAttack
    {
        get => canAttack && !takingDamage;
        set => canAttack = value;
    }

    public bool CanMove { get; set; }

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
    public event System.EventHandler<Attack> SwitchedAttack;

    //Monobehavior Lifecycle
    private void Awake()
    {
        attacks = GetComponents<Attack>();
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Player must have Animator component");
        input = GameInput.GetInstance();
        Asserts.AssertNotNull(input, "Scene must have GameInput script");

        CanMove = true;
        CanAttack = true;
        attackIndex = 0;
        currentAttack = null;
        if (attacks.Length >= 0)
        {
            currentAttack = attacks[attackIndex];
            SetAttackSprite(currentAttack.Indicator);
        }
        takingDamage = false;
    }

    private void Update()
    {
        if (input.GetAttackButtonDown())
            currentAttack?.AttackPressed();

        if (input.GetAttackButtonUp())
            currentAttack?.AttackReleased();

        if (input.GetSwitchAttackButtonDown()
            && CanAttack)
            SwitchNextAttack();
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

    public void SwitchNextAttack()
    {
        if (++attackIndex >= attacks.Length)
            attackIndex = 0;

        if (attacks.Length >= 0)
        {
            currentAttack = attacks[attackIndex];
            SetAttackSprite(currentAttack.Indicator);
            SwitchedAttack?.Invoke(this, currentAttack);
        }
    }

    public void SetAttackSprite(Sprite sprite)
    {
        if (attackIndicator)
            attackIndicator.sprite = sprite;
    }
}
