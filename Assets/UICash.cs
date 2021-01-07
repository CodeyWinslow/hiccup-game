using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class UICash : MonoBehaviour
{
    [SerializeField]
    Wallet wallet;

    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (wallet)
            wallet.AmountChanged += OnCashChanged;
    }
    
    public void OnCashChanged(object sender, int amount)
    {
        text.text = "$" + amount;
    }
}
