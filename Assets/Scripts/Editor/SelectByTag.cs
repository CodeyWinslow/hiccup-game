using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectByTag : ScriptableObject
{
    
    private static string SelectedTag = "Player";

    [MenuItem("Helpers/Select By Tag")]
    public static void SelectObjectsWithTag()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(SelectedTag);
        Selection.objects = objects;
    }

    [MenuItem("Helpers/Tags/Player")]
    public static void SetTagToPlayer()
    {
        SelectedTag = "Player";
    }

    [MenuItem("Helpers/Tags/Wall")]
    public static void SetTagToWall()
    {
        SelectedTag = "Wall";
    }
}
