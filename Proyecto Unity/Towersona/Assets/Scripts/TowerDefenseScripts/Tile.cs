using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isPath = false;
    public bool hasTower = false;
    public Vector2 position;    

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeTexture(Texture2D texture)
    {
        meshRenderer.material.mainTexture = texture;
    }  

    private void OnMouseUpAsButton()
    {
        if (!isPath && !hasTower && PlayerStats.TowerAvaible && !PlayerStats.MaxReached)
        {
            hasTower = true;
            World.Instance.SpawnTowersona(transform.position);
        }
    }
}
