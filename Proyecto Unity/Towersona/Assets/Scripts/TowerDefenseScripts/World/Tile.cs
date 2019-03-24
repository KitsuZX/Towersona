using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public bool isPath = false;
    [HideInInspector]
    public bool hasTower = false;
    [HideInInspector]
    public Vector2 position;

    [SerializeField]
    private Color selectedColor;

    //Private references
    private MeshRenderer meshRenderer;
    private BuildManager towersController;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        towersController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BuildManager>();
    }

    public void ChangeTexture(Texture2D texture)
    {     
        meshRenderer.material.mainTexture = texture;
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
        if (!isPath && !hasTower)
        {
            hasTower = true;
            towersController.SpawnTowersona(this);
        }
    }
}
