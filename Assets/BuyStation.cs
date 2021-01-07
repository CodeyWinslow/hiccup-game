using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStation : MonoBehaviour
{
    [SerializeField]
    protected int cost;

    protected Player p;

    GameInput input;

    public System.EventHandler OnStation;
    public System.EventHandler OffStation;
    public System.EventHandler Buy;

    protected virtual void Awake()
    {
        input = GameInput.GetInstance();
    }

    protected void Start()
    {
        OffStation?.Invoke(this, null);
    }

    protected void Update()
    {
        if (input.GetSwitchTargetDown()
            && p != null)
        {
            Wallet w = p.GetComponent<Wallet>();
            if (w && w.Dollars >= cost)
            {
                w.DeductMoney(cost);
                Buy?.Invoke(this, null);
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player pl = other.GetComponent<Player>();
        if (pl)
        {
            p = pl;
            OnStation?.Invoke(this, null);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Player pl = other.GetComponent<Player>();
        if (pl)
        {
            p = null;
            OffStation?.Invoke(this, null);
        }
    }
}
