using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAssigner : MonoBehaviour
{
    public Texture2D[] textures;

    private SkinnedMeshRenderer meshRenderer;

    public void AssignTexture(int index)
    {
        if (index >= 0 && index < textures.Length)
        {
            meshRenderer.materials[1].mainTexture = textures[index];
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
}
