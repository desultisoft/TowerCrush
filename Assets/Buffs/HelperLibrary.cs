using UnityEngine;

public static class HelperLibrary
{
    public static void AddMaterial(Material m, SpriteRenderer rend)
    {
        Material[] newMaterials = new Material[rend.materials.Length + 1];
        for (int i = 0; i < rend.materials.Length; i++)
        {
            newMaterials[i] = rend.materials[i];
        }
        newMaterials[rend.materials.Length] = m;
        rend.materials = newMaterials;

        foreach (Material m2 in rend.materials)
        {
            Debug.Log(m2.name);
        }
    }
}
