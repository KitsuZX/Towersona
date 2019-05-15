using System.Collections.Generic;
using UnityEngine;

public class Towersona : MonoBehaviour
{
    [Header("Parameters")]
    public int[] costs = new int[4];

    [Header("References")] 
    public GameObject[] towersonaLODPrefabs;
    public GameObject[] towersonaHODPrefabs;
    
    [Header("Models")]
    public Mesh[] lowpolyModels;
    public Mesh[] highpolyModels;
        
    [HideInInspector]
    public BuildingPlace place;   
    [HideInInspector]
    public TowersonaHOD towersonaHOD;
    [HideInInspector]
    public TowersonaLOD towersonaLOD;
    [HideInInspector]
    public TowersonaNeeds towersonaNeeds;   

    [HideInInspector]
    public TowersonaLevel towersonaLevel = TowersonaLevel.LVL1;
    [HideInInspector]
    public TowersonaStats stats;

    private Transform parent;


    public void Spawn(BuildingPlace place, Transform parent)
    {
        this.place = place;
        this.parent = parent;

        //Spawn towersona LOD
        towersonaLOD = SpawnTowersonaLOD(parent);

        //Spawn towersona HOD
        towersonaHOD = SpawnTowersonaHOD(parent);
        towersonaNeeds = towersonaHOD.GetComponentInChildren<TowersonaNeeds>();

 
        stats = new TowersonaStats(this);
    }

    public void LevelUp()
    {     
        print("Upgrading =^.^=");
        towersonaLevel = TowersonaLevel.LVL2;

        Destroy(towersonaLOD.gameObject);
        towersonaLOD = SpawnTowersonaLOD(parent);

        Destroy(towersonaHOD.gameObject);
        towersonaHOD = SpawnTowersonaHOD(parent);

        stats.AssignData();

        PlayerStats.Instance.SpendMoney(costs[(int)towersonaLevel]);
    }

    public void Evolve(int evolution)
    {       
        print("Evolving to evolution " + (evolution + 1));

        towersonaLevel = (TowersonaLevel)(evolution + 2);

        Destroy(towersonaLOD.gameObject);
        towersonaLOD = SpawnTowersonaLOD(parent);

        Destroy(towersonaHOD.gameObject);
        towersonaHOD = SpawnTowersonaHOD(parent);

        PlayerStats.Instance.SpendMoney(costs[(int)towersonaLevel]);
    } 

    public void TowersonaLODTouched()
    {        
        GameManager.Instance.ChangeCamera(towersonaHOD.GetComponentInChildren<Camera>());
        //world.SelectTile(tile);

        BuildManager.Instance.SelectTowersona(this);
    }

    private TowersonaLOD SpawnTowersonaLOD(Transform parent)
    {
        GameObject towersonaObject = Instantiate(towersonaLODPrefabs[(int)towersonaLevel], place.transform.position, Quaternion.Euler(-17f, 180f, 0f));
        towersonaObject.transform.SetParent(parent);
        towersonaObject.name = gameObject.name + " LOD";

        TowersonaLOD towersonaLOD = towersonaObject.GetComponent<TowersonaLOD>();
        towersonaLOD.towersona = this;

        return towersonaLOD;
    }

    private TowersonaHOD SpawnTowersonaHOD(Transform parent)
    {
        Vector3 buildingPosition = new Vector3(BuildManager.Instance.lastXUsed, 0f, 50f); 

        GameObject towersonaHODObject = Instantiate(BuildManager.Instance.detailedTowersonaViewPrefab, buildingPosition, Quaternion.identity);
        towersonaHODObject.name = gameObject.name + " HOD";
        towersonaHODObject.transform.SetParent(parent);

        TowersonaHOD towersonaHOD = towersonaHODObject.GetComponent<TowersonaHOD>();       

        Camera camera = towersonaHOD.transform.GetComponentInChildren<Camera>();
        GameManager.Instance.ChangeCamera(camera);

        TowersonaNeeds tsn = towersonaHOD.SpawnTowersonaHOD(this, towersonaHODPrefabs[(int)towersonaLevel]);
        tsn.name = gameObject.name + " needs";

        BuildManager.Instance.lastXUsed += 15f;

        return towersonaHOD;
    }

    public enum TowersonaLevel
    {
        LVL1, LVL2, LVL31, LVL32
    }
}
