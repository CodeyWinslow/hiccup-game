using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Singleton
    static UIManager instance;

    public static UIManager GetInstance() => instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
