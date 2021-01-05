using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStartMessage : MonoBehaviour
{
    GameController cont;
    bool waitingForAttack;
    // Start is called before the first frame update
    void Start()
    {
        waitingForAttack = true;
        EnemyHealth.WasHurt += EnemyHurt;
        cont = GameController.GetInstance();
        cont.RoundChanged += RoundChanged;
    }

    private void OnDestroy()
    {
        if (waitingForAttack)
            EnemyHealth.WasHurt -= EnemyHurt;
        cont.RoundChanged -= RoundChanged;
    }

    public void RoundChanged(object sender, int round)
    {
        gameObject.SetActive(true);

        if (round > 1)
            EnemyHealth.WasHurt += EnemyHurt;
        else
            EnemyHealth.WasHurt -= EnemyHurt;
    }

    public void EnemyHurt(object sender, System.EventArgs e)
    {
        EnemyHealth.WasHurt -= EnemyHurt;

        gameObject.SetActive(false);
    }
}
