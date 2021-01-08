using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterPlayerDead : MonoBehaviour
{
    [SerializeField]
    GameObject deadMessage;

    GameInput input;
    bool dead;

    private void Awake()
    {
        input = GameInput.GetInstance();
        dead = false;
    }

    private void Update()
    {
        if (dead && input.GetAttackButtonDown())
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnceDead()
    {
        dead = true;
        if (deadMessage)
            deadMessage.SetActive(true);
    }
}
