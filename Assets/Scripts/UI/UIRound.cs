using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMesh))]
public class UIRound : MonoBehaviour
{
    TextMeshPro text;
    GameController cont;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        cont = GameController.GetInstance();
        cont.RoundChanged += OnRoundChanged;
    }

    private void OnDestroy()
    {
        cont.RoundChanged -= OnRoundChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Round: 0";
    }

    public void OnRoundChanged(object sender, int round)
    {
        text.text = "Round: " + round;
    }
}
