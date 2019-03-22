using UnityEngine;

public class Towersona : MonoBehaviour
{
    [Header("References")]   
    public GameObject towersonaLODPrefab;
    public GameObject towersonaHODPrefab;

    [Header("Models")]
    public Mesh[] lowpolyModels;
    public Mesh[] highpolyModels;

    [HideInInspector]
    public Tile tile;   
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

    [Header("Parameters")]
    public int cost = 45;
    
    private World world;

    public void Spawn(Tile _tile, string name)
    {
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        stats = new TowersonaStats(this);

        Transform parent = new GameObject().transform;
        parent.SetParent(GameObject.FindGameObjectWithTag("Towersonas Parent").transform, true);
        parent.name = name;

        tile = _tile; 

        //Spawn towersona LOD
        towersonaLOD = SpawnTowersonaLOD(parent, _tile.transform);

        //Spawn towersona HOD
        towersonaHOD = SpawnTowersonaHOD(parent);
        towersonaNeeds = towersonaHOD.GetComponentInChildren<TowersonaNeeds>();
    }

    public void Upgrade()
    {
        //TODO: Upgrading
        print("Upgrading =^.^=");
        towersonaLOD.SwitchModel(lowpolyModels[1]);
        towersonaHOD.Upgrade(highpolyModels[1]);

        towersonaLevel = TowersonaLevel.LVL2;
    }

    public void Evolve(int evolution)
    {
        //TODO: evolving
        print("Evolving to evolution " + (evolution + 1));
        //SwitchModel(lowpolyModels[evolution + 2]);
    } 

    public void TowersonaLODTouched()
    {        
        GameManager.Instance.ChangeCamera(towersonaHOD.GetComponentInChildren<Camera>());
        world.SelectTile(tile);

        BuildManager.Instance.SelectTowersona(this);
    }

    private TowersonaLOD SpawnTowersonaLOD(Transform parent, Transform tile)
    {
        GameObject towersonaObject = Instantiate(towersonaLODPrefab, tile.transform.position, Quaternion.Euler(0f, 180f, 0f));
        towersonaObject.GetComponentInChildren<MeshFilter>().mesh = lowpolyModels[0];
        towersonaObject.transform.SetParent(parent, true);
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

        TowersonaHOD towersonaHOD = towersonaHODObject.GetComponent<TowersonaHOD>();
        towersonaHODObject.transform.SetParent(parent, true);


        Camera camera = towersonaHOD.transform.GetComponentInChildren<Camera>();
        GameManager.Instance.ChangeCamera(camera);

        TowersonaNeeds tsn = towersonaHOD.SpawnTowersonaHOD(this, towersonaHODPrefab);
        tsn.name = parent.gameObject.name + " needs";

        BuildManager.Instance.lastXUsed += 15f;

        return towersonaHOD;
    }

    public enum TowersonaLevel
    {
        LVL1, LVL2, LVL31, LVL32
    }
}
