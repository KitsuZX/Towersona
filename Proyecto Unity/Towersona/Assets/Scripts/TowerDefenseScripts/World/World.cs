using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [Header("Parameters")]
    public int levelWidth = 10;
    public int levelHeigth = 10;

    [HideInInspector]
    public Tile[,] tiles;
    [HideInInspector]
    public List<Transform> controlPoints;
    [HideInInspector]
    public Tile tileSelected;

    //Private references
    private WorldGenerator generator;

    private void Awake()
    {      
        generator = GetComponent<WorldGenerator>();
 
        tiles = new Tile[levelWidth, levelHeigth];
        controlPoints = new List<Transform>();       
    }

    public void Generate()
    {
        generator.Generate();
    }     

    public void SelectTile(Tile tile)
    {
        if (tileSelected != null)
        {
            tileSelected.DeselectTile();
        }

        tileSelected = tile;

        tile.SelectTile();
    }
}
