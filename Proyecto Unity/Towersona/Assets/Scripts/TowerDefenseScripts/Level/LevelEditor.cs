using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Instance { get; private set; }

    public Texture2D background;

    [SerializeField]
    private GameObject spawnPointPrefab;
    
    MeshRenderer meshRenderer;

    private void OnValidate()
    {
        if (!Instance) Instance = this;
        else DestroyImmediate(this);

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial.mainTexture = background;
    }

    public void ChangeBackground(Texture2D texture)
    {
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

    public void CreatePath()
    {
        Debug.Log("Camino y spawnpoint asociado a él creado");
    }

    public void CreateBuildingSite()
    {
        Debug.Log("Buildingsite creado");
    }


    public void ResetLevel()
    {     
        background = null;
        meshRenderer.sharedMaterial.mainTexture = background;
    }

   
}

public struct LevelData
{
    public BezierSpline[] paths;
    public BuildingPlace[] buildingPlaces;
    public SpawnPoint[] spawnPoints;   
}
