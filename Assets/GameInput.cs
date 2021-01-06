using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //Singleton
    static GameInput instance;

    public static GameInput GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameInput>();
            if (instance == null)
            {
                GameObject container = new GameObject("GameInput");
                instance = container.AddComponent<GameInput>();
            }
        }

        return instance;
    }

    public float GetHorizontalMovement()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            return Input.GetAxis("ControllerHorizontal");
        }

        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalMovement()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            return Input.GetAxis("ControllerVertical");
        }

        return Input.GetAxis("Vertical");
    }

    public float GetTargetAxis()
    {
        if (Input.GetAxis("Target") == 0)
        {
            return Input.GetAxis("ControllerTarget");
        }

        return Input.GetAxis("Target");
    }

    public bool GetAttackButtonDown()
    {
        if (!Input.GetButtonDown("Attack"))
        {
            return Input.GetButtonDown("ControllerAttack");
        }

        return Input.GetButtonDown("Attack");
    }

    public bool GetAttackButtonUp()
    {
        if (!Input.GetButtonUp("Attack"))
        {
            return Input.GetButtonUp("ControllerAttack");
        }

        return Input.GetButtonUp("Attack");
    }

    public bool GetSwitchAttackButtonDown()
    {
        if (!Input.GetButtonDown("SwitchAttack"))
        {
            return Input.GetButtonDown("ControllerSwitchAttack");
        }

        return Input.GetButtonDown("SwitchAttack");
    }

    public bool GetSwitchTargetDown()
    {
        if (!Input.GetButtonDown("SwitchTarget"))
        {
            return Input.GetButtonDown("ControllerSwitchTarget");
        }

        return Input.GetButtonDown("SwitchTarget");
    }
}
