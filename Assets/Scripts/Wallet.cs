using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    int dollars;

    public int Dollars => dollars;

    private void Awake()
    {
        dollars = 0;
    }

    public void AddMoney(int amount)
    {
        dollars += amount;
    }

    public void DeductMoney(int amount)
    {
        dollars = Mathf.Max(0, dollars - amount);
    }
}
