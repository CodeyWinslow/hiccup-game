using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour, IAttack
{
    [SerializeField]
    string inputButton;

    protected CharCombat combat;

    public event EventHandler AttackPressed;
    public event EventHandler AttackReleased;

    void Update()
    {
        if (Input.GetButtonDown(inputButton))
            AttackPressed?.Invoke();

        if (Input.GetButtonUp(inputButton))
            AttackReleased?.Invoke();
    }

    public abstract void OnAttack();
}
