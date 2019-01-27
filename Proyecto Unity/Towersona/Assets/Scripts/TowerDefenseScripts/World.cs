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
    [SerializeField]
    private Camera defaultCamera;
    [SerializeField]
    private Color[] colors;
    private Stack<Color> towersonaColors;

    private float lastXUsed = 0f;

    public GameObject towersona;
    public GameObject detailedTowersonaViewPrefab;

    public List<TowersonaNeeds> towersonaNeeds;

    private Camera activeCamera;
    private Color rColor;

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

        towersonaColors = new Stack<Color>();
        tiles = new Tile[levelWidth, levelHeigth];
        controlPoints = new List<Transform>();
        towersonaNeeds = new List<TowersonaNeeds>();
        activeCamera = GameObject.FindGameObjectWithTag("Default Camera").GetComponent<Camera>();

        foreach (Color color in colors)
        {
            towersonaColors.Push(color);
        }
    }

    public void SpawnTowersona(Vector3 tilePosition, Tile tile) {
        TowerDefenseManager.Instance.towersBuilt++;
        Towersona t = Instantiate(towersona, tilePosition, Quaternion.identity).GetComponent<Towersona>();
        t.tile = tile;
        TowerDefenseManager.Instance.SelectTile(tile);
        TowerDefenseManager.Instance.towersonas.Add(t);
        PlayerStats.TowerAvaible = false;

        MeshRenderer[] mr = t.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer m in mr)
        {
            m.material.color = rColor;
        }

        SkinnedMeshRenderer[] smr = t.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer m in smr)
        {
            m.material.color = rColor;
        }


    }

    public TowersonaNeeds SpawnDetailedTowersonaView(Color color, Towersona towersona)
    {
        rColor = towersonaColors.Pop();

        Vector3 position = Vector3.zero;
        position.x = lastXUsed;
        position.z = 50f;
        lastXUsed += 15f;

        GameObject towersonaNeedsScene = Instantiate(detailedTowersonaViewPrefab, position, Quaternion.identity);
        TowersonaNeeds tsn = towersonaNeedsScene.GetComponentInChildren<TowersonaNeeds>();
        tsn.name = "Towersona need";
        //tsn.GetComponentInChildren<MeshRenderer>().material.color = color;   

        MeshRenderer[] mr = towersonaNeedsScene.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer m in mr)
        {
            m.material.color = rColor;
        }

        SkinnedMeshRenderer[] smr = towersonaNeedsScene.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer m in smr)
        {
            m.material.color = rColor;
        }

        towersonaNeeds.Add(tsn);
      
        return tsn;
    }

    public void ChangeCamera(Towersona towersona)
    {
       
        activeCamera.enabled = false;

        if(defaultCamera != null)
        {
            Destroy(defaultCamera.gameObject);
        }

        activeCamera = towersona.towersonaNeedsCamera;

        activeCamera.enabled = true;
    }
}
