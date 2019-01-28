using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isPath = false;
    public bool hasTower = false;
 
    public Texture2D mainTexture;

    [HideInInspector]
    public Vector2 position;
    [HideInInspector]

    [SerializeField]
    private Color selectedColor;

    private MeshRenderer meshRenderer;
    private TowersController towersController;
    private World world;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        towersController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TowersController>();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
    }

    public void ChangeTexture(Texture2D texture)
    {
        mainTexture = texture;
        meshRenderer.material.mainTexture = mainTexture;
    }  

    public void SelectTile()
    {
        meshRenderer.material.color = selectedColor;
    }

    public void DeselectTile()
    { 
        meshRenderer.material.color = Color.white;
    }

    private void OnMouseUpAsButton()
    {
        if (!isPath && !hasTower && towersController.towerAvaible && !towersController.maxReached)
        {
            hasTower = true;
            towersController.SpawnTowersona(this);
        }
    }
}
