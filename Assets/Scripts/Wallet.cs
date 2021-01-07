using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    int dollars;

    public int Dollars => dollars;

    public event System.EventHandler<int> AmountChanged;

    private void Awake()
    {
        dollars = 0;
    }

    private void Start()
    {
        AmountChanged?.Invoke(this, dollars);
    }

    public void AddMoney(int amount)
    {
        dollars += amount;
        AmountChanged?.Invoke(this, dollars);
    }

    public void DeductMoney(int amount)
    {
        dollars = Mathf.Max(0, dollars - amount);
        AmountChanged?.Invoke(this, dollars);
    }
}
