using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuyStation : MonoBehaviour
{
    [SerializeField]
    BuyStation station;

    private void Awake()
    {
        if (station)
        {
            station.OnStation += EnteredStation;
            station.OffStation += ExitedStation;
        }
    }

    private void OnDestroy()
    {
        if (station)
        {
            station.OnStation -= EnteredStation;
            station.OffStation -= ExitedStation;
        }
    }

    public void EnteredStation(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }

    public void ExitedStation(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }
}
