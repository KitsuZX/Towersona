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

    private float lastXUsed = 0f;

    public GameObject towersona;
    public GameObject detailedTowersonaViewPrefab;

    public List<TowersonaNeeds> towersonaNeeds;

    private Camera activeCamera;

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
        towersonaNeeds = new List<TowersonaNeeds>();
    }

    public void SpawnTowersona(Vector3 tilePosition) {
        Instantiate(towersona, tilePosition, Quaternion.identity);     
    
    }

    public TowersonaNeeds SpawnDetailedTowersonaView(Color color, Towersona towersona)
    {
        Vector3 position = Vector3.zero;
        position.x = lastXUsed;
        position.z = 50f;
        lastXUsed += 15f;

        GameObject towersonaNeedsScene = Instantiate(detailedTowersonaViewPrefab, position, Quaternion.identity);
        TowersonaNeeds tsn = towersonaNeedsScene.GetComponentInChildren<TowersonaNeeds>();
        tsn.name = "Towersona need";
        tsn.GetComponentInChildren<MeshRenderer>().material.color = color;    

        towersonaNeeds.Add(tsn);
      
        return tsn;
    }

    public void ChangeCamera(Towersona towersona)
    {
        if (activeCamera != null)
        {
            activeCamera.enabled = false;
        }

        activeCamera = towersona.towersonaNeedsCamera;

        activeCamera.enabled = true;
    }
}
