using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asserts : MonoBehaviour
{
    public static void AssertNotNull(Object obj, string msg)
    {
        if (obj == null)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError(msg);
        }
    }
}
