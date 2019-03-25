using System.Collections.Generic;
using UnityEngine;

public class Towersona : MonoBehaviour
{
    [Header("Parameters")]
    public int[] costs = new int[4];

    [Header("References")]   
    public GameObject towersonaLODPrefab;
    public GameObject towersonaHODPrefab;

    [Header("Models")]
    public Mesh[] lowpolyModels;
    public Mesh[] highpolyModels;
    
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
    
    private World world;

    public void Spawn(BuildingPlace place, Transform parent)
    {
        this.place = place;
        //Spawn towersona LOD
        towersonaLOD = SpawnTowersonaLOD(parent);

        //Spawn towersona HOD
        towersonaHOD = SpawnTowersonaHOD(parent);
        towersonaNeeds = towersonaHOD.GetComponentInChildren<TowersonaNeeds>();

        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        stats = new TowersonaStats(this);
    }

    public void LevelUp()
    {
        //TODO: Upgrading
        print("Upgrading =^.^=");
        towersonaLOD.SwitchModel(lowpolyModels[1]);
        towersonaHOD.Upgrade(highpolyModels[1]);

        towersonaLevel = TowersonaLevel.LVL2;

        stats.AssignData();

        PlayerStats.Instance.SpendMoney(costs[(int)towersonaLevel]);
    }

    public void Evolve(int evolution)
    {
        //TODO: evolving
        print("Evolving to evolution " + (evolution + 1));
        //SwitchModel(lowpolyModels[evolution + 2]);

        //PlayerStats.Instance.SpendMoney(costs[(int)towersonaLevel]);
    } 

    public void TowersonaLODTouched()
    {        
        GameManager.Instance.ChangeCamera(towersonaHOD.GetComponentInChildren<Camera>());
        //world.SelectTile(tile);

        BuildManager.Instance.SelectTowersona(this);
    }

    private TowersonaLOD SpawnTowersonaLOD(Transform parent)
    {
        GameObject towersonaObject = Instantiate(towersonaLODPrefab, place.transform.position, Quaternion.Euler(0f, 180f, 0f));
        towersonaObject.GetComponentInChildren<MeshFilter>().mesh = lowpolyModels[0];
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

        TowersonaNeeds tsn = towersonaHOD.SpawnTowersonaHOD(this, towersonaHODPrefab);
        tsn.name = gameObject.name + " needs";

        BuildManager.Instance.lastXUsed += 15f;

        return towersonaHOD;
    }

    public enum TowersonaLevel
    {
        LVL1, LVL2, LVL31, LVL32
    }
}
