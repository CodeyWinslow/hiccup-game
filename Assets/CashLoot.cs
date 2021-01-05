using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashLoot : MonoBehaviour
{
    [SerializeField]
    int minAmount;
    [SerializeField]
    int maxAmount;

    int amount;

    private void Awake()
    {
        amount = Random.Range(minAmount, maxAmount);
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Wallet wallet = other.GetComponent<Wallet>();
        if (wallet)
        {
            wallet.AddMoney(amount);
            Destroy(gameObject);
        }
    }
}
