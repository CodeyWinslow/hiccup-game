using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Player instance;
    public static Player GetPlayer()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
