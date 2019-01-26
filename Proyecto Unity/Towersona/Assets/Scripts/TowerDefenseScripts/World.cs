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

    private float lastZUsed = 50f;

    public GameObject towersona;
    public GameObject detailedTowersonaViewPrefab;

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

    public TowersonaNeeds SpawnDetailedTowersonaView()
    {
        Vector3 position = Vector3.zero;
        position.z = lastZUsed;
        lastZUsed += 50f;

        TowersonaNeeds tsn = Instantiate(detailedTowersonaViewPrefab, position, Quaternion.identity).GetComponentInChildren<TowersonaNeeds>();
        tsn.name = "Towersona need";
        return tsn;
    }
}
