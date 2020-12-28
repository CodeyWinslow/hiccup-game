using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cullable : MonoBehaviour
{
    Material[] originalMaterials;
    MeshRenderer rend;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        if (rend)
        {
            originalMaterials = rend.materials.Clone() as Material[];
        }
    }

    public void Hide(Material hideMat)
    {
        if (rend)
            rend.materials = new Material[1]{ hideMat };
    }

    public void Show()
    {
        if (rend)
            rend.materials = originalMaterials.Clone() as Material[];
    }
}
