using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isPath = false;
    public Vector2 position;    

    [SerializeField]
    private Material pathMaterial = null;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeTexture(Texture2D texture)
    {
        meshRenderer.material.mainTexture = texture;
    }
}
