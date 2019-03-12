using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towersona : MonoBehaviour
{
    [Header("References")]
    public GameObject[] highpolyModels;
    public GameObject[] lowpolyModels;

    [HideInInspector]
    public Tile tile;   
    [HideInInspector]
    public DetailedTowersonaView detailedTowersonaView; 


    [HideInInspector]
    public TowersonaStats towersonaStats;  

    [Header("Parameters")]
    public int cost = 45;
    
    private TowersonaNeeds towersonaNeeds;
    private GameObject towersonaHOD;
    private GameObject towersonaLOD;

    private TowersController towersController;
    private World world;
    private GameManager gameManager;

    private void Initialize()
    {
        //References
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");

        towersController = gm.GetComponent<TowersController>();
        gameManager = gm.GetComponent<GameManager>();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();              

        //GetComponent<AudioSource>().Play();
    }

    public void LevelUp()
    {
        //TODO: leveling up
        print("Leveling up =^.^=");
        Destroy(towersonaLOD);
        Destroy(towersonaHOD);

        SpawnModel(lowpolyModels[1]);
        detailedTowersonaView.SpawnTowersonaNeedsModel(highpolyModels[1], this);
    }

    public void Evolve()
    {
        //TODO: evolving
    }

    private void OnMouseUpAsButton()
    {
        gameManager.ChangeCamera(detailedTowersonaView.GetComponentInChildren<Camera>());
        world.SelectTile(tile);      
    }

    public void Spawn(Tile tile)
    {
        Initialize(); 

        this.tile = tile;

        towersonaLOD = SpawnModel(lowpolyModels[0]);

        //Spawn towersona needs sceene and save a reference
        towersonaHOD = towersController.SpawnDetailedTowersonaView(this);
        detailedTowersonaView = towersonaHOD.GetComponentInParent<DetailedTowersonaView>();
        towersonaNeeds = towersonaHOD.GetComponentInChildren<TowersonaNeeds>();

        towersonaLOD.GetComponent<TowersonaStats>().towersonaNeeds = towersonaNeeds;
    }

    private GameObject SpawnModel(GameObject model)
    {
        Transform towersParent = GameObject.FindGameObjectWithTag("Towers Parent").transform;

        GameObject m = Instantiate(model, tile.transform.position, Quaternion.Euler(0f, 180f, 0f));
        m.transform.SetParent(towersParent);

 

        return m;
    }
}
