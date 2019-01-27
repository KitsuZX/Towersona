using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isPath = false;
    public bool hasTower = false;
    public Vector2 position;

    [HideInInspector]
    public Texture2D mainTexture;

    [SerializeField]
    private Color selectedColor;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeTexture(Texture2D texture)
    {
        mainTexture = texture;
        meshRenderer.material.mainTexture = mainTexture;
    }  

    public void SelectTile()
    {
        //meshRenderer.material.color = selectedTexture;
        //meshRenderer.material.color = selectedColor;
    }

    public void DeselectTile()
    {

        //meshRenderer.material.mainTexture = mainTexture;
        meshRenderer.material.color = Color.white;
    }

    private void OnMouseUpAsButton()
    {
        if (!isPath && !hasTower && PlayerStats.TowerAvaible && !PlayerStats.MaxReached)
        {
            hasTower = true;
            World.Instance.SpawnTowersona(transform.position, this);
        }
    }
}
