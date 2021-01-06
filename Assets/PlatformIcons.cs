using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIcons : MonoBehaviour
{
    [SerializeField]
    GameObject[] controllerIcons;
    [SerializeField]
    GameObject[] pcIcons;

    PlatformChoice currentPlatform;

    private void Awake()
    {
        currentPlatform = 0;
        ChangePlatform(PlatformChoice.PC);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlatform == PlatformChoice.Controller
         && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0
            || Mathf.Abs(Input.GetAxis("Vertical")) > 0
            || Mathf.Abs(Input.GetAxis("Target")) > 0
            || Input.GetButtonDown("Attack")
            || Input.GetButtonDown("SwitchAttack")
            || Input.GetButtonDown("SwitchTarget")))
        {
            ChangePlatform(PlatformChoice.PC);
        }
        else if (currentPlatform == PlatformChoice.PC
            && (Mathf.Abs(Input.GetAxis("ControllerHorizontal")) > 0
            || Mathf.Abs(Input.GetAxis("ControllerVertical")) > 0
            || Mathf.Abs(Input.GetAxis("ControllerTarget")) > 0
            || Input.GetButtonDown("ControllerAttack")
            || Input.GetButtonDown("ControllerSwitchAttack")
            || Input.GetButtonDown("ControllerSwitchTarget"))
            )
        {
            ChangePlatform(PlatformChoice.Controller);
        }
    }

    public void ChangePlatform(PlatformChoice choice)
    {
        if (choice != currentPlatform)
        {
            if (choice == PlatformChoice.PC)
            {
                foreach (GameObject g in pcIcons)
                    g.SetActive(true);

                foreach (GameObject g in controllerIcons)
                    g.SetActive(false);
            }
            else if (choice == PlatformChoice.Controller)
            {
                foreach (GameObject g in controllerIcons)
                    g.SetActive(true);

                foreach (GameObject g in pcIcons)
                    g.SetActive(false);
            }

            currentPlatform = choice;
        }
    }

    public enum PlatformChoice
    {
        PC = 1,
        Controller
    }
}
