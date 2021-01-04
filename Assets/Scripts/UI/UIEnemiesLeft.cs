using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemiesLeft : MonoBehaviour
{
    TextMeshPro text;
    GameController cont;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        cont = GameController.GetInstance();
        cont.EnemiesLeftChanged += UpdateEnemiesLeft;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        cont.EnemiesLeftChanged -= UpdateEnemiesLeft;
    }

    public void UpdateEnemiesLeft(object sender, int enemiesLeft)
    {
        text.text = enemiesLeft + " Enemies Left";
    }
}
