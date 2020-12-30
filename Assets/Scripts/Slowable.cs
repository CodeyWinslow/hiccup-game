using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowable : MonoBehaviour
{
    [SerializeField]
    float slowSpeed = 2.5f;
    [SerializeField]
    float normalSpeed = 5f;

    Movement move;

    private void Awake()
    {
        move = GetComponent<Movement>();
    }

    public void Slow()
    {
        move.ChangeSpeed(slowSpeed);
    }

    public void Unslow()
    {
        move.ChangeSpeed(normalSpeed);
    }
}
