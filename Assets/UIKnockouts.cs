using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIKnockouts : MonoBehaviour
{
    TextMeshPro text;
    GameController cont;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        cont = GameController.GetInstance();
        cont.KnockoutsChanged += UpdateKnockouts;
    }

    private void OnDestroy()
    {
        cont.KnockoutsChanged -= UpdateKnockouts;
    }

    // Update is called once per frame
    public void UpdateKnockouts(object sender, int knockouts)
    {
        text.text = knockouts + " Knockouts";
    }
}
