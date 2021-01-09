using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResume : MonoBehaviour
{
    public void OnButtonPress()
    {
        GameController.GetInstance().Unpause();
    }
}
