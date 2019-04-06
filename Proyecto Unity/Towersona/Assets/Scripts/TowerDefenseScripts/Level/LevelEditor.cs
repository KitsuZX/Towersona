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
    [SerializeField]
    private GameObject pathPrefab;
    [SerializeField]
    private GameObject buildingPlacePrefab;

    MeshRenderer meshRenderer;
 
    List<GameObject> paths;
    List<GameObject> buildingPlaces;

    private void OnValidate()
    {
        if (!Instance) Instance = this;
        else DestroyImmediate(this);

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial.mainTexture = background;
    }

    public void ChangeBackground(Texture2D texture)
    {
        background = texture;
        meshRenderer.sharedMaterial.mainTexture = background;
    }

    public GameObject CreatePath()
    {    
        GameObject path = Instantiate(pathPrefab);
        path.transform.SetParent(GameObject.FindGameObjectWithTag("Paths parent").transform, true);

        GameObject spawn = Instantiate(spawnPointPrefab);
        spawn.GetComponent<SpawnPoint>().path = path.GetComponent<BezierSpline>();
        spawn.transform.SetParent(path.transform, true);

        paths.Add(path);

        return path;
    }

    public GameObject CreateBuildingSite()
    {
        GameObject buildingPlace = Instantiate(buildingPlacePrefab);
        buildingPlace.transform.SetParent(GameObject.FindGameObjectWithTag("Build Places Parent").transform, true);

        buildingPlaces.Add(buildingPlace);

        return buildingPlace;
    }


    public void ResetLevel()
    {     
        background = null;
        meshRenderer.sharedMaterial.mainTexture = background;
       
        foreach (var path in paths)
        {
            DestroyImmediate(path);
        }
        paths.Clear();


        foreach (var buildingPlace in buildingPlaces)
        {
            DestroyImmediate(buildingPlace);
        }
        buildingPlaces.Clear();
    }
   
}


