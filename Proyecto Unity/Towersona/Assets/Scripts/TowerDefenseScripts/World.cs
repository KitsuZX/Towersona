using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    public int levelWidth = 10;
    public int levelHeigth = 10;
    [HideInInspector]
    public Tile[,] tiles;
    [HideInInspector]
    public List<Transform> controlPoints;

    public GameObject towersona;

    private void Awake()
    {
        if (!Instance)
        {           
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        tiles = new Tile[levelWidth, levelHeigth];
        controlPoints = new List<Transform>();
    }

    public void SpawnTowersona(Vector3 tilePosition) {
        Instantiate(towersona, tilePosition, Quaternion.identity);
    }
}
