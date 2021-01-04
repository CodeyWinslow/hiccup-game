using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyMessage : MonoBehaviour
{
    public event System.EventHandler<GameObject> Destroyed;

    private void OnDestroy()
    {
        Destroyed?.Invoke(this, gameObject);
    }
}
