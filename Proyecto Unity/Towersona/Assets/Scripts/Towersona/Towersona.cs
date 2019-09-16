using System.Collections.Generic;
using UnityEngine;

public class Towersona : MonoBehaviour
{
	public string menuName;

    [Header("References")] 
    public GameObject[] towersonaLODPrefabs;
    public GameObject[] towersonaHODPrefabs; 
        
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
    public TowersonaStats[] statsArray;

    [HideInInspector]
    public TowersonaStats stats;

    private Transform parent;

    private void OnValidate()
    {
		if(statsArray.Length > 0) stats = statsArray[0];
    }

    public void Spawn(BuildingPlace place, Transform parent)
    {
        this.place = place;
        this.parent = parent;

		place.towersona = this;

		//Spawn towersona HOD
		towersonaHOD = SpawnTowersonaHOD(parent);
        towersonaNeeds = towersonaHOD.GetComponentInChildren<TowersonaNeeds>();

        //Assign stats   
        stats = statsArray[0];
        stats.needs = towersonaNeeds;

        InvokeRepeating("UpdateStats", 0f, 0.1f);        

        //Spawn towersona LOD
        towersonaLOD = SpawnTowersonaLOD(parent);      
    }   

    public void LevelUp(int level)
    {
        CancelInvoke();

        towersonaLevel = (TowersonaLevel)(level + 1);

        Destroy(towersonaLOD.gameObject);
        towersonaLOD = SpawnTowersonaLOD(parent);

        Destroy(towersonaHOD.gameObject);
        towersonaHOD = SpawnTowersonaHOD(parent);
        towersonaNeeds = towersonaHOD.GetComponentInChildren<TowersonaNeeds>();

        stats = statsArray[level + 1];
        stats.needs = towersonaNeeds;
        InvokeRepeating("UpdateStats", 0f, 0.1f);

        PlayerStats.Instance.SpendMoney(stats.buyCost);
    } 

    public void TowersonaLODTouched()
    {        
        GameManager.Instance.ChangeCamera(towersonaHOD.GetComponentInChildren<Camera>());
        //world.SelectTile(tile);

        BuildManager.Instance.SelectTowersona(this);
    }

    private TowersonaLOD SpawnTowersonaLOD(Transform parent)
    {
        GameObject towersonaObject = Instantiate(towersonaLODPrefabs[(int)towersonaLevel], place.buildingSpot.position, Quaternion.Euler(-17f, 180f, 0f));
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

	public void UpdateStats()
	{
		towersonaLOD.pattern.UpdateStats();
	}

    public void Sell()
    {
		place.hasTower = false;
        PlayerStats.Instance.AddMoney(stats.sellCost);
        Destroy(gameObject);
    }

    public enum TowersonaLevel
    {
        LVL1, LVL2, LVL31, LVL32
    }
}
